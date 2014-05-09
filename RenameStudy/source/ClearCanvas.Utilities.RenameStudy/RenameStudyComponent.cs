#region License
// Seth Berkowitz 2010
//
// Copyright (c) 2006-20010, ClearCanvas Inc.
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
using System.Collections.ObjectModel;
using System.Reflection;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Dicom.Utilities;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.Desktop.Validation;
using ClearCanvas.Dicom.Utilities.Anonymization;
using ClearCanvas.Utilities.DicomEditor;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.ImageViewer;

namespace ClearCanvas.Utilities.RenameStudy
{
    /// <summary>
    /// Extension point for views onto <see cref="renameStudyComponent"/>.
    /// </summary>
    [ExtensionPoint]
    public sealed class RenameStudyComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    /// <summary>
    /// renameStudyComponent class.
    /// </summary>
    [AssociateView(typeof(RenameStudyComponentViewExtensionPoint))]
    public class RenameStudyComponent : ApplicationComponent
    {
        private class ValidateAnonymizationRule : IValidationRule
        {
            private readonly RenameStudyComponent _parent;
            private readonly string _property;
            private readonly ValidationFailureReason _validationReason;

            public ValidateAnonymizationRule(RenameStudyComponent parent, string property, ValidationFailureReason validationReason)
            {
                _parent = parent;
                _property = property;
                _validationReason = validationReason;
            }

            #region IValidationRule Members

            public string PropertyName
            {
                get { return _property; }
            }

            public ValidationResult GetResult(IApplicationComponent component)
            {
                ReadOnlyCollection<ValidationFailureDescription> failures = _parent.GetValidationFailures();
                foreach (ValidationFailureDescription failure in failures)
                {
                    if (failure.PropertyName == _property && _validationReason == failure.Reason)
                    {
                        if (failure.Reason == ValidationFailureReason.EmptyValue)
                            return new ValidationResult(false, "The value cannot be empty.");
                        else
                            return new ValidationResult(false, "The value conflicts with the original value.");
                    }
                }

                return new ValidationResult(true, "");
            }

            #endregion
        }

        private readonly StudyData _original;
        private readonly StudyData _renamed;
        private readonly DicomAnonymizer.ValidationStrategy _validator;
            
        private bool _preserveSeriesData;

        internal RenameStudyComponent(StudyItem studyItem)
        {
            _original = new StudyData();
            _original.AccessionNumber = studyItem.AccessionNumber;
            _original.PatientsName = studyItem.PatientsName;
            _original.PatientId = studyItem.PatientId;
            _original.StudyDescription = studyItem.StudyDescription;
            _original.PatientsBirthDateRaw = studyItem.PatientsBirthDate;
            _original.StudyDateRaw = studyItem.StudyDate;

            _renamed = _original.Clone();

            _validator = new DicomAnonymizer.ValidationStrategy();
        }

        internal StudyData OriginalData
        {
            get { return _original; }
        }

        internal StudyData RenamedData
        {
            get { return _renamed; }
        }

        public string PatientsName
        {
            get { return _renamed.PatientsNameRaw; }
            set
            {
                if (_renamed.PatientsNameRaw == value)
                    return;

                _renamed.PatientsNameRaw = value;
                NotifyPropertyChanged("PatientsName");
            }
        }

        public string PatientId
        {
            get { return _renamed.PatientId; }
            set
            {
                if (_renamed.PatientId == value)
                    return;

                _renamed.PatientId = value;
                NotifyPropertyChanged("PatientId");
            }
        }

        public string AccessionNumber
        {
            get { return _renamed.AccessionNumber; }
            set
            {
                if (_renamed.AccessionNumber == value)
                    return;

                _renamed.AccessionNumber = value;
                NotifyPropertyChanged("AccessionNumber");
            }
        }

        public string StudyDescription
        {
            get { return _renamed.StudyDescription; }
            set
            {
                if (_renamed.StudyDescription == value)
                    return;

                _renamed.StudyDescription = value;
                NotifyPropertyChanged("StudyDescription");
            }
        }

