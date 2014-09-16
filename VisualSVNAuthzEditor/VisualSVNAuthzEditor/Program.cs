using System;
using System.Windows.Forms;
using VisualSVNAuthzEditor.Properties;

namespace VisualSVNAuthzEditor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if(Settings.Default.IsFirstRun)
			{
				Settings.Default.Upgrade();
				Settings.Default.IsFirstRun = false;
				Settings.Default.Save();
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
		}
	}
}
