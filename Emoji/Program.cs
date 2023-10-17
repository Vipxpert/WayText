using System.Diagnostics;
using System.Threading;

namespace Emoji
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        static Mutex mutex = new Mutex(true, "CopyPasteProject");

        [STAThread]

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            /*if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                // Another instance is running; close the new instance
                Process currentProcess = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
                {
                    if (process.Id != currentProcess.Id)
                    {
                        process.CloseMainWindow();
                        process.WaitForExit();
                        break;
                    }
                }
            }*/


            Application.Run(new Form1());
        }
    }
}





