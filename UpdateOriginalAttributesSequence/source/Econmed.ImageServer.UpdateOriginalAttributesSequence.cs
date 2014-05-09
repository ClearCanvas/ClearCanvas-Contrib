/*
Copyright (c) 2010, econmed GmbH
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in
      the documentation and/or other materials provided with the
      distribution.
    * Neither the name of the <ORGANIZATION> nor the names of its
      contributors may be used to endorse or promote products derived
      from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using ClearCanvas.Common;
using ClearCanvas.Dicom;
using ClearCanvas.ImageServer.Common.Helpers;
using ClearCanvas.ImageServer.Core.Edit;

[assembly: Plugin()]
[assembly: AssemblyTitle("Econmed.ImageServer.UpdateOriginalAttributesSequence")]
[assembly: AssemblyDescription("Plugin that updates the OriginalAttributesSequence after a study got edited.")]
[assembly: AssemblyCompany("econmed GmbH")]
[assembly: AssemblyProduct("econmed Image Server plugin")]
[assembly: AssemblyCopyright("Copyright (c) 2010")]

namespace Econmed.ImageServer.UpdateOriginalAttributesSequence
{
    [ExtensionOf(typeof(WebEditStudyProcessorExtensionPoint))]
    public class UpdateOriginalAttributesSequence : IWebEditStudyProcessorExtension
    {
        public UpdateOriginalAttributesSequence() { }

        #region IWebEditStudyProcessorExtension Member

        public bool Enabled
        {
            get { return true; }
        }

        public void Initialize() { }

        public void OnStudyEditing(WebEditStudyContext context)
        {
            Platform.Log(LogLevel.Info, "Updating OriginalAttributesSequence...");
            List<DicomAttribute> values = new List<DicomAttribute>();
            foreach (BaseImageLevelUpdateCommand c in context.EditCommands)
            {
                if (!(c is SetTagCommand)) { continue; }
                DicomAttribute value = c.UpdateEntry.TagPath.Tag.CreateDicomAttribute();
                value.SetStringValue(c.UpdateEntry.OriginalValue);
                values.Add(value);
            }
            int cut = context.Reason.IndexOf("::");            
            context.EditCommands.Insert(0,
                new AddOriginalAttributes(CreateOriginalAttributesSequence(values, null, null,
                    context.Reason.Substring(0, cut > DicomVr.CSvr.MaximumLength || cut < 0 ? Math.Min((int)DicomVr.CSvr.MaximumLength, context.Reason.Length) : cut))));
        }

        public void OnStudyEdited(WebEditStudyContext context) { }

        #endregion

        #region IDisposable Member

        public void Dispose() { }

        #endregion

        public static DicomSequenceItem CreateOriginalAttributesSequence(IEnumerable<DicomAttribute> originalValues,
            string originalAttributesSource, string modifyingSystem, string reasonForModification)
        {
            DicomSequenceItem item = new DicomSequenceItem();
            item[DicomTags.AttributeModificationDatetime]
                .SetDateTime(0, DateTime.Now);
            if (!string.IsNullOrEmpty(originalAttributesSource))
            {
                item[DicomTags.SourceOfPreviousValues]
                    .SetStringValue(originalAttributesSource);
            }
            if (!string.IsNullOrEmpty(modifyingSystem))
            {
                item[DicomTags.ModifyingSystem]
                    .SetStringValue(modifyingSystem);
            }
            if (!string.IsNullOrEmpty(reasonForModification))
            {
                item[DicomTags.ReasonForTheAttributeModification]
                    .SetStringValue(reasonForModification);
            }
            DicomAttributeSQ sq =
                new DicomAttributeSQ(DicomTags.ModifiedAttributesSequence);
            item[sq.Tag] = sq;

            DicomSequenceItem modified = new DicomSequenceItem();
            sq.AddSequenceItem(modified);

            foreach (DicomAttribute value in originalValues)
            {
                modified[value.Tag] = value;
            }
            return item;
        }
    }


    public class AddOriginalAttributes : BaseImageLevelUpdateCommand
    {
        DicomSequenceItem value = null;

        public AddOriginalAttributes()
        {
            UpdateEntry =
                new ImageLevelUpdateEntry()
                {
                    TagPath =
                        new DicomTagPath()
                        {
                            Tag = DicomTagDictionary.GetDicomTag(DicomTags.OriginalAttributesSequence)
                        },
                    Value = "N/A",
                    OriginalValue = "N/A"
                };
        }

        public AddOriginalAttributes(DicomSequenceItem value)
            : this()
        {
            this.value = value;
        }

        public override bool Apply(DicomFile file)
        {
            if (null != value)
            {
                file.DataSet[DicomTags.OriginalAttributesSequence].AddSequenceItem(value);
            }
            return true;
        }
    }
}
