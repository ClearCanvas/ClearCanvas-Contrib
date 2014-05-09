#region License

// Copyright (c) 2012, Stewart Bright
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Dicom;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using System.Runtime.InteropServices;
using ClearCanvas.ImageViewer.Common;

namespace ImportFromBitmap
{
	public partial class ImportFromBitmapComponent
	{
		private class ImportProcessor
		{
			#region Private Fields

			private readonly ImportFromBitmapComponent _parent;
			private readonly IEnumerable<string> _paths;

			private volatile IBackgroundTaskContext _taskContext;
			private volatile string _tempFileDirectory;

			private volatile DicomAttributeCollection _baseDataSet;
			private volatile int _numberOfFilesConverted;
			private volatile int _numberOfBadFiles;
			private Exception _error;

			private readonly object _waitShowProgressLock;
			private bool _showProgress;

			#endregion

			public ImportProcessor(ImportFromBitmapComponent parent, IEnumerable<string> paths)
			{
				_parent = parent;
				_paths = paths;
				_waitShowProgressLock = new object();
				_showProgress = false;
			}

			#region Properties

			private IDesktopWindow DesktopWindow
			{
				get { return _parent._desktopWindow; }
			}

			#endregion

			#region Public Methods

			public void Run()
			{
				CreateBaseDataSet();
				
				_tempFileDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "ClearCanvas");
				_tempFileDirectory = System.IO.Path.Combine(_tempFileDirectory, "BitmapImport");
				DeleteEmptyFolders(_tempFileDirectory);

				_tempFileDirectory = System.IO.Path.Combine(_tempFileDirectory, System.IO.Path.GetRandomFileName());
				Directory.CreateDirectory(_tempFileDirectory);

				BackgroundTask task = new BackgroundTask(Process, true);
				task.Terminated += delegate
									{
										OnComplete();
										task.Dispose();
									};

				lock (_waitShowProgressLock)
				{
					task.Run();

					Monitor.Wait(_waitShowProgressLock);
					if (_showProgress)
					{
						ProgressDialogComponent progressComponent = new ProgressDialogComponent(task, true, ProgressBarStyle.Blocks);
						LaunchAsShelf(DesktopWindow, progressComponent, SR.TitleImportingImages, ShelfDisplayHint.DockFloat);
					}

					Monitor.Pulse(_waitShowProgressLock);
				}
			}

			#endregion

			#region Private Methods

			private void CreateBaseDataSet()
			{
				_baseDataSet = new DicomAttributeCollection();
				
				//Sop Common
				_baseDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.SecondaryCaptureImageStorageUid);

				//Patient
				_baseDataSet[DicomTags.PatientId].SetStringValue(_parent.PatientId);
				_baseDataSet[DicomTags.PatientsName].SetStringValue(String.Format("{0}^{1}^{2}^^",
				                                                                  _parent.LastName, _parent.FirstName, _parent.MiddleName));

				_baseDataSet[DicomTags.PatientsBirthDate].SetDateTime(0, _parent.Dob);
				_baseDataSet[DicomTags.PatientsSex].SetStringValue(_parent.Sex.ToString());

