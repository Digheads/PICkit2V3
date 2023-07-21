using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	internal static class Program
	{
        [STAThread]
        private static void Main(string[] args)
		{
            string filename = "";

            // this instance is a root process instance, windows shell is the only parent
            if (args.Length == 3 && args[2] == "-fromShell")
                filename = Path.Combine(args[0], args[1]);

            // checking whether started as MPLAB X IDE's child process
            if(args.Length == 2)
            {
                var startInfo = new ProcessStartInfo(Application.ExecutablePath, "\"" +args[0] + "\" \"" + args[1] + "\" -fromShell")
                {
                    UseShellExecute = true
                };
                Process.Start(startInfo);
                return;
            }

            using (Mutex m = new Mutex(false, "PICkit2V3_Mutex", out bool isNew))
            {
                if (isNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormPICkit2(filename));
                }
                else
                {
                    var pipeClient = new NamedPipeClientStream(".", "PICkit2V3_Pipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
                    pipeClient.Connect();

                    var ss = new StreamString(pipeClient);
                    if (ss.ReadString() == "<RC%C6=?z76ek>*5")
                    {
                        ss.WriteString(filename);
                    }

                    pipeClient.Close();
                }
            }
        }
	}
}
