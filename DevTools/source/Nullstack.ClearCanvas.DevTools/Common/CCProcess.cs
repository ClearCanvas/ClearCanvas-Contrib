using System;
using System.Diagnostics;
using System.IO;
using ClearCanvas.Common;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	public class CCProcess
	{
		public static readonly CCProcess Desktop = new CCProcess(Process.GetCurrentProcess());
		public static readonly CCProcess Service;

		private readonly Process _process;
		private const string SERVICE_PROCESS_NAME = "ClearCanvas.Server.ShredHostService";

		private CCProcess(Process process)
		{
			_process = process;
		}

		static CCProcess()
		{
			// find the service process
			Process serviceProcess = null;
			CCPath pathCurDir = new CCPath(Environment.CurrentDirectory.Replace(Path.DirectorySeparatorChar, '/'));
			int score = 0;

			// if there are multiple shred host services, guess the one with more common path components
			foreach (Process process in Process.GetProcessesByName(SERVICE_PROCESS_NAME))
			{
				try
				{
					string processDir = Path.GetDirectoryName(process.MainModule.FileName);
					CCPath pathProcessDir = new CCPath(processDir.Replace(Path.DirectorySeparatorChar, '/'));
					int scoreProcessDir = pathCurDir.GetCommonPath(pathProcessDir).Segments.Count;
					if (scoreProcessDir > score)
					{
						score = scoreProcessDir;
						serviceProcess = process;
					}
				}
				catch (Exception) {}
			}

			if (serviceProcess != null)
			{
				Service = new CCProcess(serviceProcess);
			}
			else
			{
				Service = new CCProcess(Process.GetCurrentProcess());
				Platform.Log(LogLevel.Warn, "DevTools couldn't find the service process.");
			}
		}

		/// <summary>
		/// Gets the filename of the process' entry module.
		/// </summary>
		public string Filename
		{
			get { return _process.MainModule.FileName; }
		}

		/// <summary>
		/// Gets the process ID.
		/// </summary>
		public int PID
		{
			get { return _process.Id; }
		}

		/// <summary>
		/// Gets the working directory of the process.
		/// </summary>
		public string WorkingDirectory
		{
			get
			{
				if (_process.HasExited)
					return string.Empty;

				try
				{
					return Path.GetDirectoryName(_process.MainModule.FileName);
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
		}

		public override string ToString()
		{
			return _process.ProcessName;
		}

		public static implicit operator Process(CCProcess process)
		{
			return process._process;
		}
	}
}