#region License

// Copyright (c) 2009, ClearCanvas Inc.
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
using ClearCanvas.Dicom.Utilities.Xml;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Common.Utilities;
using ClearCanvas.ImageServer.Core.Validation;
using ClearCanvas.ImageServer.Model;

namespace Martin.ImageServer.Services.Archiving.Nas
{

    /// <summary>
    /// Enumerated values for different levels of validation
    /// </summary>
    [Flags]
    internal enum ValidationLevels
    {
        /// <summary>
        /// Include study level validation
        /// </summary>
        Study,

        /// <summary>
        /// Include series level validation
        /// </summary>
        Series
    }

    /// <summary>
    /// Class to validate the study after it has been restored from the archive.
    /// </summary>
    internal class StudyStorageValidator
    {
        #region Public Methods

        /// <summary>
        /// Validates the specified <see cref="StudyStorageLocation"/>.
        /// </summary>
        /// <param name="storageLocation">The study to be validated</param>
        /// <param name="validationLevels">Set to </param>
        public void Validate(StudyStorageLocation storageLocation, ValidationLevels validationLevels)
        {
            StudyXml studyXml = storageLocation.LoadStudyXml();
            Study study = storageLocation.Study;
            ServerPartition partition = storageLocation.ServerPartition;

            if ((validationLevels & ValidationLevels.Study) == ValidationLevels.Study)
            {
                DoStudyLevelValidation(storageLocation, studyXml, study, partition);
            }

            if ( (validationLevels & ValidationLevels.Series) == ValidationLevels.Series)
            {
                DoSeriesLevelValidation(storageLocation, studyXml, study);
            }

        }

        #endregion

        #region Private Methods

        private void DoSeriesLevelValidation(StudyStorageLocation storageLocation, StudyXml studyXml, Study study)
        {
            IDictionary<string, Series> seriesList = study.Series;
            foreach (var entry in seriesList)
            {
                Series series = entry.Value;
                SeriesXml seriesXml = studyXml[series.SeriesInstanceUid];

                ValidateSeries(storageLocation, series, seriesXml);

            }
        }

        private void DoStudyLevelValidation(StudyStorageLocation storageLocation, StudyXml studyXml, Study study, ServerPartition partition)
        {
            int xmlNumInstances = studyXml.NumberOfStudyRelatedInstances;
            int xmlNumSeries = studyXml.NumberOfStudyRelatedSeries;

            if (study.NumberOfStudyRelatedInstances != xmlNumInstances)
            {
                throw new StudyIntegrityValidationFailure(
                    ValidationErrors.InconsistentObjectCount,
                    new ValidationStudyInfo(study, partition),
                    String.Format("Number of study related instances in the database ({0}) does not match number of images in the filesystem ({1})",
                                  study.NumberOfStudyRelatedInstances, xmlNumInstances));
            }

            if (study.NumberOfStudyRelatedSeries != xmlNumSeries)
            {
                throw new StudyIntegrityValidationFailure(ValidationErrors.InconsistentObjectCount,
                                                          new ValidationStudyInfo(study, partition),
                                                          String.Format("Number of study related series in the database ({0}) does not match number of series in the xml ({1})",
                                                                        study.NumberOfStudyRelatedSeries, xmlNumSeries));

            }


            long dirFileCount = DirectoryUtility.Count(storageLocation.GetStudyPath(),
                                                       "*" + ServerPlatform.DicomFileExtension, true, null);

            if (xmlNumInstances != dirFileCount)
            {
                throw new StudyIntegrityValidationFailure(ValidationErrors.InconsistentObjectCount,
                                                          new ValidationStudyInfo(study, partition),
                                                          String.Format("Number of instance in xml ({0}) does not match number of images in the filesystem ({1})",
                                                                        xmlNumInstances, dirFileCount));
            }
        }

        private void ValidateSeries(StudyStorageLocation location, Series series, SeriesXml seriesXml)
        {
            Study study = location.Study;
            ServerPartition partition = location.ServerPartition;

            if (seriesXml == null)
            {
                throw new StudyIntegrityValidationFailure(ValidationErrors.InconsistentObjectCount,
                                                          new ValidationStudyInfo(study, partition),
                                                          String.Format("Series {0} exists in the datbase but not in the study xml",
                                                                        series.SeriesInstanceUid));

            }

            if (series.NumberOfSeriesRelatedInstances != seriesXml.NumberOfSeriesRelatedInstances)
            {
                throw new StudyIntegrityValidationFailure(ValidationErrors.InconsistentObjectCount,
                                                          new ValidationStudyInfo(study, partition),
                                                          String.Format("Number of Series Related Instance in the database and xml for series {0} do not match: {1} vs {2}",
                                                                        series.SeriesInstanceUid, series.NumberOfSeriesRelatedInstances, seriesXml.NumberOfSeriesRelatedInstances));

            }

            long seriesImageCount = DirectoryUtility.Count(location.GetSeriesPath(series.SeriesInstanceUid), "*" + ServerPlatform.DicomFileExtension, true, null);
            if (seriesXml.NumberOfSeriesRelatedInstances != seriesImageCount)
            {
                throw new StudyIntegrityValidationFailure(ValidationErrors.InconsistentObjectCount,
                                                          new ValidationStudyInfo(study, partition),
                                                          String.Format("Number of Series Related Instance in the xml for series {0} does not match number of images in the series folder: {1} vs {2}",
                                                                        series.SeriesInstanceUid, seriesXml.NumberOfSeriesRelatedInstances, seriesImageCount));
            }

        }

        #endregion

    }
}