        public DateTime? StudyDate
        {
            get { return _renamed.StudyDate; }
            set
            {
                if (_renamed.StudyDate == value)
                    return;

                _renamed.StudyDate = value;
                NotifyPropertyChanged("StudyDate");
            }
        }

        public DateTime? PatientsBirthDate
        {
            get { return _renamed.PatientsBirthDate; }
            set
            {
                if (_renamed.PatientsBirthDate == value)
                    return;

                _renamed.PatientsBirthDate = value;
                NotifyPropertyChanged("PatientsBirthDate");
            }
        }

        public bool PreserveSeriesData
        {
            get { return _preserveSeriesData; }
            set
            {
                if (_preserveSeriesData == value)
                    return;

                _preserveSeriesData = value;
                NotifyPropertyChanged("PreserveSeriesDescriptions");
            }
        }

        public override void Start()
        {
            // Use original DICOM values instead of anonymous info

            //_renamed.PatientsNameRaw = "Patient^Anonymous";
            //_renamed.PatientId = "12345";
            //_renamed.StudyDate = Platform.Time;
            //_renamed.AccessionNumber = "A12345";
            //_preserveSeriesData = true;

            //if (_renamed.PatientsBirthDate != null)
            //{
            //    _renamed.PatientsBirthDate = new DateTime(_renamed.PatientsBirthDate.Value.Year, 1, 1, 0, 0, 0);
            //    _renamed.PatientsBirthDate = _renamed.PatientsBirthDate.Value.AddDays(TimeSpan.FromDays(new Random().Next(0, 364)).Days);
            //}

            _renamed.PatientsNameRaw = _original.PatientsNameRaw;
            _renamed.PatientId = _original.PatientId;
            _renamed.StudyDate = _original.StudyDate;
            _renamed.AccessionNumber = _original.AccessionNumber;
            _preserveSeriesData = true;
            _renamed.PatientsBirthDate = _original.PatientsBirthDate;

            base.Start();

            base.Validation.Add(new ValidateAnonymizationRule(this, "PatientId", ValidationFailureReason.EmptyValue));
            base.Validation.Add(new ValidateAnonymizationRule(this, "PatientsName", ValidationFailureReason.EmptyValue));
            base.Validation.Add(new ValidateAnonymizationRule(this, "AccessionNumber", ValidationFailureReason.EmptyValue));
            base.Validation.Add(new ValidateAnonymizationRule(this, "StudyDescription", ValidationFailureReason.EmptyValue));
            base.Validation.Add(new ValidateAnonymizationRule(this, "StudyDate", ValidationFailureReason.EmptyValue));
            base.Validation.Add(new ValidateAnonymizationRule(this, "PatientsBirthDate", ValidationFailureReason.EmptyValue));

            //base.Validation.Add(new ValidateAnonymizationRule(this, "PatientId", ValidationFailureReason.ConflictingValue));
            //base.Validation.Add(new ValidateAnonymizationRule(this, "PatientsName", ValidationFailureReason.ConflictingValue));
            //base.Validation.Add(new ValidateAnonymizationRule(this, "AccessionNumber", ValidationFailureReason.ConflictingValue));
            //base.Validation.Add(new ValidateAnonymizationRule(this, "StudyDescription", ValidationFailureReason.ConflictingValue));
            //base.Validation.Add(new ValidateAnonymizationRule(this, "StudyDate", ValidationFailureReason.ConflictingValue));
            //base.Validation.Add(new ValidateAnonymizationRule(this, "PatientsBirthDate", ValidationFailureReason.ConflictingValue));
        }

        public void Accept()
        {
            if (this.HasValidationErrors)
            {
                base.ShowValidation(true);
            }
            else
            {
                this.ExitCode = ApplicationComponentExitCode.Accepted;
                this.Host.Exit();
            }
        }

        public void Cancel()
        {
            this.ExitCode = ApplicationComponentExitCode.None;
            this.Host.Exit();
        }

        private ReadOnlyCollection<ValidationFailureDescription> GetValidationFailures()
        {
            return _validator.GetValidationFailures(_original, _renamed);
        }
    }
}
