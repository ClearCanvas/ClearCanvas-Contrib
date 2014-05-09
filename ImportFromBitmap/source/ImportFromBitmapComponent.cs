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
using System.IO;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Validation;
using ClearCanvas.ImageViewer.Services.LocalDataStore;

namespace ImportFromBitmap
{
	[ExtensionPoint]
	public sealed class ImportFromBitmapComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
	{
	}

	public enum PatientsSex
	{
		M,
		F,
		O
	}

	public enum PhotometricInterpretation
	{
		Monochrome2,
		Rgb
	}

	[AssociateView(typeof(ImportFromBitmapComponentViewExtensionPoint))]
	public partial class ImportFromBitmapComponent : ApplicationComponent
	{
		private static volatile ImportProcessor _activeProcessor;

		private IDesktopWindow _desktopWindow;
		private string _patientId;
		private string _lastName;
		private string _firstName;
		private string _middleName;
		private PatientsSex _sex;
		private DateTime? _dob;
		private string _accessionNumber;
		private PhotometricInterpretation _photometricInterpretation;

		private string _studyDescription;
		private DateTime? _studyDate;
		private DateTime? _studyTime;

		private ImportFromBitmapComponent()
		{
			_dob = null;
			_studyDate = Platform.Time.Date;
		}

		[ValidateLength(1, 64, Message = "MessageMustEnterPatientId")]
		[ValidateRegex(@"[\r\n\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string PatientId
		{
			get { return _patientId; }
			set
			{
				if (value == _patientId)
					return;

				_patientId = value;
				NotifyPropertyChanged("PatientId");
			}
		}

		[ValidateLength(1, Message = "MessageMustEnterPatientName")]
		[ValidateRegex(@"[\r\n\e\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (value == _lastName)
					return;

				_lastName = value;
				NotifyPropertyChanged("LastName");
			}
		}

		[ValidateLength(1, Message = "MessageMustEnterPatientName")]
		[ValidateRegex(@"[\r\n\e\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (value == _firstName)
					return;

				_firstName = value;
				NotifyPropertyChanged("FirstName");
			}
		}

		[ValidateRegex(@"[\r\n\e\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string MiddleName
		{
			get { return _middleName; }
			set
			{
				if (value == _middleName)
					return;

				_middleName = value;
				NotifyPropertyChanged("MiddleName");
			}
		}

		public PatientsSex Sex
		{
			get { return _sex; }
			set
			{
				if (value == _sex)
					return;

				_sex = value;
				NotifyPropertyChanged("Sex");
			}
		}
	
		public DateTime? Dob
		{
			get { return _dob; }
			set
			{
				if (value == _dob)
					return;

				_dob = value;
				NotifyPropertyChanged("Dob");
			}
		}

		[ValidateLength(1, 16, Message = "MessageMustEnterAccession")]
		[ValidateRegex(@"[\r\n\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string AccessionNumber
		{
			get { return _accessionNumber; }
			set
			{
				if (value == _accessionNumber)
					return;

				_accessionNumber = value;
				NotifyPropertyChanged("AccessionNumber");
			}
		}

		[ValidateLength(0, 64, Message = "MessageStudyDescriptionTooLong")]
		[ValidateRegex(@"[\r\n\f\\]+", SuccessOnMatch = false, Message = "MessageInvalidCharacters")]
		public string StudyDescription
		{
			get { return _studyDescription; }
			set
			{
				if (value == _studyDescription)
					return;

				_studyDescription = value;
				NotifyPropertyChanged("StudyDescription");
			}
		}

		public DateTime? StudyDate
		{
			get { return _studyDate; }
			set
			{
				if (value == _studyDate)
					return;

				_studyDate = value;
				NotifyPropertyChanged("StudyDate");
			}
		}

		public DateTime? StudyTime
		{
			get { return _studyTime; }
			set
			{
				if (value == _studyTime)
					return;

				_studyTime = value;
				NotifyPropertyChanged("StudyTime");
			}
		}

		public PhotometricInterpretation PhotometricInterpretation
		{
			get { return _photometricInterpretation; }
			set
			{
				if (value == _photometricInterpretation)
					return;

				_photometricInterpretation = value;
				NotifyPropertyChanged("PhotometricInterpretation");
			}
		}

		public override void Start()
		{
			base.Start();
			_desktopWindow = this.Host.DesktopWindow;
		}

		public void Cancel()
		{
			base.ExitCode = ApplicationComponentExitCode.None;
			this.Host.Exit();
		}

		public void Accept()
		{
			if (base.HasValidationErrors)
			{
				base.ShowValidation(true);
				return;
			}

			base.ExitCode = ApplicationComponentExitCode.Accepted;
			this.Host.Exit();
		}

		internal static void Launch(IDesktopWindow parent, IEnumerable<string> paths)
		{
			try
			{
				if (_activeProcessor != null)
				{
					parent.ShowMessageBox(SR.MessageImportAlreadyRunning, MessageBoxActions.Ok);
					return;
				}

				if (!LocalDataStoreActivityMonitor.IsConnected)
				{
					parent.ShowMessageBox(SR.MessageLocalDataStoreServiceMustBeRunning, MessageBoxActions.Ok);
					return;
				}

				if (null == CollectionUtils.FirstElement(GetFiles(paths)))
				{
					parent.ShowMessageBox(SR.MessageNothingToImport, MessageBoxActions.Ok);
					return;
				}

				ImportFromBitmapComponent component = new ImportFromBitmapComponent();
				if (ApplicationComponentExitCode.Accepted != LaunchAsDialog(parent, component, SR.TitleImportNonDicom))
					return;

				_activeProcessor = new ImportProcessor(component, paths);
				_activeProcessor.Run();
			}
			catch (Exception e)
			{
				ExceptionHandler.Report(e, parent);
			}
		}

		private static void OnProcessingComplete()
		{
			_activeProcessor = null;
		}

		private static IEnumerable<string> GetFiles(IEnumerable<string> paths)
		{
			foreach (string path in paths)
			{
				if (File.Exists(path))
				{
					yield return path;
				}
				else if (Directory.Exists(path))
				{
					foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
						yield return file;
				}
			}
		}
	}
}
