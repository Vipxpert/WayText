using Microsoft.Win32;


namespace CopyToolGUI
{
    public class AppStartup
    {
        public static void AddApplicationToStartup(string appName, string appPath)
        {
            // Check if the application is already in startup
            if (!IsApplicationInStartup(appName, appPath))
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                key.SetValue(appName, appPath + " /startup"); // Add /startup argument to indicate boot launch
                key.Close();

                //MessageBox.Show("Your application has been added to startup.");
            }
            else
            {
                //MessageBox.Show("Your application is already in startup.");
            }
        }

        public static bool IsApplicationInStartup(string appName, string appPath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (key != null)
            {
                string registryValue = (string)key.GetValue(appName);
                key.Close();

                return registryValue != null && registryValue.StartsWith(appPath, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public static void RemoveApplicationFromStartup(string appName, string appPath)
        {
            if (IsApplicationInStartup(appName, appPath))
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (key != null)
                {
                    key.DeleteValue(appName, false);
                    key.Close();

                    //MessageBox.Show("Your application has been removed from startup.");
                }
            }
            else
            {
                //MessageBox.Show("Your application was not found in startup.");
            }
        }
    }
}
