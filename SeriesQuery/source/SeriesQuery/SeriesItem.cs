using System;
using System.Collections.Generic;
using System.Text;

namespace SeriesQuery
{
    public class SeriesItem
    {
        
            /// <summary>
            /// Initializes a new instance of <see cref="StudyItem"/>.
            /// </summary>
            public SeriesItem()
            {
            }

        /// <summary>
            /// Gets or sets the series number
            /// </summary>
            public string SeriesNumber
            {
                get { return _seriesNumber; }
                set { _seriesNumber = value; }
            }

            /// <summary>
            /// Gets or sets the series description.
            /// </summary>
            public string SeriesDescription
            {
                get { return _seriesDescription; }
                set { _seriesDescription = value; }
            }

            /// <summary>
            /// Gets or sets the modality
            /// </summary>
            public string Modality
            {
                get { return _modality; }
                set { _modality = value; }
            }

            /// <summary>
            /// Gets or sets the Study Instance UID.
            /// </summary>
            public string StudyInstanceUID
            {
                get { return _studyInstanceUID; }
                set { _studyInstanceUID = value; }
            }

            /// <summary>
            /// Gets or sets the Series Instance UID.
            /// </summary>
            public string SeriesInstanceUID
            {
                get { return _seriesInstanceUID; }
                set { _seriesInstanceUID = value; }
            }

            /// <summary>
            /// Gets or sets the number of series related instances.
            /// </summary>
            public string NumberOfSeriesRelatedInstances
            {
                get { return _numberOfSeriesRelatedInstances; }
                set { _numberOfSeriesRelatedInstances = value; }
            }


            public string Date
            {
                get { return _date; }
                set { _date = value; }
            }

            public string Time
            {
                get { return _time; }
                set { _time = value; }
            }
             
            #region Private Members
            string _seriesNumber; 
            private string _seriesDescription;
            private string _studyInstanceUID;
            private string _seriesInstanceUID;
            private string _modality;
            private string _numberOfSeriesRelatedInstances;
            private string _date;
            private string _time;
            #endregion
        }
    
}
