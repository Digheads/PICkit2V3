using System;
using System.IO;
using System.Windows.Forms;

namespace PICkit2V3
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string filename = "";
			if (args.Length == 2)
			{
				filename = Path.Combine(args[0], args[1]);
			}
            Application.Run(new FormPICkit2(filename));
        }
	}
}
