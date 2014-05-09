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

using System.Windows.Forms;
using ClearCanvas.Common;
using ClearCanvas.Desktop.View.WinForms;

namespace ImportFromBitmap.View.WinForms
{
	/// <summary>
	/// Provides a Windows Forms user-interface for <see cref="ImportFromBitmapComponent"/>.
	/// </summary>
	public partial class ImportFromBitmapComponentControl : ApplicationComponentUserControl
	{
		private readonly ImportFromBitmapComponent _component;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ImportFromBitmapComponentControl(ImportFromBitmapComponent component)
			: base(component)
		{
			_component = component;
			InitializeComponent();

			//set these values in case the underlying component's is null, so that when they are un-checked,
			//they will be reasonable.
			_dob.Value = Platform.Time;
			_studyDate.Value = Platform.Time;
			_studyTime.Value = Platform.Time;

			_patientId.DataBindings.Add("Value", _component, "PatientId", true, DataSourceUpdateMode.OnPropertyChanged);
			_dob.DataBindings.Add("Value", _component, "Dob", true, DataSourceUpdateMode.OnPropertyChanged);
			_firstName.DataBindings.Add("Value", _component, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged);
			_lastName.DataBindings.Add("Value", _component, "LastName", true, DataSourceUpdateMode.OnPropertyChanged);
			_middleName.DataBindings.Add("Value", _component, "MiddleName", true, DataSourceUpdateMode.OnPropertyChanged);
			_accessionNumber.DataBindings.Add("Value", _component, "AccessionNumber", true, DataSourceUpdateMode.OnPropertyChanged);
			_studyDescription.DataBindings.Add("Value", _component, "StudyDescription", true, DataSourceUpdateMode.OnPropertyChanged);
			_studyDate.DataBindings.Add("Value", _component, "StudyDate", true, DataSourceUpdateMode.OnPropertyChanged);
			_studyTime.DataBindings.Add("Value", _component, "StudyTime", true, DataSourceUpdateMode.OnPropertyChanged);

			Binding maleBinding = new Binding("Checked", _component, "Sex", true, DataSourceUpdateMode.OnPropertyChanged);
			Binding femaleBinding = new Binding("Checked", _component, "Sex", true, DataSourceUpdateMode.OnPropertyChanged);
			Binding otherBinding = new Binding("Checked", _component, "Sex", true, DataSourceUpdateMode.OnPropertyChanged);

			_male.DataBindings.Add(maleBinding);
			_female.DataBindings.Add(femaleBinding);
			_other.DataBindings.Add(otherBinding);

			maleBinding.Format += delegate(object sender, ConvertEventArgs e)
									{
										if (e.DesiredType != typeof(bool))
											return;

										e.Value = ((PatientsSex)e.Value) == PatientsSex.M;
									};

			femaleBinding.Format += delegate(object sender, ConvertEventArgs e)
									{
										if (e.DesiredType != typeof(bool))
											return;

										e.Value = ((PatientsSex)e.Value) == PatientsSex.F;
									};

			otherBinding.Format += delegate(object sender, ConvertEventArgs e)
									{
										if (e.DesiredType != typeof(bool))
											return;

										e.Value = ((PatientsSex)e.Value) == PatientsSex.O;
									};

			maleBinding.Parse += new ConvertEventHandler(OnParseSex);
			femaleBinding.Parse += new ConvertEventHandler(OnParseSex);
			otherBinding.Parse += new ConvertEventHandler(OnParseSex);

			#region Bindings for Photometric Interpretation

			Binding monochrome2Binding = new Binding("Checked", _component, "PhotometricInterpretation", true, DataSourceUpdateMode.OnPropertyChanged);
			Binding rgbBinding = new Binding("Checked", _component, "PhotometricInterpretation", true, DataSourceUpdateMode.OnPropertyChanged);

			_piMonochrome2.DataBindings.Add(monochrome2Binding);
			_piRgb.DataBindings.Add(rgbBinding);

			monochrome2Binding.Format += delegate(object sender, ConvertEventArgs e)
			{
				if (e.DesiredType != typeof(bool))
					return;

				e.Value = ((PhotometricInterpretation)e.Value) == PhotometricInterpretation.Monochrome2;
			};

			rgbBinding.Format += delegate(object sender, ConvertEventArgs e)
			{
				if (e.DesiredType != typeof(bool))
					return;

				e.Value = ((PhotometricInterpretation)e.Value) == PhotometricInterpretation.Rgb;
			};

			monochrome2Binding.Parse += new ConvertEventHandler(OnParsePhotometricInterpretation);
			rgbBinding.Parse += new ConvertEventHandler(OnParsePhotometricInterpretation);

			#endregion

			_cancel.Click += delegate { _component.Cancel(); };
			_ok.Click += delegate { _component.Accept(); };
		}

		private void OnParseSex(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(PatientsSex))
				return;

			if (_male.Checked)
				e.Value = PatientsSex.M;
			else if (_female.Checked)
				e.Value = PatientsSex.F;
			else
				e.Value = PatientsSex.O;
		}

		private void OnParsePhotometricInterpretation(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(PhotometricInterpretation))
				return;

			if (_piMonochrome2.Checked)
				e.Value = PhotometricInterpretation.Monochrome2;
			else
				e.Value = PhotometricInterpretation.Rgb;
		}
	}
}