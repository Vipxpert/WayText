using System.Diagnostics;

namespace Emoji
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (Process.GetProcessesByName("CopyToolGUI").Length > 1)
            {
                MessageBox.Show("There's another instance of CopyTool running on this computer. Close that one first then try again \n\n¯\\_(ツ)_/¯", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //closing = true;
                Application.Exit();
            }
            else {
                bool startedAtBoot = false;
                // Check if the /startup command-line argument is present
                string[] commandLineArgs = Environment.GetCommandLineArgs();
                if (Array.Exists(commandLineArgs, arg => arg.Equals("/startup", StringComparison.OrdinalIgnoreCase)))
                {
                    startedAtBoot = true;
                }
                if (startedAtBoot)
                {
                    // Your application is starting at boot, adjust behavior or set flags here
                    Application.Run(new Form1(startedAtBoot));
                }
                else
                {
                    // Your application is starting manually
                    Application.Run(new Form1(startedAtBoot));
                }
            }
        }
    }
}





