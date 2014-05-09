using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	internal class XmlSettingsFile
	{
		#region Static Helpers

		private static readonly Dictionary<Type, string> __localSettingsFilenames = new Dictionary<Type, string>();
		private static readonly Dictionary<Type, string> __roamingSettingsFilenames = new Dictionary<Type, string>();

		public static XmlSettingsFile GetLocalSettingsFile(Type settingsClass)
		{
			return new XmlSettingsFile(GetLocalFilename(settingsClass));
		}

		public static XmlSettingsFile GetRoamingSettingsFile(Type settingsClass)
		{
			return new XmlSettingsFile(GetRoamingFilename(settingsClass));
		}

		private static string GetLocalFilename(Type settingsType)
		{
			if (!__localSettingsFilenames.ContainsKey(settingsType))
				__localSettingsFilenames.Add(settingsType, __GetAppSettingsFilename(settingsType, false));
			return __localSettingsFilenames[settingsType];
		}

		private static string GetRoamingFilename(Type settingsType)
		{
			if (!__roamingSettingsFilenames.ContainsKey(settingsType))
				__roamingSettingsFilenames.Add(settingsType, __GetAppSettingsFilename(settingsType, true));
			return __roamingSettingsFilenames[settingsType];
		}

		private static string __GetAppSettingsFilename(Type settingsType, bool roaming)
		{
			string appDataPath;
			if (roaming)
				appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			else
				appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			string assemblyComp = "";
			AssemblyCompanyAttribute attribComp = GetCustomAttribute<AssemblyCompanyAttribute>(settingsType.Assembly);
			if (attribComp != null && attribComp.Company != null)
				assemblyComp = attribComp.Company;

			string assemblyTitle = new FileInfo(settingsType.Assembly.Location).Name;

			string assemblyVer = "default";
			//Version version = settingsType.Assembly.GetName().Version;
			//if (version != null)
			//    assemblyVer = version.ToString();

			string path = Path.Combine(Path.Combine(Path.Combine(appDataPath, assemblyComp), assemblyTitle), assemblyVer);
			foreach (char badchar in Path.GetInvalidPathChars())
			{
				path = path.Replace(badchar.ToString(), "");
			}
			return Path.Combine(path, "assembly.config");
		}

		private static T GetCustomAttribute<T>(Assembly assembly) where T : class
		{
			foreach (object attrib in assembly.GetCustomAttributes(typeof (T), false))
			{
				return (T) attrib;
			}
			return null;
		}

		#endregion

		private const string ROOT_NODE = "configuration";

		private readonly string _filename;
		private readonly XmlDocument SettingsXML;

		private XmlSettingsFile(string filename)
		{
			_filename = filename;

			XmlDocument m_SettingsXML = new XmlDocument();

			try
			{
				m_SettingsXML.Load(filename);
			}
			catch (Exception)
			{
				XmlDeclaration dec = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
				m_SettingsXML.AppendChild(dec);

				XmlNode nodeRoot;

				nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, ROOT_NODE, "");
				m_SettingsXML.AppendChild(nodeRoot);
			}

			SettingsXML = m_SettingsXML;
		}

		public void Save()
		{
			try
			{
				Directory.CreateDirectory(new FileInfo(_filename).DirectoryName);
				SettingsXML.Save(_filename);
			}
			catch (Exception) {}
		}

		public string GetValue(SettingsProperty setting)
		{
			string ret = "";

			try
			{
				ret = SettingsXML.SelectSingleNode(ROOT_NODE + "/" + setting.Name).InnerText;
			}
			catch (Exception)
			{
				if ((setting.DefaultValue != null))
				{
					ret = setting.DefaultValue.ToString();
				}
				else
				{
					ret = "";
				}
			}

			return ret;
		}

		public void SetValue(SettingsPropertyValue propVal)
		{
			XmlElement SettingNode;

			try
			{
				SettingNode = (XmlElement) SettingsXML.SelectSingleNode(ROOT_NODE + "/" + propVal.Name);
			}
			catch (Exception)
			{
				SettingNode = null;
			}

			if ((SettingNode != null))
			{
				SettingNode.InnerText = propVal.SerializedValue.ToString();
			}
			else
			{
				SettingNode = SettingsXML.CreateElement(propVal.Name);
				SettingNode.InnerText = propVal.SerializedValue.ToString();
				SettingsXML.SelectSingleNode(ROOT_NODE).AppendChild(SettingNode);
			}
		}
	}
}