using System;
using System.Configuration;
using System.Reflection;
using System.Xml;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;

namespace Nullstack.ClearCanvas.DevTools
{
	/// <summary>
	/// A tool for deleting the action model
	/// </summary>
	[GroupHint("flush", "Nullstack.ClearCanvas.DevTools.ActionModel.Delete")]
	[MenuAction("flush", "global-menus/mnDebug/mnActionModels/mnDelete", "Flush")]
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class ActionModelTool : Tool<IDesktopToolContext>
	{
		private static FieldInfo field;
		private static object instance;
		private static string defaultValue;

		public override void Initialize()
		{
			base.Initialize();

			// initialization involves finding the internal class ActionModelSettings by reflection and recording the information we find

			Assembly assembly = Assembly.GetAssembly(typeof (Application));
			Type type = assembly.GetType("ClearCanvas.Desktop.Actions.ActionModelSettings");
			PropertyInfo propDefault = type.GetProperty("Default");
			PropertyInfo propActionModelXml = type.GetProperty("ActionModelsXml");

			// do discovery of default value tagged to the property
			DefaultSettingValueAttribute[] attribs = (DefaultSettingValueAttribute[]) propActionModelXml.GetCustomAttributes(typeof (DefaultSettingValueAttribute), false);
			if (attribs.Length > 0)
			{
				defaultValue = attribs[0].Value;
			}
			else
			{
				defaultValue = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<action-models />";
			}

			// do discovery of the default settings instance
			instance = propDefault.GetValue(null, null);

			field = type.GetField("_actionModelXmlDoc", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		/// <summary>
		/// Clears the action model.
		/// </summary>
		public void Flush()
		{
			if (Application.ShowMessageBox(SR.msgConfirmDeleteActionModel, MessageBoxActions.YesNo) == DialogBoxAction.No)
				return;

			XmlDocument newmodel = new XmlDocument();
			newmodel.LoadXml(defaultValue);
			field.SetValue(instance, newmodel);

			Application.ShowMessageBox(SR.msgActionModelDeleted, MessageBoxActions.Ok);
		}
	}
}