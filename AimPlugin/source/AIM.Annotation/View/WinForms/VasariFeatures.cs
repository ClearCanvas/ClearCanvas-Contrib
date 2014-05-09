using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using aim_dotnet;
using ClearCanvas.Common.Utilities;

namespace AIM.Annotation.View.WinForms
{
    public partial class VasariFeatures : UserControl
    {
        protected readonly DropDownEntity _ddEntAE;
        protected readonly DropDownEntity _ddEntEloquentBrainIO;
        protected readonly DropDownEntity _lesionAE;

        public VasariFeatures()
        {
            InitializeComponent();

            this.SuspendLayout();
            int startPtY = 3;
            int cnt = 0;

        	const int verticalScrollbarWidth = 0; // SystemInformation.VerticalScrollBarWidth;

            // Location/Anatmic Entity
            _ddEntAE = new DropDownEntity("Location (Anatomic Entity)", VasariTemplateData.AnatomicEntityData, false);
            _ddEntAE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        	_ddEntAE.AutoScaleMode = AutoScaleMode.Inherit;
            _ddEntAE.Location = new System.Drawing.Point(3, startPtY);
            _ddEntAE.Name = "dddEntAE" + cnt;
			_ddEntAE.Size = new Size(this.Width - this.Margin.Horizontal - verticalScrollbarWidth, _ddEntAE.Height);
            _ddEntAE.TabIndex = cnt;
            _ddEntAE.Tag = "AnatomicEntity";
            startPtY += _ddEntAE.Height + 3;
            cnt++;
            this.Controls.Add(_ddEntAE);

            // Eloquent Brain Involvement/Imaging Observation
            _ddEntEloquentBrainIO = new DropDownEntity("Eloquent Brain Involvement (Imaging Observation)", VasariTemplateData.EloquentBrainInvolvementData, false);
            _ddEntEloquentBrainIO.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			_ddEntEloquentBrainIO.AutoScaleMode = AutoScaleMode.Inherit;
            _ddEntEloquentBrainIO.Location = new System.Drawing.Point(3, startPtY);
            _ddEntEloquentBrainIO.Name = "ddEntEloquentBrainIO" + cnt;
			_ddEntEloquentBrainIO.Size = new Size(this.Width - this.Margin.Horizontal - verticalScrollbarWidth, _ddEntEloquentBrainIO.Height);
            _ddEntEloquentBrainIO.TabIndex = cnt;
            _ddEntEloquentBrainIO.Tag = "ImagingObservation";
            startPtY += _ddEntEloquentBrainIO.Height + 3;
            cnt++;
            this.Controls.Add(_ddEntEloquentBrainIO);

            // Eloquent Brain Involvement/Imaging Observation
            _lesionAE = new DropDownEntity("Lesion (Imaging Observation)", VasariTemplateData.LesionData, false);
            _lesionAE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			_lesionAE.AutoScaleMode = AutoScaleMode.Inherit;
            _lesionAE.Location = new System.Drawing.Point(3, startPtY);
            _lesionAE.Name = "lesionAE" + cnt;
			_lesionAE.Size = new Size(this.Width - this.Margin.Horizontal - verticalScrollbarWidth, _lesionAE.Height);
            _lesionAE.TabIndex = cnt;
            _lesionAE.Tag = "ImagingObservation";
            startPtY += _lesionAE.Height + 3;
            cnt++;
            this.Controls.Add(_lesionAE);

        	DropDownEntity ddEnt;
            // ImagingObservationCharacteristic for Lesion/ImagingObservation
            foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> entity in VasariTemplateData.ImagingObservationCharData)
            {
                ddEnt = new DropDownEntity(entity.Key, entity.Value, false);
                ddEnt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                ddEnt.Location = new System.Drawing.Point(25, startPtY);
                ddEnt.Name = "dddEnt" + cnt;
				ddEnt.Size = new Size(this.Width - this.Margin.Horizontal - verticalScrollbarWidth - 20, ddEnt.Height);
                ddEnt.TabIndex = cnt;
                ddEnt.Tag = "ImagingObservationCharacteristic";
                startPtY += ddEnt.Height + 3;
                cnt++;
                this.Controls.Add(ddEnt);
            }

            this.ResumeLayout(false);
        }

