using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;

namespace Nullstack.ClearCanvas.DevTools.Threader {
	[ExtensionOf(typeof(DesktopToolExtensionPoint))]
	public class ThreadMonitorTool : Tool<IDesktopToolContext>
	{
		private volatile bool _running;

		public void DoIt()
		{
			
		}

		public void REallyDoIt()
		{
			StringBuilder sb = new StringBuilder();
			_running = true;
			while(_running)
			{
				foreach (object o in Process.GetCurrentProcess().Threads)
				{
					if(o is ProcessThread)
					{
						ProcessThread thread = (ProcessThread) o;
						sb.AppendFormat("ID: {0}, Priority: {1}", thread.Id, thread.PriorityLevel);
						sb.AppendLine();
					}
					else if(o is Thread)
					{
						Thread thread = (Thread)o;
						sb.AppendFormat("ID: {0}, Priority: {1}", thread.Name, thread.Priority);
						sb.AppendLine();
					}
				}

				Thread.Sleep(5);
			}
		}
	}
}
