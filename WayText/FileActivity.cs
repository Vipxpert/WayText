namespace CopyToolGUI
{
    public  class FileActivity
    {
        //Copy folder's contents to a folder
        public static void CopyFolderContents(string sourceFolder, string destinationFolder)
        {
            // Check if the source folder exists
            if (!Directory.Exists(sourceFolder))
            {
                MessageBox.Show("Source folder does not exist: " + sourceFolder);
                return;
            }
            // Create destination if not exist
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
                //MessageBox.Show("Destination folder created: " + destinationFolder);
            }
            string[] files = Directory.GetFiles(sourceFolder); // Get a list of files in the source folder
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file); // Get the file name
                string destinationFilePath = Path.Combine(destinationFolder, fileName); // Combine the file name with the destination folder path
                File.Copy(file, destinationFilePath, true); // The 'true' parameter overwrites existing files
            }
            string[] subdirectories = Directory.GetDirectories(sourceFolder); // Get a list of subdirectories in the source folder
            foreach (string subdirectory in subdirectories)
            {
                string subdirectoryName = Path.GetFileName(subdirectory); // Get the subdirectory name
                string destinationSubdirectoryPath = Path.Combine(destinationFolder, subdirectoryName); // Combine the subdirectory name with the destination folder path
                CopyFolderContents(subdirectory, destinationSubdirectoryPath); // Recursively copy the contents of the subdirectory to the destination folder
            }
            MessageBox.Show("Copied from " + sourceFolder + " to " + destinationFolder);
        }

        //
        public static void CopyFileToFolder(string sourceFilePath, string destinationFolderPath)
        {
            // Check if the source file exists
            if (!File.Exists(sourceFilePath))
            {
                MessageBox.Show("Source file does not exist: " + sourceFilePath);
                return;
            }
            // Check if the destination folder exists, and create it if not
            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
                //MessageBox.Show("Destination folder created: " + destinationFolderPath);
            }
            string fileName = Path.GetFileName(sourceFilePath); // Get the file name from the source file path
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName); // Combine the file name with the destination folder path
            File.Copy(sourceFilePath, destinationFilePath, true); // The 'true' parameter overwrites an existing file with the same name
            MessageBox.Show("File copied to: " + destinationFilePath);
        }
    }
}