        public AnatomicEntity SelectedAnatomicEntity
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "AnatomicEntity"))
                    {
                        AnatomicEntity anatomicEntity = new AnatomicEntity();
                        anatomicEntity.CodeValue = ddEnt.SelectedEntity.Key;
                        anatomicEntity.CodeMeaning = ddEnt.SelectedEntity.Value;
                        anatomicEntity.CodingSchemeDesignator = "VASARI";

                        return anatomicEntity;
                    }
                }

                return null;
            }

            set
            {
                if (value == null)
                    return;

                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "AnatomicEntity"))
                    {
                        if (IsValueInCollection(VasariTemplateData.AnatomicEntityData, new KeyValuePair<string, string>(value.CodeValue, value.CodeMeaning)))
                        {
                            ddEnt.SelectedEntity = new KeyValuePair<string, string>(value.CodeValue, value.CodeMeaning);
                            return;
                        }
                    }
                }
            }
        }

        public List<ImagingObservation> SelectedImagingObservations
        {
            get
            {
                List<ImagingObservation> imagingObservations = new List<ImagingObservation>();
                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string) ddEnt.Tag, "ImagingObservation"))
                    {
                        ImagingObservation imagingObservation = new ImagingObservation();
                        imagingObservation.CodeValue = ddEnt.SelectedEntity.Key;
                        imagingObservation.CodeMeaning = ddEnt.SelectedEntity.Value;
                        imagingObservation.CodingSchemeDesignator = "VASARI";

                        // Get Imaging Observation Characteristics for Tumor Imaging Observation
                        if (IsValueInCollection(VasariTemplateData.LesionData, ddEnt.SelectedEntity))
                        {
                            List<ImagingObservationCharacteristic> imgChars = new List<ImagingObservationCharacteristic>();
                            foreach (Control ctrlIOC in this.Controls)
                            {
                                DropDownEntity ddEntIOC = ctrlIOC as DropDownEntity;
                                if (ddEntIOC != null && string.Equals((string)ddEntIOC.Tag, "ImagingObservationCharacteristic"))
                                {
                                    if (!string.IsNullOrEmpty(ddEntIOC.SelectedEntity.Key) && !string.IsNullOrEmpty(ddEntIOC.SelectedEntity.Value))
                                    {
                                        ImagingObservationCharacteristic imagingObsChar = new ImagingObservationCharacteristic();
                                        imagingObsChar.CodeValue = ddEntIOC.SelectedEntity.Key;
                                        imagingObsChar.CodeMeaning = ddEntIOC.SelectedEntity.Value;
                                        imagingObsChar.CodingSchemeDesignator = "VASARI";

                                        imgChars.Add(imagingObsChar);
                                    }
                                }
                            }
                            if (imgChars.Count > 0)
                                imagingObservation.ImagingObservationCharacteristicCollection = imgChars;
                        }

                        imagingObservations.Add(imagingObservation);
                    }
                }

                return imagingObservations.Count > 0 ? imagingObservations : null;
            }

            set
            {
                if (value == null)
                    return;

                foreach (ImagingObservation imagingObservation in value)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        DropDownEntity ddEnt = ctrl as DropDownEntity;
                        if (ddEnt != null && string.Equals((string) ddEnt.Tag, "ImagingObservation") &&
                            ddEnt.HasCode(imagingObservation.CodeValue))
                        {
                            ddEnt.SelectedEntity = new KeyValuePair<string, string>(imagingObservation.CodeValue, imagingObservation.CodeMeaning);

                            if (imagingObservation.ImagingObservationCharacteristicCollection != null && 
                                imagingObservation.ImagingObservationCharacteristicCollection.Count > 0 &&
                                IsValueInCollection(VasariTemplateData.LesionData, ddEnt.SelectedEntity))
                            {
                                // Imaging Observation Characteristics for Tumor Imaging Observation
                                foreach (Control ctrlIOC in this.Controls)
                                {
                                    DropDownEntity ddEntIOC = ctrlIOC as DropDownEntity;
                                    if (ddEntIOC != null && string.Equals((string)ddEntIOC.Tag, "ImagingObservationCharacteristic"))
                                    {
                                        bool isCtrlUpdated = false;

                                        // Find correct Imaging Observation Characteristic for the given DropDownEntity 
                                        foreach (ImagingObservationCharacteristic imgObsChar in imagingObservation.ImagingObservationCharacteristicCollection)
                                        {
                                            if (ddEntIOC.HasCode(imgObsChar.CodeValue))
                                            {
                                                ddEntIOC.SelectedEntity = new KeyValuePair<string, string>(imgObsChar.CodeValue, imgObsChar.CodeMeaning);
                                                isCtrlUpdated = true;
                                                break;
                                            }
                                        }

                                        if (!isCtrlUpdated) // no Imaging Obs Char exists for this DropDownEntity
                                            ddEntIOC.SelectedEntity = new KeyValuePair<string, string>(string.Empty, string.Empty);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public ImagingObservation SelectedEloquentBrainInvolvementIO
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "ImagingObservation"))
                    {
                        if (IsValueInCollection(VasariTemplateData.EloquentBrainInvolvementData, ddEnt.SelectedEntity))
                        {
                            ImagingObservation imagingObservation = new ImagingObservation();
                            imagingObservation.CodeValue = ddEnt.SelectedEntity.Key;
                            imagingObservation.CodeMeaning = ddEnt.SelectedEntity.Value;
                            imagingObservation.CodingSchemeDesignator = "VASARI";
                            return imagingObservation;
                        }
                    }
                }

                return null;
            }

            set
            {
                if (value == null)
                    return;

                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "ImagingObservation"))
                    {
                        KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(value.CodeValue, value.CodeMeaning);
                        if (IsValueInCollection(VasariTemplateData.EloquentBrainInvolvementData, kvp))
                        {
                            ddEnt.SelectedEntity = kvp;
                            return;
                        }
                    }
                }
            }
        }

        public ImagingObservation SelectedImaingObservation
        {
            get
            {
                ImagingObservation imagingObservation = new ImagingObservation();
                imagingObservation.CodeValue = "84";
                imagingObservation.CodeMeaning = "Tumor";
                imagingObservation.CodingSchemeDesignator = "VASARI";

                List<ImagingObservationCharacteristic> imgChars = new List<ImagingObservationCharacteristic>();
                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "ImagingObservationCharacteristic"))
                    {
                        if (ddEnt.SelectedEntity.Key != string.Empty && ddEnt.SelectedEntity.Value != string.Empty)
                        {
                            ImagingObservationCharacteristic imagingObsChar = new ImagingObservationCharacteristic();
                            imagingObsChar.CodeValue = ddEnt.SelectedEntity.Key;
                            imagingObsChar.CodeMeaning = ddEnt.SelectedEntity.Value;
                            imagingObsChar.CodingSchemeDesignator = "VASARI";

                            imgChars.Add(imagingObsChar);
                        }
                    }
                }
                if (imgChars.Count > 0)
                    imagingObservation.ImagingObservationCharacteristicCollection = imgChars;

                return imagingObservation;
            }

            set
            {
                if (value == null)
                    return;

                bool ioFound = false;

                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "ImagingObservation"))
                    {
                        KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(value.CodeValue, value.CodeMeaning);
                        if (IsValueInCollection(VasariTemplateData.LesionData, kvp))
                        {
                            ddEnt.SelectedEntity = kvp;
                            ioFound = true;
                            break;
                        }
                    }
                }

                // wrong IO or no Imaging Obs Char
                if (!ioFound || value.ImagingObservationCharacteristicCollection == null || value.ImagingObservationCharacteristicCollection.Count == 0)
                    return;


                foreach (Control ctrl in this.Controls)
                {
                    DropDownEntity ddEnt = ctrl as DropDownEntity;
                    if (ddEnt != null && string.Equals((string)ddEnt.Tag, "ImagingObservationCharacteristic"))
                    {
                        bool isCtrlUpdated = false;

                        // Find correct Imaging Observation Characteristic for the given DropDownEntity 
                        foreach (ImagingObservationCharacteristic imgObsChar in value.ImagingObservationCharacteristicCollection)
                        {
                            if (ddEnt.HasCode(imgObsChar.CodeValue))
                            {
                                ddEnt.SelectedEntity = new KeyValuePair<string, string>(imgObsChar.CodeValue, imgObsChar.CodeMeaning);
                                isCtrlUpdated = true;
                                break;
                            }
                        }

                        if (!isCtrlUpdated) // no Imaging Obs Char exists for this DropDownEntity
                            ddEnt.SelectedEntity = new KeyValuePair<string, string>(string.Empty, string.Empty);
                    }
                }
            }
        }

        public bool isFilled()
        {
            if (string.IsNullOrEmpty(_ddEntAE.SelectedEntity.Value))
                return false;
            if (string.IsNullOrEmpty(_ddEntEloquentBrainIO.SelectedEntity.Value))
                return false;
            if (string.IsNullOrEmpty(_lesionAE.SelectedEntity.Value))
                return false;
            foreach (Control ctrl in this.Controls)
            {
                DropDownEntity ddEnt = ctrl as DropDownEntity;
                if (ddEnt == null || string.Equals((string)ddEnt.Tag, "ImagingObservation") || string.IsNullOrEmpty(ddEnt.SelectedEntity.Value))
                    return false;
            }
            return true;
        }

        protected bool IsValueInCollection(List<KeyValuePair<string, string>> list, KeyValuePair<string, string> kvp)
        {
            return CollectionUtils.Contains(list, delegate(KeyValuePair<string, string> io)
                                                      { return io.Key == kvp.Key && io.Value == kvp.Value; });
        }
    }

    internal class VasariTemplateData
    {
        public static List<KeyValuePair<string, List<KeyValuePair<string, string>>>> ImagingObservationCharData
        {
            get
            {
                List<KeyValuePair<string, List<KeyValuePair<string, string>>>> theData = new List<KeyValuePair<string, List<KeyValuePair<string, string>>>>();
                List<KeyValuePair<string, string>> items;

                // Ependymal Invasion
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("94", "ependymal invasion absent"));
                items.Add(new KeyValuePair<string, string>("93", "ependymal invasion present"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Ependymal Invasion", items));

                // Definition of the Non-Enhancing Margin
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("71", "smooth non-enhancing margin"));
                items.Add(new KeyValuePair<string, string>("70", "non-enhancing margin not applicable"));
                items.Add(new KeyValuePair<string, string>("72", "irregular non-enhancing margin"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Definition of the Non-Enhancing Margin", items));

                // T1/FLAIR Ratio
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("92", "infiltrative T1/FLAIR ratio"));
                items.Add(new KeyValuePair<string, string>("90", "expansive T1/FLAIR ratio"));
                items.Add(new KeyValuePair<string, string>("95", "T1/FLAIR ratio not applicable"));
                items.Add(new KeyValuePair<string, string>("91", "mixed T1/FLAIR ratio"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("T1/FLAIR Ratio", items));

                // Proportion Enhancing
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("59", "greater than 95% enhancement"));
                items.Add(new KeyValuePair<string, string>("57", "34-67% enhancement"));
                items.Add(new KeyValuePair<string, string>("56", "6-33% enhancement"));
                items.Add(new KeyValuePair<string, string>("58", "68-95% enhancement"));
                items.Add(new KeyValuePair<string, string>("55", "less than 5% enhancement"));
                items.Add(new KeyValuePair<string, string>("54", "no enhancement"));
                items.Add(new KeyValuePair<string, string>("53", "enhancement indeterminate"));
                items.Add(new KeyValuePair<string, string>("60", "100% enhancement"));
                items.Add(new KeyValuePair<string, string>("52", "proportion of enhancement not applicable"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Proportion Enhancing", items));

                // Side of Tumor Epicenter
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("64", "center epicenter"));
                items.Add(new KeyValuePair<string, string>("66", "left epicenter"));
                items.Add(new KeyValuePair<string, string>("65", "right epicenter"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Side of Tumor Epicenter", items));

                // nCET Tumor Crosses Midline
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("61", "ncet tumor does cross midline"));
                items.Add(new KeyValuePair<string, string>("62", "ncet tumor does not cross midline"));
                items.Add(new KeyValuePair<string, string>("63", "ncet tumor crosses midline not applicable"));                
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("nCET Tumor Crosses Midline", items));

                // Enhancing Tumor Crosses Midline
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("3", "enhancing tumor crosses midline not applicable"));
                items.Add(new KeyValuePair<string, string>("1", "enhancing tumor does cross midline"));                
                items.Add(new KeyValuePair<string, string>("2", "enhancing tumor does not cross midline"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Enhancing Tumor Crosses Midline", items));

                // Proportion of Edema
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("34", "100% edema"));
                items.Add(new KeyValuePair<string, string>("32", "68-95% edema"));
                items.Add(new KeyValuePair<string, string>("28", "no edema"));
                items.Add(new KeyValuePair<string, string>("33", "greater than 95% edema"));
                items.Add(new KeyValuePair<string, string>("31", "34-67% edema"));
                items.Add(new KeyValuePair<string, string>("29", "less than 5% edema"));
                items.Add(new KeyValuePair<string, string>("27", "edema indeterminate"));
                items.Add(new KeyValuePair<string, string>("30", "6-33% edema"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Proportion of Edema", items));

                // Proportion Necrosis
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("49", "68-95% necrosis"));
                items.Add(new KeyValuePair<string, string>("48", "34-67% necrosis"));
                items.Add(new KeyValuePair<string, string>("43", "proportion of necrosis not applicable"));
                items.Add(new KeyValuePair<string, string>("46", "less than 5% necrosis"));
                items.Add(new KeyValuePair<string, string>("45", "no necrosis"));
                items.Add(new KeyValuePair<string, string>("44", "necrosis indeterminate"));
                items.Add(new KeyValuePair<string, string>("51", "100% necrosis"));
                items.Add(new KeyValuePair<string, string>("50", "greater than 95% necrosis"));
                items.Add(new KeyValuePair<string, string>("47", "6-33% necrosis"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Proportion Necrosis", items));

                // Proportion nCET
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("19", "no ncet"));
                items.Add(new KeyValuePair<string, string>("23", "68-95% ncet"));
                items.Add(new KeyValuePair<string, string>("24", "greater than 95% ncet"));
                items.Add(new KeyValuePair<string, string>("17", "proportion of ncet not applicable"));
                items.Add(new KeyValuePair<string, string>("20", "less than 5% ncet"));
                items.Add(new KeyValuePair<string, string>("25", "100% ncet"));
                items.Add(new KeyValuePair<string, string>("18", "ncet indeterminate"));
                items.Add(new KeyValuePair<string, string>("22", "34-67% ncet"));
                items.Add(new KeyValuePair<string, string>("21", "6-33% ncet"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Proportion nCET", items));

                // Definition Of Enhancing Margin
                
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("10", "poorly-defined enhancing margin"));
                items.Add(new KeyValuePair<string, string>("12", "well-defined enhancing margin"));
                items.Add(new KeyValuePair<string, string>("11", "enhancing margin definition not applicable"));                
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Definition of the Enhancing Margin", items));

                // Thickness of the Enhancing Margin
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("80", "no enhancing margin"));
                items.Add(new KeyValuePair<string, string>("81", "thin enhancing margin"));
                items.Add(new KeyValuePair<string, string>("82", "thick enhancing margin"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Thickness of the Enhancing Margin", items));

                // Multifocal or Multicentric
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("15", "multicentric"));
                items.Add(new KeyValuePair<string, string>("16", "multifocal"));
                items.Add(new KeyValuePair<string, string>("14", "gliomatosis"));
                items.Add(new KeyValuePair<string, string>("13", "morphology region not applicable"));                             
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Multifocal or Multicentric", items));

                // Enhancement Quality
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("69", "marked/avid enhancement"));
                items.Add(new KeyValuePair<string, string>("68", "mild/minimal enhancement"));
                items.Add(new KeyValuePair<string, string>("67", "no enhancement"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Enhancement Quality", items));

                // Deep WM Invasion
                items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("6", "deep wm invasion present"));
                items.Add(new KeyValuePair<string, string>("7", "deep wm invasion absent"));
                theData.Add(new KeyValuePair<string, List<KeyValuePair<string, string>>>("Deep WM Invasion", items));

                return theData;
            }
        }

        public static List<KeyValuePair<string, string>> AnatomicEntityData
        {
            get
            {
                List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("77", "occipital lobe"));
                items.Add(new KeyValuePair<string, string>("76", "parietal lobe"));
                items.Add(new KeyValuePair<string, string>("78", "cerebellum"));
                items.Add(new KeyValuePair<string, string>("79", "brain stem"));
                items.Add(new KeyValuePair<string, string>("74", "temporal lobe"));
                items.Add(new KeyValuePair<string, string>("73", "frontal lobe"));
                items.Add(new KeyValuePair<string, string>("75", "insula"));

                return items;
            }
        }

        public static List<KeyValuePair<string, string>> EloquentBrainInvolvementData
        {
            get
            {
                List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("87", "speech receptive center involvement"));
                items.Add(new KeyValuePair<string, string>("89", "vision center involvement"));
                items.Add(new KeyValuePair<string, string>("88", "motor center involvement"));
                items.Add(new KeyValuePair<string, string>("86", "speech motor center involvement"));
                items.Add(new KeyValuePair<string, string>("85", "no eloquent brain involvement"));

                return items;
            }
        }

        public static List<KeyValuePair<string, string>> LesionData
        {
            get
            {
                List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
                items.Add(new KeyValuePair<string, string>("84", "tumor"));
                return items;
            }
        }
    }
}
