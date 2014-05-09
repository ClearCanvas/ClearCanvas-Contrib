using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ClearCanvas.Common.Utilities;

namespace Tagging
{
    /// <summary>
    /// Rudimentary implementation of a database that stores study tags.
    /// </summary>
    public class TagDatabase
    {
        /// <summary>
        /// Holds tags associated with each study.
        /// </summary>
        private Dictionary<string, string[]> _studyTags;

        /// <summary>
        /// Holds the entire tag set, defining the order of the elements in the tag vectors.
        /// </summary>
        private string[] _tagSet;

        /// <summary>
        /// Holds the tag vector for each study, where the vectors contains a 1 or 0 for each tag, indicating if the study has that tag or not.
        /// </summary>
        private Dictionary<string, int[]> _studyTagVectors;

        /// <summary>
        /// Indicates whether the previously computed tag vectors are still valid.
        /// </summary>
        private bool _tagVectorsValid = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TagDatabase()
        {
            _studyTags = new Dictionary<string, string[]>();
            _studyTagVectors = new Dictionary<string, int[]>();

            Load();
        }


        #region Public API

        /// <summary>
        /// Gets the set of tags associated with the specified study.
        /// </summary>
        /// <param name="instanceUID"></param>
        /// <returns></returns>
        public string[] GetTagsForStudy(string instanceUID)
        {
            string[] tags = new string[0];
            _studyTags.TryGetValue(instanceUID, out tags);
            return tags;
        }

        /// <summary>
        /// Sets the tags associated with the specified study.
        /// </summary>
        /// <param name="instanceUID"></param>
        /// <param name="tags"></param>
        public void SetTagsForStudy(string instanceUID, string[] tags)
        {
            _studyTags[instanceUID] = tags;
            _tagVectorsValid = false;
            Save();
        }

        /// <summary>
        /// Searches for studies that are tagged with the specified tags.
        /// Each KeyValuePair result contains a study instance UID as the key, and a relevance score between 0 and 1.
        /// </summary>
        /// <param name="searchTags"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public KeyValuePair<string, double>[] FindStudies(string[] searchTags, int maxResults, double minScore)
        {
            // ensure the tag vectors are up to date
            if (!_tagVectorsValid)
                BuildStudyTagVectors();

            int[] refVector = GetTagVector(searchTags);
            return FindSimilar(refVector, maxResults, minScore);
        }


        /// <summary>
        /// Searches for studies that are tagged similarly to the specified study.
        /// Each KeyValuePair result contains a study instance UID as the key, and a relevance score between 0 and 1.
        /// </summary>
        /// <param name="instanceUID"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public KeyValuePair<string, double>[] FindSimilarStudies(string instanceUID, int maxResults, double minScore)
        {
            // ensure the tag vectors are up to date
            if (!_tagVectorsValid)
                BuildStudyTagVectors();

            // if the specified study does not have a tag vector, we can't find similar studies
            int[] refVector;
            if (!_studyTagVectors.TryGetValue(instanceUID, out refVector))
                return new KeyValuePair<string, double>[0];

            // add 1 to maxResults, because we then need to reject the reference study itself, which shows up as a result
            return CollectionUtils.Reject(FindSimilar(refVector, maxResults + 1, minScore),
                delegate(KeyValuePair<string, double> result) { return result.Key == instanceUID; }).ToArray();
        }

        #endregion

        /// <summary>
        /// Finds studies that are tagged similarly to the specified tag vector.
        /// </summary>
        /// <param name="tagVector"></param>
        /// <param name="maxResults"></param>
        /// <param name="minScore"></param>
        /// <returns></returns>
        private KeyValuePair<string, double>[] FindSimilar(int[] tagVector, int maxResults, double minScore)
        {
            // if tag vector is all zeros, it doesn't make sense to find similar vectors
            if (CollectionUtils.Max(tagVector, 0) == 0)
                return new KeyValuePair<string, double>[0];

            // compare to all other studies
            Dictionary<string, double> scores = new Dictionary<string, double>();
            foreach (KeyValuePair<string, int[]> kvp in _studyTagVectors)
            {
                scores.Add(kvp.Key, TanamotoScore(tagVector, kvp.Value));
            }

            // reject results that have less than the minimum score
            List<KeyValuePair<string, double>> results = CollectionUtils.Reject(scores,
                delegate(KeyValuePair<string, double> result)
                {
                    return result.Value < minScore;
                });

            // sort results such that closest matches appear first
            results = CollectionUtils.Sort(results,
                delegate(KeyValuePair<string, double> result1, KeyValuePair<string, double> result2)
                {
                    return -result1.Value.CompareTo(result2.Value);
                });

            // return at most maxResults
            return results.GetRange(0, Math.Min(results.Count, maxResults)).ToArray();
        }

