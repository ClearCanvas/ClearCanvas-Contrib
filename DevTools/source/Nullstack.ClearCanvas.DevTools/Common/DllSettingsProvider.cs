using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	/// <summary>
	/// A custom <see cref="SettingsProvider"/> to write to the user's Application Data path but with a path
	/// that is more easily computed for use by external apps (like installers).
	/// </summary>
	/// <remarks>
	/// Concept and some code adapted from http://www.codeproject.com/KB/vb/CustomSettingsProvider.aspx.
	/// </remarks>
	/// <example>
	/// <code lang="cs">
	/// [System.Configuration.SettingsProvider(typeof(Nullstack.ClearCanvas.DevTools.Config.DllSettingsProvider))]
	/// internal sealed partial class ApplicationSettings1 {
	///     ....
	/// }
	/// </code>
	/// </example>
	internal class DllSettingsProvider : SettingsProvider
	{
		private string _appName = "Settings";

		public override void Initialize(string name, NameValueCollection col)
		{
			base.Initialize(this.ApplicationName, col);
		}

		public override string ApplicationName
		{
			get { return _appName; }
			set { _appName = value; }
		}

		private Type GetTypeFromContext(SettingsContext context)
		{
			return (context["SettingsClassType"] as Type) ?? this.GetType();
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
		{
			XmlSettingsFile localFile = XmlSettingsFile.GetLocalSettingsFile(GetTypeFromContext(context));
			XmlSettingsFile roamingFile = XmlSettingsFile.GetRoamingSettingsFile(GetTypeFromContext(context));

			foreach (SettingsPropertyValue propval in propvals)
			{
				if (IsRoaming(propval.Property))
				{
					roamingFile.SetValue(propval);
				}
				else
				{
					localFile.SetValue(propval);
				}
			}

			localFile.Save();
			roamingFile.Save();
		}

		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
		{
			XmlSettingsFile localFile = XmlSettingsFile.GetLocalSettingsFile(GetTypeFromContext(context));
			XmlSettingsFile roamingFile = XmlSettingsFile.GetRoamingSettingsFile(GetTypeFromContext(context));
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

			foreach (SettingsProperty setting in props)
			{
				SettingsPropertyValue value = new SettingsPropertyValue(setting);
				value.IsDirty = false;

				if (IsRoaming(setting))
				{
					value.SerializedValue = roamingFile.GetValue(setting);
				}
				else
				{
					value.SerializedValue = localFile.GetValue(setting);
				}

				values.Add(value);
			}

			return values;
		}

		private static bool IsRoaming(SettingsProperty prop)
		{
			foreach (DictionaryEntry d in prop.Attributes)
			{
				Attribute a = (Attribute) d.Value;
				if (a is SettingsManageabilityAttribute)
				{
					return true;
				}
			}
			return false;
		}
	}
}