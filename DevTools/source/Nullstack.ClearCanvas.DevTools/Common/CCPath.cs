#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
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
using System.Reflection;
using ClearCanvas.Common.Utilities;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	/// <summary>
	/// Represents a path.
	/// </summary>
	/// <remarks>
	/// Instances of this class are immutable.
	/// <para>This is actually a copy of the <see cref="ClearCanvas.Desktop.Path"/> class due to minor changes between this version and the one in the 1.2 SDK.</para>
	/// </remarks>
	internal class CCPath
	{
		/// <summary>
		/// Gets the empty <see cref="CCPath"/> object.
		/// </summary>
		public static CCPath Empty = new CCPath(new PathSegment[] {});

		private const string SEPARATOR = "/";
		private const string ESCAPED_SEPARATOR = "//";
		private const string TEMP = "__$:$__";

		private readonly List<PathSegment> _segments;

		/// <summary>
		/// Creates a new <see cref="CCPath"/> from the specified path string, resolving
		/// resource keys in the path string using the specified <see cref="ResourceResolver"/>.
		/// </summary>
		/// <remarks>
		/// The path string may contain any combination of literals and resource keys.  Localization
		/// will be attempted on each path segment by treating the segment as a resource key,
		/// and path segments that fail as resource keys will be treated as literals.
		/// </remarks>
		/// <param name="pathString">The path string to parse.</param>
		/// <param name="resolver">The <see cref="IResourceResolver"/> to use for localization.</param>
		public CCPath(string pathString, IResourceResolver resolver)
			: this(ParsePathString(pathString, resolver)) {}

		/// <summary>
		/// Creates a new <see cref="CCPath"/> from the specified path string, with no resource resolver.
		/// </summary>
		/// <remarks>
		/// The path string must only contain literals, because there is no resource resolver to perform
		/// localization.
		/// </remarks>
		/// <param name="pathString"></param>
		public CCPath(string pathString)
			: this(ParsePathString(pathString, new ResourceResolver(new Assembly[] {}))) {}

		/// <summary>
		/// Internal constructor.
		/// </summary>
		/// <param name="segments"></param>
		private CCPath(IEnumerable<PathSegment> segments)
		{
			_segments = new List<PathSegment>(segments);
		}

		/// <summary>
		/// Gets the individual segments contained in this path.
		/// </summary>
		public IList<PathSegment> Segments
		{
			get { return _segments.AsReadOnly(); }
		}

		/// <summary>
		/// The final segment in this path, or null if this path is empty.
		/// </summary>
		public PathSegment LastSegment
		{
			get { return CollectionUtils.LastElement(_segments); }
		}

		/// <summary>
		/// Gets a new <see cref="CCPath"/> object representing the specified sub-path.
		/// </summary>
		public CCPath SubPath(int start, int count)
		{
			return new CCPath(_segments.GetRange(start, count));
		}

		/// <summary>
		/// Gets the full path string, localized.
		/// </summary>
		public string LocalizedPath
		{
			get
			{
				return StringUtilities.Combine(_segments, SEPARATOR,
				                               delegate(PathSegment s) { return s.LocalizedText.Replace(SEPARATOR, ESCAPED_SEPARATOR); }, false);
			}
		}

		/// <summary>
		/// Returns a new <see cref="CCPath"/> object obtained by appending <paramref name="other"/> path
		/// to this path.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public CCPath Append(CCPath other)
		{
			List<PathSegment> combined = new List<PathSegment>(_segments);
			combined.AddRange(other.Segments);

			return new CCPath(combined);
		}

		/// <summary>
		/// Converts this path back to a string.
		/// </summary>
		public override string ToString()
		{
			return StringUtilities.Combine(_segments, SEPARATOR,
			                               delegate(PathSegment s) { return s.ResourceKey.Replace(SEPARATOR, ESCAPED_SEPARATOR); }, false);
		}

		/// <summary>
		/// Returns a new <see cref="CCPath"/> object representing the longest common path
		/// between this object and <paramref name="other"/>.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public CCPath GetCommonPath(CCPath other)
		{
			List<PathSegment> commonPath = new List<PathSegment>();
			for (int i = 0; i < Math.Min(_segments.Count, other.Segments.Count); i++)
			{
				if (_segments[i] == other.Segments[i])
					commonPath.Add(_segments[i]);
				else
					break; // must break as soon as paths differ
			}

			return new CCPath(commonPath);
		}

		/// <summary>
		/// Returns true if this path starts with <paramref name="other"/>.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool StartsWith(CCPath other)
		{
			// if other path is longer, then this path can't possibly "start with" it
			if (other.Segments.Count > _segments.Count)
				return false;

			// check that segments are equal up to length of other path
			for (int i = 0; i < other.Segments.Count; i++)
			{
				if (!Equals(_segments[i], other.Segments[i]))
					return false;
			}
			return true;
		}

		private static PathSegment[] ParsePathString(string pathString, IResourceResolver resolver)
		{
			// replace any escaped separators with some weird temporary string
			pathString = StringUtilities.EmptyIfNull(pathString).Replace(ESCAPED_SEPARATOR, TEMP);

			// split string by separator
			string[] parts = pathString.Split(new string[] {SEPARATOR}, StringSplitOptions.None);
			int n = parts.Length;
			PathSegment[] segments = new PathSegment[n];
			for (int i = 0; i < n; i++)
			{
				// replace the temp string with the unescaped separator
				parts[i] = parts[i].Replace(TEMP, SEPARATOR);
				segments[i] = new PathSegment(parts[i], resolver != null ? resolver.LocalizeString(parts[i]) : parts[i]);
			}
			return segments;
		}
	}

	/// <summary>
	/// Represents a single segment of a <see cref="CCPath"/>.
	/// </summary>
	/// <remarks>
	/// <para>This is actually a copy of the <see cref="ClearCanvas.Desktop.PathSegment"/> class due to minor changes between this version and the one in the 1.2 SDK.</para>
	/// </remarks>
	internal class PathSegment : IEquatable<PathSegment>
	{
		private readonly string _key;
		private readonly string _localized;

		/// <summary>
		/// Internal constructor.
		/// </summary>
		/// <param name="key">The resource key or unlocalized path segment string.</param>
		/// <param name="localized">The localized path segment string.</param>
		internal PathSegment(string key, string localized)
		{
			_key = key;
			_localized = localized;
		}

		/// <summary>
		/// Gets the resource key or unlocalized text.
		/// </summary>
		public string ResourceKey
		{
			get { return _key; }
		}

		/// <summary>
		/// Gets the localized text.
		/// </summary>
		public string LocalizedText
		{
			get { return _localized; }
		}

		///<summary>
		///</summary>
		///<param name="pathSegment1"></param>
		///<param name="pathSegment2"></param>
		///<returns></returns>
		public static bool operator !=(PathSegment pathSegment1, PathSegment pathSegment2)
		{
			return !Equals(pathSegment1, pathSegment2);
		}

		///<summary>
		///</summary>
		///<param name="pathSegment1"></param>
		///<param name="pathSegment2"></param>
		///<returns></returns>
		public static bool operator ==(PathSegment pathSegment1, PathSegment pathSegment2)
		{
			return Equals(pathSegment1, pathSegment2);
		}

		/// <summary>
		/// Gets whether or not <paramref name="pathSegment"/> is equal to this object.
		/// </summary>
		public bool Equals(PathSegment pathSegment)
		{
			if (pathSegment == null) return false;
			return Equals(_localized, pathSegment._localized);
		}

		/// <summary>
		/// Gets whether or not <paramref name="obj"/> is equal to this object.
		/// </summary>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as PathSegment);
		}

		/// <summary>
		/// Gets a hash code.
		/// </summary>
		public override int GetHashCode()
		{
			return _localized != null ? _localized.GetHashCode() : 0;
		}
	}
}