				//Study
				_baseDataSet[DicomTags.StudyInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
				_baseDataSet[DicomTags.StudyDate].SetDateTime(0, _parent.StudyDate);
				_baseDataSet[DicomTags.StudyTime].SetDateTime(0, _parent.StudyTime);
				_baseDataSet[DicomTags.AccessionNumber].SetStringValue(_parent.AccessionNumber);
				_baseDataSet[DicomTags.StudyDescription].SetStringValue(_parent.StudyDescription);

				_baseDataSet[DicomTags.ReferringPhysiciansName].SetNullValue();
				_baseDataSet[DicomTags.StudyId].SetNullValue();

				//Series
				_baseDataSet[DicomTags.SeriesInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
				_baseDataSet[DicomTags.Modality].SetStringValue("OT");
				_baseDataSet[DicomTags.SeriesNumber].SetStringValue("1");

				//SC Equipment
				_baseDataSet[DicomTags.ConversionType].SetStringValue("WSD");

				//General Image
				_baseDataSet[DicomTags.ImageType].SetStringValue(@"DERIVED\SECONDARY");
				_baseDataSet[DicomTags.PatientOrientation].SetNullValue();

				//Image Pixel
				switch(_parent._photometricInterpretation)
				{
					case ImportFromBitmap.PhotometricInterpretation.Monochrome2:
						_baseDataSet[DicomTags.SamplesPerPixel].SetInt32(0, 1);
						_baseDataSet[DicomTags.PhotometricInterpretation].SetStringValue("MONOCHROME2");
						_baseDataSet[DicomTags.BitsAllocated].SetInt32(0, 8);
						_baseDataSet[DicomTags.BitsStored].SetInt32(0, 8);
						_baseDataSet[DicomTags.HighBit].SetInt32(0, 7);
						_baseDataSet[DicomTags.PixelRepresentation].SetInt32(0, 0);
						_baseDataSet[DicomTags.PlanarConfiguration].SetInt32(0, 0);
						break;
					case ImportFromBitmap.PhotometricInterpretation.Rgb:
					default:
						_baseDataSet[DicomTags.SamplesPerPixel].SetInt32(0, 3);
						_baseDataSet[DicomTags.PhotometricInterpretation].SetStringValue("RGB");
						_baseDataSet[DicomTags.BitsAllocated].SetInt32(0, 8);
						_baseDataSet[DicomTags.BitsStored].SetInt32(0, 8);
						_baseDataSet[DicomTags.HighBit].SetInt32(0, 7);
						_baseDataSet[DicomTags.PixelRepresentation].SetInt32(0, 0);
						_baseDataSet[DicomTags.PlanarConfiguration].SetInt32(0, 0);
						break;
				}
			}

			private void OnComplete()
			{
				OnProcessingComplete();

				if (_error != null)
				{
					DesktopWindow.ShowMessageBox(SR.MessageUnexpectedError, MessageBoxActions.Ok);
				}
				else if (_numberOfFilesConverted == 0)
				{
					string message;
					if (_numberOfBadFiles > 0)
						message = String.Format(SR.MessageFormatTotalFailure, _numberOfBadFiles);
					else
						message = SR.MessageNothingToImport;

					DesktopWindow.ShowMessageBox(message, MessageBoxActions.Ok);
				}
				else
				{
					string message = String.Format(SR.MessageFormatReportSuccess, 
						_numberOfFilesConverted, _numberOfFilesConverted + _numberOfBadFiles);
					
					DesktopWindow.ShowMessageBox(message, MessageBoxActions.Ok);
				}
			}

			private void Process(IBackgroundTaskContext context)
			{
				try
				{
					_taskContext = context;

					ICollection<string> files = GetFilesToImport();
					if (files.Count > 0 && !_taskContext.CancelRequested)
					{
						ConvertFiles(files);
						ImportFiles();
					}
				}
				catch(Exception e)
				{
					Platform.Log(LogLevel.Error, e);
					_error = e;
				}

				if (_taskContext.CancelRequested)
					_taskContext.Cancel();
				else
					_taskContext.Complete();
			}

			private ICollection<string> GetFilesToImport()
			{
				lock (_waitShowProgressLock)
				{
					try
					{
						_taskContext.ReportProgress(new BackgroundTaskProgress(0, SR.MessageEnumeratingFiles));
						List<string> files = new List<string>();
						foreach (string file in GetFiles(_paths))
						{
							if (_taskContext.CancelRequested)
								break;

							files.Add(file);

							if (!_showProgress && files.Count >= 10)
							{
								_showProgress = true;
								Monitor.Pulse(_waitShowProgressLock);
								Monitor.Wait(_waitShowProgressLock);
							}
						}

						return files;
					}
					finally
					{
						if (!_showProgress)
						{
							Monitor.Pulse(_waitShowProgressLock);
							Monitor.Wait(_waitShowProgressLock);
						}
					}
				}
			}

			private void ConvertFiles(ICollection<string> files)
			{
				int progressStep = 1;
				int instanceNumber = 1;

				foreach (string file in files)
				{
					if (_taskContext.CancelRequested)
						break;

					int progressPercent = Math.Min((int)(progressStep / (float)files.Count * 100), 100);
					string progressMessage = String.Format(SR.MessageFormatConvertingToDicom, progressStep++, files.Count);
					_taskContext.ReportProgress(new BackgroundTaskProgress(progressPercent, progressMessage));

					try 
					{
						using (Bitmap image = LoadImage(file))
						{
							DicomFile dicomFile = ConvertImage(image, instanceNumber);
							dicomFile.Save();

							++instanceNumber;
							++_numberOfFilesConverted;
						}
					}
					catch (Exception e)
					{
						++_numberOfBadFiles;
						Platform.Log(LogLevel.Warn, e, "The file could not be converted to Dicom, likely because it's format isn't recognized ({0}).", file);
					}
				}
			}

			private void ImportFiles()
			{
				LocalDataStoreServiceClient client = new LocalDataStoreServiceClient();
				try
				{
					client.Open();
					FileImportRequest request = new FileImportRequest();
					request.FilePaths = new string[] { _tempFileDirectory };
					request.Recursive = true;
					request.FileImportBehaviour = FileImportBehaviour.Move;
					client.Import(request);
					client.Close();
				}
				catch
				{
					client.Abort();
					throw;
				}
			}

			private DicomFile ConvertImage(Bitmap image, int instanceNumber)
			{
				DicomUid sopInstanceUid = DicomUid.GenerateUid();

				string fileName = String.Format("{0}.dcm", sopInstanceUid.UID);
				fileName = System.IO.Path.Combine(_tempFileDirectory, fileName);

				DicomFile dicomFile = new DicomFile(fileName, new DicomAttributeCollection(), _baseDataSet.Copy(true, true, true));

				//meta info
				dicomFile.MediaStorageSopInstanceUid = sopInstanceUid.UID;
				dicomFile.MediaStorageSopClassUid = SopClass.SecondaryCaptureImageStorageUid;

				//General Image
				dicomFile.DataSet[DicomTags.InstanceNumber].SetInt32(0, instanceNumber);

				DateTime now = Platform.Time;
				DateTime time = DateTime.MinValue.Add(new TimeSpan(now.Hour, now.Minute, now.Second));

				//SC Image
				dicomFile.DataSet[DicomTags.DateOfSecondaryCapture].SetDateTime(0, now);
				dicomFile.DataSet[DicomTags.TimeOfSecondaryCapture].SetDateTime(0, time);

				//Sop Common
				dicomFile.DataSet[DicomTags.InstanceCreationDate].SetDateTime(0, now);
				dicomFile.DataSet[DicomTags.InstanceCreationTime].SetDateTime(0, time);
				dicomFile.DataSet[DicomTags.SopInstanceUid].SetStringValue(sopInstanceUid.UID);

				int rows, columns;
				//Image Pixel
				switch(_parent.PhotometricInterpretation)
				{
					case PhotometricInterpretation.Monochrome2:
						dicomFile.DataSet[DicomTags.PixelData].Values = GetMonochromePixelData(image, out rows, out columns);
						break;
					case PhotometricInterpretation.Rgb:
					default:
						dicomFile.DataSet[DicomTags.PixelData].Values = GetColorPixelData(image, out rows, out columns);
						break;
				}

				//Image Pixel
				dicomFile.DataSet[DicomTags.Rows].SetInt32(0, rows);
				dicomFile.DataSet[DicomTags.Columns].SetInt32(0, columns);
				
				return dicomFile;
			}

			#endregion

			#region Private Helper Methods
			
			private static Bitmap LoadImage(string file)
			{
				Bitmap image = Image.FromFile(file, true) as Bitmap;
				if (image == null)
					throw new ArgumentException(String.Format("The specified file cannot be loaded as a bitmap {0}.", file));

				if (image.PixelFormat != PixelFormat.Format24bppRgb)
				{
					Platform.Log(LogLevel.Info, "Attempting to convert non RBG image to RGB ({0}) before converting to Dicom.", file);

					Bitmap old = image;
					using (old)	
					{
						image = new Bitmap(old.Width, old.Height, PixelFormat.Format24bppRgb);
						using (Graphics g = Graphics.FromImage(image))
						{
							g.DrawImage(old, 0, 0, old.Width, old.Height);
						}
					}
				}

				return image;
			}

			private static byte[] GetMonochromePixelData(Bitmap image, out int rows, out int columns)
			{
				rows = image.Height;
				columns = image.Width;

				//At least one of rows or columns must be even.
				if (rows % 2 != 0 && columns % 2 != 0)
					--columns; //trim the last column.

				int size = rows * columns;
				byte[] pixelData = MemoryManager.Allocate<byte>(size);
				int i = 0;

				for (int row = 0; row < rows; ++row)
				{
					for (int column = 0; column < columns; column++)
					{
						pixelData[i++] = image.GetPixel(column, row).R;
					}
				}

				return pixelData;
			}

			private static byte[] GetColorPixelData(Bitmap image, out int rows, out int columns)
			{
				rows = image.Height;
				columns = image.Width;

				//At least one of rows or columns must be even.
				if (rows%2 != 0 && columns%2 != 0)
					--columns; //trim the last column.

				BitmapData data = image.LockBits(new Rectangle(0, 0, columns, rows), ImageLockMode.ReadOnly, image.PixelFormat);
				IntPtr bmpData = data.Scan0;

				try
				{
					int stride = columns*3;
					int size = rows*stride;

					byte[] pixelData = MemoryManager.Allocate<byte>(size);

					for (int i = 0; i < rows; ++i)
						Marshal.Copy(new IntPtr(bmpData.ToInt64() + i*data.Stride), pixelData, i*stride, stride);

					//swap BGR to RGB
					SwapRedBlue(pixelData);
					return pixelData;
				}
				finally
				{
					image.UnlockBits(data);
				}
			}

			private static void SwapRedBlue(byte[] pixels)
			{
				for (int i = 0; i < pixels.Length; i += 3)
				{
					byte temp = pixels[i];
					pixels[i] = pixels[i + 2];
					pixels[i + 2] = temp;
				}
			}

			private static void DeleteEmptyFolders(string directory)
			{
				if (!Directory.Exists(directory))
					return;

				foreach (string subDirectory in Directory.GetDirectories(directory))
				{
					try
					{
						string[] files = Directory.GetFileSystemEntries(subDirectory);
						if (files == null || files.Length == 0)
							Directory.Delete(subDirectory);
					}
					catch (Exception e)
					{
						Platform.Log(LogLevel.Warn, e, "Failed to delete old temp directory ({0})", subDirectory);
					}
				}
			}

			#endregion
		}
	}
}