        /// <summary>
        /// Builds the tag vectors for all studies.
        /// </summary>
        private void BuildStudyTagVectors()
        {
            // determine the unique tag set
            List<string> allTags = new List<string>();
            foreach (string[] tags in _studyTags.Values)
            {
                allTags.AddRange(tags);
            }

            _tagSet = CollectionUtils.Sort(CollectionUtils.Unique(allTags),
                delegate(string t1, string t2) { return t1.CompareTo(t2); }).ToArray();

            // build study tag vectors
            foreach (KeyValuePair<string, string[]> kvp in _studyTags)
            {
                string studyUid = kvp.Key;
                string[] studyTags = kvp.Value;

                _studyTagVectors[studyUid] = GetTagVector(studyTags);
            }

            _tagVectorsValid = true;
        }

        /// <summary>
        /// Creates a tag vector based on the specified tag subset and the overall <see cref="_tagSet"/>.
        /// </summary>
        /// <param name="tagSubSet"></param>
        /// <returns></returns>
        private int[] GetTagVector(string[] tagSubSet)
        {
            List<int> tagVector = new List<int>();
            foreach (string tag in _tagSet)
            {
                int score = CollectionUtils.Contains(tagSubSet, delegate(string t) { return t == tag; }) ? 1 : 0;
                tagVector.Add(score);
            }
            return tagVector.ToArray();
        }

        /// <summary>
        /// Computes the Tanamoto coefficient for the specified tag vectors.  This measures their similarity.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double TanamotoScore(int[] v1, int[] v2)
        {
            int n = v1.Length;
            double c1 = 0, c2 = 0, shr = 0;

            for (int i = 0; i < n; i++)
            {
                if (v1[i] > 0) c1 += 1;
                if (v2[i] > 0) c2 += 1;
                if (v1[i] > 0 && v2[i] > 0) shr += 1;
            }

            return shr / (c1 + c2 - shr);
        }

        /// <summary>
        /// Load the tag database from user settings.
        /// </summary>
        private void Load()
        {
            XmlDocument xmlDoc = TagDatabaseSettings.Default.DatabaseXml;
            if (xmlDoc != null)
            {
                foreach (XmlElement studyNode in xmlDoc.SelectNodes("studies/study"))
                {
                    string uid = studyNode.GetAttribute("uid");
                    string[] tags = CollectionUtils.Map<XmlElement, string>(studyNode.SelectNodes("tag"),
                        delegate(XmlElement e) { return e.GetAttribute("value"); }).ToArray();

                    _studyTags[uid] = tags;
                }
            }
        }

        /// <summary>
        /// Save the tag database to user settings.
        /// </summary>
        private void Save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement studiesNode = xmlDoc.CreateElement("studies");
            xmlDoc.AppendChild(studiesNode);

            foreach (KeyValuePair<string, string[]> kvp in _studyTags)
            {
                XmlElement studyNode = xmlDoc.CreateElement("study");
                studiesNode.AppendChild(studyNode);
                studyNode.SetAttribute("uid", kvp.Key);

                foreach (string tag in kvp.Value)
                {
                    XmlElement tagNode = xmlDoc.CreateElement("tag");
                    studyNode.AppendChild(tagNode);
                    tagNode.SetAttribute("value", tag);
                }
            }

            TagDatabaseSettings.Default.DatabaseXml = xmlDoc;
            TagDatabaseSettings.Default.Save();
        }
    }
}
