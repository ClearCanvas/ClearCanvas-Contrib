using System;
using System.Collections.Generic;
using System.Text;

namespace SeriesQuery
{
    public class ImageItem
    {

        /// <summary>
        /// Initializes a new instance of <see cref="StudyItem"/>.
        /// </summary>
        public ImageItem()
        {
        }

        /// <summary>
        /// Gets or sets the Image number
        /// </summary>
        public string ImageNumber
        {
            get { return _ImageNumber; }
            set { _ImageNumber = value; }
        }

        /// <summary>
        /// Gets or sets the Image description.
        /// </summary>
        public string ImageDescription
        {
            get { return _ImageDescription; }
            set { _ImageDescription = value; }
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
        /// Gets or sets the Image Instance UID.
        /// </summary>
        public string ImageInstanceUID
        {
            get { return _ImageInstanceUID; }
            set { _ImageInstanceUID = value; }
        }

        /// <summary>
        /// Gets or sets the number of Image related instances.
        /// </summary>
        public string NumberOfImageRelatedInstances
        {
            get { return _numberOfImageRelatedInstances; }
            set { _numberOfImageRelatedInstances = value; }
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
        string _ImageNumber;
        private string _ImageDescription;
        private string _studyInstanceUID;
        private string _ImageInstanceUID;
        private string _modality;
        private string _numberOfImageRelatedInstances;
        private string _date;
        private string _time;
        #endregion
    }

}
