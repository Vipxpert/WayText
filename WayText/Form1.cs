using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text.Json;
using CopyToolGUI;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CopyToolGUI.Actions;
using System.ComponentModel;
using WayTextGUI.Actions;

namespace Emoji
{
    public partial class Form1 : Form
    {
        private const int WM_HOTKEY = 0x0312; //Fixed don't change
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //Capture windows keyboard event
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                // Check if the hotkey was pressed
                int id = m.WParam.ToInt32();
                if (id == 1) // Adjust this to match the ID you used when registering the hotkey
                {
                    if (this.Visible)
                    {
                        this.Hide();
                        notiCopyTool.Visible = true;
                    }
                    else
                    {
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        notiCopyTool.Visible = false;
                        //notiCopyTool.ShowBalloonTip(2000);
                    }
                }

                int t, c;
                for (int i = 0; i < hotkeyChangeTypeCategory.Length; i++)
                {
                    if (id == i + 2)
                    {
                        t = Convert.ToInt32(hotkeyChangeTypeCategory[i][0]);
                        c = Convert.ToInt32(hotkeyChangeTypeCategory[i][1]);
                        CBTypes.SelectedIndex = t;
                        CBCategories.SelectedIndex = c;
                    }
                }
            }
            base.WndProc(ref m);
        }

        //Click on tray icon
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
                notiCopyTool.Visible = true;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notiCopyTool.Visible = false;
                notiCopyTool.ShowBalloonTip(2000);
            }
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
            System.Diagnostics.Process.Start(Application.ExecutablePath); // Start a new instance of the application
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Configㅤfields
        int i = 0, j = 0, numberOfColumn = 0, maxNumberOfColumnVisible = 5, formHeight = 371, startTypeIndex = 0, startCategoryIndex = 0, numberOfScroll = 0, scrollInterval = 500;
        string[] hotkey = { "0x0000", "0x70" }; //Modfier + virtual key (nothing + F1)
        //ctrl + 1, ctrl + 2
        string[][] hotkeyChangeTypeCategory = { new string[] { "3", "0", "0x0002", "0x31" }, new string[] { "4", "0", "0x0002", "0x32" } };
        bool actionInstantCopy, startOnBoot = true, startedAtBoot, hideOnBoot, showHint;
        bool[] headerGroupExpand; bool headerExpand = true; //Header expand feature
        string[][] excludedFolderFromGroup, includedFolderInGroup; //Specify files's names that are excluded from the program
        List<string> groupType; //Name groups
        //folderDelimiter will include only folders that includes the delimiter
        //excludeFolderDelimiter will exclude any folders that includes the delimiter
        string folderDelimiter = "", excludeFolderDelimiter = "exclude";
        string dataDelimiter = "\u3164"; //Delimiter for file reading
        string path = "current";

        List<string> folderNames = new List<string>(); //Store folders
        List<string> fileNames = new List<string>(); //Store all files in folders
        List<string[]> structure = new List<string[]>(); //Contain files in each folder's array element

        List<string> types = new List<string>(); //Store trimmed names of folders
        List<string> categories = new List<string>(); //Store trimmed names of files

        string originalText;
        string unoriginalText;
        bool changedProgrammatically = false;
        static Random random = new Random();

        private void LLData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\"))
                {
                    try
                    {
                        Process.Start("explorer.exe", path);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message + "Couldn't open with explorer.exe, trying files.exe instead");
                        Process.Start("files.exe", path);
                    }
                }
                else
                {
                    //MessageBox.Show("Folder does not exist: " + path);
                    DialogResult result = MessageBox.Show("Folder does not exist: " + path + "\nDo you want to create the default data folder?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // User clicked "Yes"
                        Directory.CreateDirectory(path);
                        //File.Copy("AppDomain.CurrentDomain.BaseDirectory", destinationFilePath)
                        FileActivity.CopyFolderContents(AppDomain.CurrentDomain.BaseDirectory + "DataSample\\", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\");
                    }
                    else
                    {

                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        public Form1(bool started)
        {
            startedAtBoot = started;
            this.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + "thinking-face.512x506.ico");
            //path = File.ReadAllText("DataPath.txt"); //Idk
            //path = path.Replace("current\\", AppDomain.CurrentDomain.BaseDirectory); //Current directory
            //path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WayText\\"; //Roaming
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\"; //Local
            string configFilePath = path + "AppConfig.json";
            if (File.Exists(configFilePath))
            {
                // Read the JSON content from the file
                string jsonContent = File.ReadAllText(configFilePath);

                // Deserialize the JSON content into the AppConfig class
                var config = JsonSerializer.Deserialize<AppConfig>(jsonContent);
                try
                {
                    showHint = config.showHint;
                    startOnBoot = config.startOnBoot;
                    hideOnBoot = config.hideOnBoot;
                    folderDelimiter = config.folderDelimiter;
                    excludeFolderDelimiter = config.excludeFolderDelimiter;
                    dataDelimiter = config.dataDelimiter;
                    maxNumberOfColumnVisible = config.maxNumberOfColumnVisible;
                    formHeight = config.formHeight;
                    excludedFolderFromGroup = config.excludedFolderFromGroup;
                    includedFolderInGroup = config.includedFolderInGroup;
                    groupType = config.groupType;
                    headerExpand = config.headerExpand;
                    headerGroupExpand = config.headerGroupExpand;
                    startTypeIndex = config.startTypeIndex;
                    actionInstantCopy = config.actionInstantCopy;
                    hotkey = config.hotkeyShowHideApp;
                    hotkeyChangeTypeCategory = config.hotkeyChangeTypeCategory;
                    numberOfScroll = config.numberOfScroll;
                    scrollInterval = config.scrollInterval;

                }
                catch
                {
                    MessageBox.Show("Something is wrong with the config.\nPlease restore the /Data/AppConfig.json file. Using default config");
                    this.Close();
                }


            }
            else
            {
                DialogResult result = MessageBox.Show(configFilePath + " not found" + "\nDo you want to create the default AppConfig.json?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    FileActivity.CopyFileToFolder(AppDomain.CurrentDomain.BaseDirectory + "DataSample\\AppConfig.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\");
                    //File.Copy("AppDomain.CurrentDomain.BaseDirectory", destinationFilePath)
                    string jsonContent = File.ReadAllText(configFilePath);
                    var config = JsonSerializer.Deserialize<AppConfig>(jsonContent);
                    startOnBoot = config.startOnBoot;
                    folderDelimiter = config.folderDelimiter;
                    excludeFolderDelimiter = config.excludeFolderDelimiter;
                    dataDelimiter = config.dataDelimiter;
                    maxNumberOfColumnVisible = config.maxNumberOfColumnVisible;
                    formHeight = config.formHeight;
                    excludedFolderFromGroup = config.excludedFolderFromGroup;
                    includedFolderInGroup = config.includedFolderInGroup;
                    groupType = config.groupType;
                    headerExpand = config.headerExpand;
                    headerGroupExpand = config.headerGroupExpand;
                    startTypeIndex = config.startTypeIndex;
                    startCategoryIndex = config.startCategoryIndex;
                    actionInstantCopy = config.actionInstantCopy;
                    hotkey = config.hotkeyShowHideApp;
                    hotkeyChangeTypeCategory = config.hotkeyChangeTypeCategory;
                }
                else
                {
                    MessageBox.Show("App cannot work without AppConfig.json");
                    Application.Exit();
                    this.Close();
                }
            }
            if (startOnBoot)
            {
                AppStartup.AddApplicationToStartup("WayText", AppDomain.CurrentDomain.BaseDirectory + "WayTextGUI.exe");
            }
            else
            {
                AppStartup.RemoveApplicationFromStartup("WayText", AppDomain.CurrentDomain.BaseDirectory + "WayTextGUI.exe");
            }




            InitializeComponent();
            scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = scrollInterval; // 0.5 seconds
            scrollTimer.Tick += ScrollTimer_Tick;
            DataPrepare();
        }
        private System.Windows.Forms.Timer scrollTimer;
        private bool scrollingLeft = false;
        private bool scrollingRight = false;

        //Read folders and text files data into folderNames, fileNames, structure
        public void DataPrepare()
        {
            foreach (string folder in Directory.GetDirectories(path).Select(Path.GetFileName).ToArray())
            {
                if (!folder.Contains(excludeFolderDelimiter))
                    folderNames.Add(folder);
            }

            //Filter folder delimiter
            if (!String.IsNullOrEmpty(folderDelimiter))
                for (i = 0; i < folderNames.Count; i++)
                {
                    //MessageBox.Show(folderNames[i]);
                    folderNames[i] = folderNames[i].Replace(folderDelimiter, null);
                }

            //Add arrays of files into the list
            foreach (string folder in folderNames)
            {
                structure.Add(Directory.GetFiles(path + folder + folderDelimiter + "\\", "*.txt").Select(Path.GetFileNameWithoutExtension).ToArray());
            }
            //Store all files in folders
            i = 0;
            foreach (string[] folder in structure)
            {
                foreach (string file in folder)
                {
                    fileNames.Add(file);
                }
            }
            foreach (string folder in folderNames)
            {
                types.Add(Trim.TrimCharacter(folder, "0123456789"));
            }
            for (i = 0; i < fileNames.Count; i++)
            {
                categories.Add(Trim.TrimCharacter(fileNames[i], "0123456789"));
            }

            changedProgrammatically = false;
            //this.ShowDialog();
        }

        //Preparing ToolTips
        System.Windows.Forms.ToolTip TTInput = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTParam = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTReplace = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTData = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTPanel = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTCopied = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTGit = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTManual = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTRevert = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTEmpty = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTAction = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTCBType = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTCBCategory = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTCBAction = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTNZalgo = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTNMinRandom = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTNMaxRandom = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTBTRightScroll = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip TTBTLeftScroll = new System.Windows.Forms.ToolTip();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Get cursor position
            Point cursorPos = Cursor.Position;
            int newX = cursorPos.X - (Width / 2);
            int newY = cursorPos.Y - (Height / 2);
            Rectangle virtualScreen = SystemInformation.VirtualScreen; //Get dimension
            //Form won't flow off the screen
            if (newX < virtualScreen.Left)
            {
                newX = virtualScreen.Left;
            }
            else if (newX + Width > virtualScreen.Right)
            {
                newX = virtualScreen.Right - Width;
            }
            if (newY < virtualScreen.Top)
            {
                newY = virtualScreen.Top;
            }
            else if (newY + Height > virtualScreen.Bottom)
            {
                newY = virtualScreen.Bottom - Height;
            }
            Location = new Point(newX, newY); //Locate form to mouse cursor
            dataGridView1.RowHeadersVisible = false;
            Text = string.Empty;
            ControlBox = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            if (showHint)
            {
                //TTInput.ToolTipTitle = "Hint";
                TTInput.ShowAlways = false;
                /*TTInput.AutoPopDelay = 0;
                TTInput.InitialDelay = 200;
                TTInput.ReshowDelay = 0;*/
                TTInput.AutoPopDelay = 0;
                TTInput.InitialDelay = 200;
                TTInput.ReshowDelay = 0;
                TTInput.SetToolTip(TBInput, "Output and input to be processed are here\nHover while typing to show contents");

                //TTParam.ToolTipTitle = "Hint";
                TTParam.ShowAlways = false;
                TTParam.AutoPopDelay = 0;
                TTParam.InitialDelay = 200;
                TTParam.ReshowDelay = 0;
                TTParam.SetToolTip(TBParam, "Parameters to process inputs with based on actions\nHover while typing to show contents");

                //Setting up ToolTips
                TTPanel.ToolTipTitle = "Hint";
                //TTPanel.UseFading = true;
                //TTPanel.UseAnimation = true;
                //TTPanel.IsBalloon = true;
                TTPanel.ShowAlways = false;
                TTPanel.AutoPopDelay = 3000;
                TTPanel.InitialDelay = 500;
                TTPanel.ReshowDelay = 5000;
                TTPanel.SetToolTip(panel1, "Right click to hide \nLeft click to drag");

                //TTInput.ToolTipTitle = "Hint";
                TTReplace.ShowAlways = false;
                TTReplace.AutoPopDelay = 0;
                TTReplace.InitialDelay = 200;
                TTReplace.ReshowDelay = 0;
                TTReplace.SetToolTip(TBReplace, "Replace which character?");

                TTRevert.ShowAlways = false;
                TTRevert.AutoPopDelay = 3000;
                TTRevert.InitialDelay = 200;
                TTRevert.ReshowDelay = 5000;
                TTRevert.SetToolTip(BTRevert, "Revert input to it's original state");

                TTEmpty.ToolTipTitle = "Hint";
                TTEmpty.ShowAlways = false;
                TTEmpty.AutoPopDelay = 3000;
                TTEmpty.InitialDelay = 200;
                TTEmpty.ReshowDelay = 5000;
                TTEmpty.SetToolTip(BTEmpty, "Left-click to empty the input text box\nRight-click to empty all text boxes");

                TTData.ToolTipTitle = "Hint";
                TTData.ShowAlways = false;
                TTData.AutoPopDelay = 3000;
                TTData.InitialDelay = 200;
                TTData.ReshowDelay = 5000;
                TTData.SetToolTip(LLData, "Left-click to open data folder\nRight-click to open app folder");

                TTManual.ToolTipTitle = "Hint";
                TTManual.ShowAlways = false;
                TTManual.AutoPopDelay = 3000;
                TTManual.InitialDelay = 200;
                TTManual.ReshowDelay = 5000;
                TTManual.SetToolTip(LLManual, "Open the README.md file");

                TTGit.ToolTipTitle = "Hint";
                TTGit.ShowAlways = false;
                TTGit.AutoPopDelay = 3000;
                TTGit.InitialDelay = 200;
                TTGit.ReshowDelay = 5000;
                TTGit.SetToolTip(LLGit, "Link to the source code!\nRight-click to copy");

                TTAction.ShowAlways = false;
                TTAction.AutoPopDelay = 3000;
                TTAction.InitialDelay = 200;
                TTAction.ReshowDelay = 5000;
                TTAction.SetToolTip(BTAction, "Go!");

                TTCBAction.ToolTipTitle = "Hint";
                TTCBAction.ShowAlways = false;
                TTCBAction.AutoPopDelay = 3000;
                TTCBAction.InitialDelay = 200;
                TTCBAction.ReshowDelay = 5000;
                TTCBAction.SetToolTip(CBTextAction, "Choose what to do with inputs");

                TTCBType.ToolTipTitle = "Hint";
                TTCBType.ShowAlways = false;
                TTCBType.AutoPopDelay = 3000;
                TTCBType.InitialDelay = 200;
                TTCBType.ReshowDelay = 5000;
                TTCBType.SetToolTip(CBTypes, "Change type");

                TTCBCategory.ToolTipTitle = "Hint";
                TTCBCategory.ShowAlways = false;
                TTCBCategory.AutoPopDelay = 3000;
                TTCBCategory.InitialDelay = 200;
                TTCBCategory.ReshowDelay = 5000;
                TTCBCategory.SetToolTip(CBCategories, "Change category");

                TTNZalgo.ToolTipTitle = "Hint";
                TTNZalgo.ShowAlways = false;
                TTNZalgo.AutoPopDelay = 3000;
                TTNZalgo.InitialDelay = 200;
                TTNZalgo.ReshowDelay = 5000;
                TTNZalgo.SetToolTip(ZalgoIntensity, "How crazy should the Zalgo effect be");

                TTNMinRandom.ToolTipTitle = "Hint";
                TTNMinRandom.ShowAlways = false;
                TTNMinRandom.AutoPopDelay = 3000;
                TTNMinRandom.InitialDelay = 200;
                TTNMinRandom.ReshowDelay = 5000;
                TTNMinRandom.SetToolTip(RegexMinRandomLength, "Minimum length of randomly generated text");

                TTNMaxRandom.ToolTipTitle = "Hint";
                TTNMaxRandom.ShowAlways = false;
                TTNMaxRandom.AutoPopDelay = 3000;
                TTNMaxRandom.InitialDelay = 200;
                TTNMaxRandom.ReshowDelay = 5000;
                TTNMaxRandom.SetToolTip(RegexMaxRandomLength, "Maximum length of randomly generated text");

                TTBTLeftScroll.ToolTipTitle = "Hint";
                TTBTLeftScroll.ShowAlways = false;
                TTBTLeftScroll.AutoPopDelay = 3000;
                TTBTLeftScroll.InitialDelay = 200;
                TTBTLeftScroll.ReshowDelay = 5000;
                TTBTLeftScroll.SetToolTip(BTLeftScroll, "Hover over or left-click to scroll left\nRight-click to jump to the beginning");

                TTBTRightScroll.ToolTipTitle = "Hint";
                TTBTRightScroll.ShowAlways = false;
                TTBTRightScroll.AutoPopDelay = 3000;
                TTBTRightScroll.InitialDelay = 200;
                TTBTRightScroll.ReshowDelay = 5000;
                TTBTRightScroll.SetToolTip(BTRightScoll, "Hover over or left-click to scroll right\nRight-click to jump to the last");
            }

            //Form drag events
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            this.panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
            this.panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            //dataGridView1.CellToolTipTextNeeded += dataGridView1_CellToolTipTextNeeded;
            //this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);

            // Register your hotkey when the form loads
            /*int modifier = int.Parse(hotkey[0], System.Globalization.NumberStyles.HexNumber);
            int key = int.Parse(hotkey[1], System.Globalization.NumberStyles.HexNumber);*/
            int modifier = Convert.ToInt32(hotkey[0].Substring(2), 16);
            int key = Convert.ToInt32(hotkey[1].Substring(2), 16);
            RegisterHotKey(this.Handle, 1, modifier, key); // Register the hotkey

            for (int i = 0; i < hotkeyChangeTypeCategory.Length; i++)
            {
                //MessageBox.Show(hotkeyChangeTypeCategory[i][2] + " " + hotkeyChangeTypeCategory[i][3]);
                modifier = Convert.ToInt32(hotkeyChangeTypeCategory[i][2].Substring(2), 16);
                key = Convert.ToInt32(hotkeyChangeTypeCategory[i][3].Substring(2), 16);
                RegisterHotKey(this.Handle, i + 2, modifier, key);
            }

            InitCBTypes(); //Initializing data into controllers

            if (startedAtBoot)
            {
                if (hideOnBoot)
                {
                    notiCopyTool.Visible = true;
                    this.Hide();
                }
            }


            if (numberOfColumn == 0)
            {
                DialogResult result = MessageBox.Show("There doesn't seem to be any data yet. Use the default data sample?\nThis's one time set-up. Or you can manually create it yourself if you know what you're doing.\nCheck README.md if you don't understand something.", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    FileActivity.CopyFolderContents(AppDomain.CurrentDomain.BaseDirectory + "DataSample\\", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\");
                    DataPrepare();
                    InitCBTypes();
                }
            }

            //Prepare string actions
            string[] textAction = { "Z̷̤̣̲ͮͤ͜ą̲̬̲̪̠ͮļ̞̯̺ͧ͞͡g̷̶̴̴̟ͥͫo̯̱ͪͭͩ͜͞f̡̢̤̤̞ͨ͜y̵̜̲̬̞̱͡","UnZalgofy", "s̶t̶r̶i̶k̶e̶t̶h̶r̶o̶u̶g̶h̶", "uʍop ǝpısd∩", "Downside up","ɿoɿɿiM","Unmirror", "esreveR",
                "raNDomly CApItAlizE", "TO UPPER CASE", "to lower case", "To Title Case", "Trim chrcter", "Trim string", "Trim duplcates","Replace",
                "Simple cipher", "Simple decipher", "Regex check", "Random text generator",
                "-- --- .-. ... .   -.-. --- -.. .   . -. -.-. .-. -.-- .--. -", "Morse code decrypt",
            "01000101 01101110 01100011 01110010 01111001 01110000 01110100 00100000 01100010 01101001 01101110 01100001 01110010 01111001", "Decrypt binary"};
            foreach (string action in textAction)
            {
                CBTextAction.Items.Add(action);
            }

            CBTextAction.SelectedIndex = 0;
            dataGridView1.Height += formHeight;
            TBInput.Top += formHeight / 2;
            TBParam.Top += formHeight / 2;
            TBReplace.Top += formHeight / 2;
            LLData.Top += formHeight;
            LLGit.Top += formHeight;
            LLManual.Top += formHeight;
            LBParamCount.Top += formHeight / 2;
            LBRandom.Top += formHeight / 2;
            LBAction.Top += formHeight / 2;
            CBTextAction.Top += formHeight / 2;
            BTAction.Top += formHeight / 2;
            BTRevert.Top += formHeight / 2;
            BTEmpty.Top += formHeight / 2;
            panel1.Height += formHeight;
            ZalgoIntensity.Top += formHeight / 2;
            label3.Top += formHeight / 2;
            RegexMaxRandomLength.Top += formHeight / 2;
            RegexMinRandomLength.Top += formHeight / 2;
            BTLeftScroll.Top += formHeight;
            BTRightScoll.Top += formHeight;

            TBInput.GotFocus += TBInput_GotFocus;
            TBInput.LostFocus += TBInput_LostFocus;

            TBParam.GotFocus += TBParam_GotFocus;
            TBParam.LostFocus += TBParam_LostFocus;

            TBReplace.GotFocus += TBReplace_GotFocus;
            TBReplace.LostFocus += TBReplace_LostFocus;

            ActiveControl = null;
            this.TopLevel = true;
            this.TopMost = true;
            //this.Focus();
            this.TopMost = true;

            //TBInput.Dock = DockStyle.Fill;
        }

        //When form closing, unregister hotkey
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 1);
        }

        //Right click to exit
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Hide(); // Hide the form
                notiCopyTool.Visible = true;
            }
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            TBInput.Size = new System.Drawing.Size(300, 36);
            TBInput.ScrollBars = ScrollBars.None;
            tbinputExpand = false;
            TBParam.Size = new System.Drawing.Size(300, 36);
            TBParam.ScrollBars = ScrollBars.None;
            tbparamExpand = false;
            TBReplace.Size = new System.Drawing.Size(300, 36);
            TBReplace.ScrollBars = ScrollBars.None;
            tbreplaceExpand = false;
            if (e.Button == MouseButtons.Right)
            {
                this.Hide(); // Hide the form
                notiCopyTool.Visible = true;
            }
        }

        //Left click to drag
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;  // _dragging is your variable flag
                _start_point = new Point(e.X, e.Y);
            }
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = false;
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _start_point = new Point(e.X, e.Y);
            }
        }

        void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = false;
            }
        }

        void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }



        //Controller initiallise process

        //First
        public void InitCBTypes()
        {
            CBTypes.Items.Clear();
            List<string> allTypes = new List<string>();
            foreach (string type in groupType)
            {
                allTypes.Add(type);
            }
            for (i = 0; i < folderNames.Count; i++)
            {
                allTypes.Add(folderNames[i]);
            }
            foreach (string type in allTypes)
            {
                CBTypes.Items.Add(Trim.TrimCharacter(type, "0123456789"));
            }
            try
            {
                CBTypes.SelectedIndex = startTypeIndex;
            }
            catch
            {
                CBTypes.SelectedIndex = 0;
            }

            if (CBTypes.SelectedIndex - 1 < 0)
            {
                InitCBCategories(CBTypes.SelectedItem.ToString());
            }
            else
            {
                if (CBTypes.SelectedIndex - groupType.Count < 0)
                {
                    InitCBCategories(CBTypes.SelectedItem.ToString());
                }
                else
                {
                    string pattern = @"\d+[a-zA-Z]+";
                    string input = folderNames[CBTypes.SelectedIndex - groupType.Count];

                    if (Regex.IsMatch(input, pattern))
                    {
                        InitCBCategories((CBTypes.SelectedIndex - groupType.Count + 1).ToString() + Trim.TrimCharacter(folderNames[CBTypes.SelectedIndex - groupType.Count], "0123456789"));
                    }
                    else
                    {
                        InitCBCategories(CBTypes.SelectedItem.ToString());
                    }
                }
            }
        }

        //Second
        private void CBType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (groupType.Contains(CBTypes.SelectedItem.ToString()) || CBTypes.SelectedItem.ToString() == "")
            {
                InitCBCategories(CBTypes.SelectedItem.ToString());
            }
            else
            {
                //InitCBCategories(folderNames[CBTypes.SelectedIndex - 1]);
                string pattern = @"(\d+)([A-Za-z]+)";
                string input = folderNames[CBTypes.SelectedIndex - groupType.Count];

                if (Regex.IsMatch(input, pattern))
                {
                    InitCBCategories((CBTypes.SelectedIndex + 1 - groupType.Count).ToString() + Trim.TrimCharacter(folderNames[CBTypes.SelectedIndex - groupType.Count], "0123456789"));
                }
                else
                {
                    InitCBCategories(CBTypes.SelectedItem.ToString());
                }
            }
        }

        //Third
        public void InitCBCategories(string type)
        {
            CBCategories.Items.Clear();

            List<string> allCategories = new List<string>();

            if (groupType.Contains(type))
            {
                allCategories.Add("");
                foreach (string category in allCategories)
                {
                    CBCategories.Items.Add(Trim.TrimCharacter(category, "0123456789"));
                }
                try
                {
                    CBCategories.SelectedIndex = startCategoryIndex;
                }
                catch
                {
                    CBCategories.SelectedIndex = 0;
                }
            }
            else
            {
                allCategories.Add("All");
                int categories = folderNames.IndexOf(type);
                foreach (string category in structure[categories])
                {
                    allCategories.Add(Trim.TrimCharacter(category, "0123456789"));
                }
                foreach (string category in allCategories)
                {
                    CBCategories.Items.Add(Trim.TrimCharacter(category, "0123456789"));
                }
                try
                {
                    CBCategories.SelectedIndex = startCategoryIndex;
                }
                catch
                {
                    CBCategories.SelectedIndex = 0;
                }
            }
        }
        //Forth
        private void CBCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            InjectCBDatagridview();
        }
        //Fifth
        //Pass Combobox's values into Datagridview
        public void InjectCBDatagridview()
        {
            if (CBCategories.SelectedItem.ToString() == "All" || CBCategories.SelectedItem.ToString() == "")
            {

                if (groupType.Contains(CBTypes.SelectedItem.ToString()) || CBTypes.SelectedItem.ToString() == "")
                {
                    InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedItem.ToString());
                }
                else
                {
                    if (CBTypes.SelectedIndex - groupType.Count < 0)
                    {
                        InitCBCategories(CBTypes.SelectedItem.ToString());
                    }
                    else
                    {
                        string pattern = @"(\d+)([A-Za-z]+)";
                        string input = folderNames[CBTypes.SelectedIndex - groupType.Count];

                        if (Regex.IsMatch(input, pattern))
                        {
                            InitDatagridView((CBTypes.SelectedIndex - groupType.Count + 1).ToString() + Trim.TrimCharacter(CBTypes.SelectedItem.ToString(), "0123456789"), CBCategories.SelectedItem.ToString());
                        }
                        else
                        {
                            InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedItem.ToString());
                        }
                    }
                }
            }
            else
            {
                string pattern = @"(\d+)([A-Za-z]+)";
                string input = structure[CBTypes.SelectedIndex - groupType.Count][CBCategories.SelectedIndex - 1];
                if (Regex.IsMatch(input, pattern))
                {
                    //MessageBox.Show("meow " + input);
                    input = folderNames[CBTypes.SelectedIndex - groupType.Count];
                    //MessageBox.Show("here " + input);
                    if (Regex.IsMatch(input, pattern))
                    {
                        InitDatagridView((CBTypes.SelectedIndex - groupType.Count + 1).ToString() + Trim.TrimCharacter(CBTypes.SelectedItem.ToString(), "0123456789"), CBCategories.SelectedIndex.ToString() + Trim.TrimCharacter(CBCategories.SelectedItem.ToString(), "0123456789"));
                    }
                    else
                    {
                        InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedIndex.ToString() + Trim.TrimCharacter(CBCategories.SelectedItem.ToString(), "0123456789"));
                    }
                }
                else
                {
                    //MessageBox.Show("Hello");
                    input = folderNames[CBTypes.SelectedIndex - groupType.Count];
                    if (Regex.IsMatch(input, pattern))
                    {
                        InitDatagridView(CBTypes.SelectedIndex.ToString() + Trim.TrimCharacter(CBTypes.SelectedItem.ToString(), "0123456789"), CBCategories.SelectedItem.ToString());
                    }
                    else
                    {
                        InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedItem.ToString());
                    }
                }
            }
        }
        //Sixth
        public void InitDatagridView(string type, string category)
        {
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            numberOfColumn = 0;

            if (groupType.Contains(type) && category == "")
            {
                if (headerGroupExpand[groupType.IndexOf(type)])
                {
                    List<string[]> list = new List<string[]>();

                    i = 0;
                    foreach (string folder in folderNames)
                    {
                        i = folderNames.IndexOf(folder);
                        if (!excludedFolderFromGroup[groupType.IndexOf(type)].Contains(Trim.TrimLeadingNumeric(folder)) && (includedFolderInGroup[groupType.IndexOf(type)].Contains(Trim.TrimLeadingNumeric(folder)) || includedFolderInGroup[groupType.IndexOf(type)].Length == 0))
                        {
                            //Check if folder is empty, then add anyways
                            if (structure[i].Length == 0)
                            {
                                list.Add(NewColumn(type, folderNames[i]));
                            }

                            foreach (string file in structure[i])
                            {
                                list.Add(NewColumn(type, folderNames[i], file));
                            }
                        }
                    }
                    int j;
                    if (list.Count > 0)
                    {
                        //Initialize first column
                        for (i = 0; i < list.Max(arr => arr.Length); i++)
                        {
                            string value = (i < list[0].Length) ? list[0][i] : null;
                            dataGridView1.Rows.Add(value);
                        }

                        //Create the latter
                        if (list.Count > 1)
                        {
                            for (i = 1; i < list.Count; i++)
                            {
                                j = 0;
                                foreach (string line in list[i])
                                {
                                    dataGridView1.Rows[j].Cells[i].Value = (j < list[i].Length) ? list[i][j] : null;
                                    j++;
                                }
                            }
                        }
                    }
                    //Resize the form
                    if (numberOfColumn > maxNumberOfColumnVisible)
                        dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * maxNumberOfColumnVisible + 25;
                    else
                        dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * (numberOfColumn) + 25;

                }
                else
                {
                    List<string[]> list = new List<string[]>();

                    i = 0;
                    foreach (string folder in folderNames)
                    {
                        if (!excludedFolderFromGroup[groupType.IndexOf(type)]
                            .Contains(Trim.TrimLeadingNumeric(folder)) && (includedFolderInGroup[groupType.IndexOf(type)].Contains(Trim.TrimLeadingNumeric(folder)) || includedFolderInGroup[groupType.IndexOf(type)].Length == 0))
                            list.Add(NewColumn(type, folderNames[i]));
                        i++;
                    }
                    int j;
                    if (list.Count > 0)
                    {
                        //Initialize first column
                        for (i = 0; i < list.Max(arr => arr.Length); i++)
                        {
                            string value = (i < list[0].Length) ? list[0][i] : null;
                            dataGridView1.Rows.Add(value);
                        }

                        //Create the latter
                        if (list.Count > 1)
                        {
                            for (i = 1; i < list.Count; i++)
                            {
                                j = 0;
                                foreach (string line in list[i])
                                {
                                    dataGridView1.Rows[j].Cells[i].Value = (j < list[i].Length) ? list[i][j] : null;
                                    j++;
                                }
                            }
                        }
                    }
                    //Resize the form
                    if (numberOfColumn > maxNumberOfColumnVisible)
                        dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * maxNumberOfColumnVisible + 25;
                    else
                        dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * (numberOfColumn) + 25;

                }
            }
            else if (!groupType.Contains(type) && category == "All")
            {
                if (headerExpand)
                {
                    List<string[]> list = new List<string[]>();
                    i = folderNames.IndexOf(type);
                    foreach (string file in structure[i])
                    {
                        list.Add(NewColumn(type, folderNames[i], file));
                    }

                    int j;
                    if (list.Count > 0)
                    {
                        //Initialize first column
                        for (i = 0; i < list.Max(arr => arr.Length); i++)
                        {
                            string value = (i < list[0].Length) ? list[0][i] : null;
                            dataGridView1.Rows.Add(value);
                        }

                        //Create the latter
                        if (list.Count > 1)
                        {
                            for (i = 1; i < list.Count; i++)
                            {
                                j = 0;
                                foreach (string line in list[i])
                                {
                                    dataGridView1.Rows[j].Cells[i].Value = (j < list[i].Length) ? list[i][j] : null;
                                    j++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    List<string[]> list = new List<string[]>();
                    i = folderNames.IndexOf(type);
                    list.Add(NewColumn(type, folderNames[i]));
                    if (list.Count > 0)
                    {
                        //Initialize first column
                        for (i = 0; i < list.Max(arr => arr.Length); i++)
                        {
                            string value = (i < list[0].Length) ? list[0][i] : null;
                            dataGridView1.Rows.Add(value);
                        }
                    }
                }
                //Resize the form
                if (numberOfColumn > maxNumberOfColumnVisible)
                    dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * maxNumberOfColumnVisible + 25;
                else if (numberOfColumn == 0)
                    dataGridView1.Width = 25;
                else
                    dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * (numberOfColumn) + 25;
            }
            //Groups doesn't have a category
            else if (groupType.Contains(type) && category != "All")
            {
                //Resize the form
                dataGridView1.Width = 0;
            }
            else if (!groupType.Contains(type) && category != "All")
            {
                List<string[]> list = new List<string[]>();
                i = folderNames.IndexOf(type);
                list.Add(NewColumn(type, type, category));

                if (list.Count > 0)
                {
                    //Initialize first column
                    for (i = 0; i < list.Max(arr => arr.Length); i++)
                    {
                        string value = (i < list[0].Length) ? list[0][i] : null;
                        dataGridView1.Rows.Add(value);
                    }
                    //Create the latter
                    if (list.Count > 1)
                    {
                        for (i = 1; i < list.Count; i++)
                        {
                            j = 0;
                            foreach (string line in list[i])
                            {
                                dataGridView1.Rows[j].Cells[i].Value = (j < list[i].Length) ? list[i][j] : null;
                                j++;
                            }
                        }
                    }
                }
                //Resize the form
                if (numberOfColumn > maxNumberOfColumnVisible)
                    dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * maxNumberOfColumnVisible + 25;
                else
                    dataGridView1.Width = (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * (numberOfColumn) + 25;
            }
            else
            {
                MessageBox.Show("Wtf");
            }
            if (dataGridView1.FirstDisplayedScrollingColumnIndex < dataGridView1.ColumnCount - 1 || dataGridView1.FirstDisplayedScrollingColumnIndex < dataGridView1.ColumnCount - 1)
            {
                BTRightScoll.Location = new Point(dataGridView1.Location.X + dataGridView1.Width - 35 - BTRightScoll.Width, BTRightScoll.Location.Y);
                BTRightScoll.Location = new Point(BTRightScoll.Location.X, BTRightScoll.Location.Y);
                //BTLeftScroll.Location = new Point(BTLeftScroll.Location.X + dataGridView1.Width - (numberOfColumn - 1 < 0 ? 0 : dataGridView1.Columns[numberOfColumn - 1].Width) * 3, BTLeftScroll.Location.Y);
                BTRightScoll.Show();
                BTLeftScroll.Show();

            }
            else
            {
                BTRightScoll.Hide();
                BTLeftScroll.Hide();
            }
        }
        //Seventh
        public string[] NewColumn(string type, string folder, string file = "")
        {
            string[] lines;
            int index;
            //MessageBox.Show(dataDelimiter);
            if (file != "")
            {
                lines = File.ReadAllText(path + folder + folderDelimiter + "\\" + file + ".txt").Split(dataDelimiter);
            }
            else
            {
                index = folderNames.IndexOf(folder);
                List<string> combinedLines = new List<string>(); // Create a list to store the combined elements
                //The var "i" was changed on the method InitDatagridiew
                foreach (string file2 in structure[i])
                {
                    // Read and split data from each file2
                    string[] file2Lines = File.ReadAllText(Path.Combine(path, folder + "\\" + file2 + ".txt")).Split(dataDelimiter);
                    // Add the elements from file2Lines to combinedLines
                    combinedLines.AddRange(file2Lines);
                }
                // Convert the list to an array if needed
                lines = combinedLines.ToArray();
            }
            dataGridView1.ColumnCount = numberOfColumn + 1;
            if (groupType.Contains(type) && file != "")
                dataGridView1.Columns[numberOfColumn].Name = Trim.TrimLeadingNumeric(folder) + "\n" + Trim.TrimLeadingNumeric(file);
            else if (groupType.Contains(type) && file == "")
                dataGridView1.Columns[numberOfColumn].Name = Trim.TrimLeadingNumeric(folder);
            else
                if (file != "")
                dataGridView1.Columns[numberOfColumn].Name = Trim.TrimLeadingNumeric(file);
            else dataGridView1.Columns[numberOfColumn].Name = Trim.TrimLeadingNumeric(folder);
            numberOfColumn++;
            return lines;
        }

        //Preparing actions's layout
        private void CBTextAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //changedProgrammatically = true;
            if (CBTextAction.Text == "Random text generator")
            {
                BTAction.Text = "Generate";
            }
            else
            {
                BTAction.Text = CBTextAction.Text;
            }
            if (showHint)
            {
                if (CBTextAction.SelectedItem.ToString() == "Random text generator")
                {
                    TTParam.SetToolTip(TBParam, "Create text from these characters");
                }
                else if (CBTextAction.SelectedItem.ToString() == "Replace")
                {
                    TTParam.SetToolTip(TBParam, "With which character?");
                }
                else if (CBTextAction.SelectedItem.ToString() == "Trim chrcter")
                {
                    TTParam.SetToolTip(TBParam, "Characters to trim off");
                }
                else if (CBTextAction.SelectedItem.ToString() == "Trim string")
                {
                    TTParam.SetToolTip(TBParam, "String to trim off");
                }
                else if (CBTextAction.SelectedItem.ToString() == "Regex check")
                {
                    TTParam.SetToolTip(TBParam, "Enter regex here");
                }
                else
                {
                    TTInput.SetToolTip(TBInput, "Output and input to be processed are here\nHover while typing to show contents");
                    TTParam.SetToolTip(TBParam, "Parameters to process inputs with based on actions\nHover while typing to show contents");
                    TTAction.SetToolTip(BTAction, "Go!");
                }
            }


            if (CBTextAction.SelectedItem.ToString() == "Z̷̤̣̲ͮͤ͜ą̲̬̲̪̠ͮļ̞̯̺ͧ͞͡g̷̶̴̴̟ͥͫo̯̱ͪͭͩ͜͞f̡̢̤̤̞ͨ͜y̵̜̲̬̞̱͡")
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = true;
                label3.Visible = true;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
            }
            else if (CBTextAction.SelectedItem.ToString() == "s̶t̶r̶i̶k̶e̶t̶h̶r̶o̶u̶g̶h̶")
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
            }
            else if (CBTextAction.SelectedItem.ToString() == "Trim string")
            {
                TBParam.Visible = true;
                LBParamCount.Visible = true;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "String";
            }
            else if (CBTextAction.SelectedItem.ToString() == "Trim chrcter")
            {
                TBParam.Visible = true;
                LBParamCount.Visible = true;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Characters";
            }
            else if (CBTextAction.SelectedItem.ToString() == "Regex check")
            {
                TBParam.Visible = true;
                LBParamCount.Visible = true;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Regex";
            }

            else if (CBTextAction.SelectedItem.ToString() == "Random text generator")
            {
                TBParam.Visible = true;
                LBParamCount.Visible = true;
                LBRandom.Visible = true;
                RegexMaxRandomLength.Visible = true;
                RegexMinRandomLength.Visible = true;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Characters";
            }
            else if (CBTextAction.SelectedItem.ToString() == "Replace")
            {
                TBParam.Visible = true;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = true;
                TBParam.PlaceholderText = "With";
            }
            else if ((CBTextAction.SelectedItem.ToString() == "-- --- .-. ... .   -.-. --- -.. .   . -. -.-. .-. -.-- .--. -"))
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
                originalText = TBInput.Text;
            }
            else if ((CBTextAction.SelectedItem.ToString() == "Morse code decrypt"))
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
                originalText = TBInput.Text;
            }
            else if ((CBTextAction.SelectedItem.ToString() == "01000101 01101110 01100011 01110010 01111001 01110000 01110100 00100000 01100010 01101001 01101110 01100001 01110010 01111001"))
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
                originalText = TBInput.Text;
            }
            else if ((CBTextAction.SelectedItem.ToString() == "Decrypt binary"))
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
                originalText = TBInput.Text;
            }
            else
            {
                TBParam.Visible = false;
                LBParamCount.Visible = false;
                LBRandom.Visible = false;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
                TBReplace.Visible = false;
                TBParam.PlaceholderText = "Parameter";
            }
        }
        //Action button click
        int binaryChoice = 1;
        private void BTAction_Click(object sender, EventArgs e)
        {

            SimpleEncrypt simpleEncrypt = new SimpleEncrypt();
            if (CBTextAction.SelectedItem == null)
            {
                MessageBox.Show("Congrate! Ester egg found\nSteam achievement acquired (sarcasm)\n\n(　´_ﾉ` )");
            }
            else
            {
                if (CBTextAction.SelectedItem.ToString() == "Z̷̤̣̲ͮͤ͜ą̲̬̲̪̠ͮļ̞̯̺ͧ͞͡g̷̶̴̴̟ͥͫo̯̱ͪͭͩ͜͞f̡̢̤̤̞ͨ͜y̵̜̲̬̞̱͡")
                {
                    changedProgrammatically = true;
                    if (ZalgoIntensity.Value < 1)
                        ZalgoIntensity.Value = 1;
                    else if (ZalgoIntensity.Value > 50)
                        ZalgoIntensity.Value = 50;
                    unoriginalText = ZalgoStuffs.ZalgoFyText(originalText, ZalgoIntensity.Value);
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "UnZalgofy")
                {
                    changedProgrammatically = true;
                    TBInput.Text = ZalgoStuffs.UnzalgoFyText(originalText);
                }
                else if (CBTextAction.SelectedItem.ToString() == "s̶t̶r̶i̶k̶e̶t̶h̶r̶o̶u̶g̶h̶")
                {
                    changedProgrammatically = true;
                    TBInput.Text = StrikeThrough.ApplyStrikethrough(originalText);
                }
                else if (CBTextAction.SelectedItem.ToString() == "uʍop ǝpısd∩")
                {
                    changedProgrammatically = true;
                    string upsideDownString = UpsideDown.MakeUpsideDown(TBInput.Text);
                    unoriginalText = upsideDownString;
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "Downside up")
                {
                    changedProgrammatically = true;
                    string upsideDownString = UpsideDown.MakeDownsideUp(TBInput.Text);
                    unoriginalText = upsideDownString;
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "ɿoɿɿiM")
                {
                    changedProgrammatically = true;
                    string mirroredString = UpsideDown.MirrorLeftRight(TBInput.Text);
                    unoriginalText = mirroredString;
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Unmirror")
                {
                    changedProgrammatically = true;
                    string unmirroredString = UpsideDown.UnMirrorLeftRight(TBInput.Text);
                    unoriginalText = unmirroredString;
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "esreveR")
                {
                    changedProgrammatically = true;
                    string reversedString = UpsideDown.Reverse(TBInput.Text);
                    unoriginalText = reversedString;
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "raNDomly CApItAlizE")
                {
                    changedProgrammatically = true;
                    unoriginalText = RandomCapital.RandomlyCapitalize(TBInput.Text);
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "TO UPPER CASE")
                {
                    changedProgrammatically = true;
                    unoriginalText = TBInput.Text.ToUpper();
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "to lower case")
                {
                    changedProgrammatically = true;
                    unoriginalText = TBInput.Text.ToLower();
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "To Title Case")
                {
                    changedProgrammatically = true;
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    unoriginalText = textInfo.ToTitleCase(TBInput.Text);
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "Trim string")
                {
                    changedProgrammatically = true;
                    string sentence = TBInput.Text;
                    string stringToRemove = TBParam.Text;
                    string trimmedSentence = Trim.TrimString(sentence, stringToRemove);
                    unoriginalText = trimmedSentence;
                    TBInput.Text = unoriginalText;

                }
                else if (CBTextAction.SelectedItem.ToString() == "Trim chrcter")
                {
                    changedProgrammatically = true;
                    string sentence = TBInput.Text;
                    string stringToRemove = TBParam.Text;
                    string trimmedSentence = Trim.TrimCharacter(sentence, stringToRemove);
                    unoriginalText = trimmedSentence;
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Trim duplcates")
                {
                    changedProgrammatically = true;
                    string sentence = TBInput.Text;
                    string stringToRemove = TBParam.Text;
                    string trimmedSentence = Trim.TrimDuplicates(sentence);
                    unoriginalText = trimmedSentence;
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Replace")
                {
                    changedProgrammatically = true;
                    unoriginalText = TBInput.Text.Replace(TBReplace.Text, TBParam.Text);
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Simple cipher")
                {
                    changedProgrammatically = true;
                    string ruleFilePath = path + "/Cipher.txt";
                    simpleEncrypt.LoadCipherRules(ruleFilePath);
                    string originalString = TBInput.Text;
                    string cipheredString = simpleEncrypt.SimpleCipher(originalString);
                    unoriginalText = cipheredString;
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Simple decipher")
                {
                    changedProgrammatically = true;
                    string ruleFilePath = path + "/Cipher.txt";
                    simpleEncrypt.LoadCipherRules(ruleFilePath);
                    string originalString = TBInput.Text;
                    string decipheredString = simpleEncrypt.SimpleDecipher(originalString);
                    unoriginalText = decipheredString;
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Random text generator")
                {
                    changedProgrammatically = true;
                    if (RegexMinRandomLength.Value > RegexMaxRandomLength.Value)
                        MessageBox.Show("Min cannot greater than max");
                    else if (RegexMinRandomLength.Value <= 0)
                        MessageBox.Show("Min must greater than 0");
                    else
                    {
                        unoriginalText = RandomText.RandomTextGenerate(TBParam.Text, Convert.ToInt32(RegexMinRandomLength.Value), Convert.ToInt32(RegexMaxRandomLength.Value));
                        TBInput.Text = unoriginalText;
                    }
                }
                else if (CBTextAction.SelectedItem.ToString() == "Regex check")
                {
                    var regex = new System.Text.RegularExpressions.Regex(TBParam.Text); // Your regex here
                    if (regex.IsMatch(originalText))
                    {
                        MessageBox.Show(originalText + " matched");
                    }
                    else
                    {
                        MessageBox.Show(originalText + " doesn't match");
                    }
                }
                else if (CBTextAction.SelectedItem.ToString() == "-- --- .-. ... .   -.-. --- -.. .   . -. -.-. .-. -.-- .--. -")
                {
                    changedProgrammatically = true;
                    unoriginalText = MorseCode.TextToMorse(originalText);
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Morse code decrypt")
                {
                    changedProgrammatically = true;
                    unoriginalText = MorseCode.MorseToText(originalText);
                    TBInput.Text = unoriginalText;
                }
                else if (CBTextAction.SelectedItem.ToString() == "01000101 01101110 01100011 01110010 01111001 01110000 01110100 00100000 01100010 01101001 01101110 01100001 01110010 01111001")
                {

                    changedProgrammatically = true;
                    unoriginalText = Binary.StringToBinary(originalText, binaryChoice);
                    TBInput.Text = unoriginalText;
                    binaryChoice = (binaryChoice % 3) + 1;
                }
                else if (CBTextAction.SelectedItem.ToString() == "Decrypt binary")
                {
                    changedProgrammatically = true;
                    unoriginalText = Binary.BinaryToString(originalText);
                    TBInput.Text = unoriginalText;
                }
                else
                {
                    MessageBox.Show("TODO");
                }
                if (actionInstantCopy && !String.IsNullOrEmpty(TBInput.Text))
                {
                    TryClipboard(TBInput.Text.ToString());
                }
            }
        }

        private void BTEmpty_Click(object sender, EventArgs e)
        {

        }

        //Header expand feature
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                string columnHeader = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                if (groupType.Contains(CBTypes.SelectedItem.ToString()))
                    headerGroupExpand[groupType.IndexOf(CBTypes.SelectedItem.ToString())] = !headerGroupExpand[groupType.IndexOf(CBTypes.SelectedItem.ToString())];
                else headerExpand = !headerExpand;
                InjectCBDatagridview();
            }
        }

        //Prevent header's default action
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        //Drag and drop will only happen if mouse move away
        private Point? _mouseDownLocation = null;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0 && info.ColumnIndex >= 0)
                {
                    string text = (String)dataGridView1.Rows[info.RowIndex].Cells[info.ColumnIndex].Value;
                    if (text != null || text != "")
                    {
                        DataObject dataObj = new DataObject();
                        try
                        {
                            dataObj.SetText(text);
                        }
                        catch { }
                        _mouseDownLocation = e.Location;
                        dataGridView1.MouseMove += dataGridView1_MouseMove;
                    }
                }
            }
        }
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDownLocation.HasValue && (Math.Abs(e.X - _mouseDownLocation.Value.X) > SystemInformation.DragSize.Width || Math.Abs(e.Y - _mouseDownLocation.Value.Y) > SystemInformation.DragSize.Height))
            {
                // Check if dataGridView1 and the SelectedCells[0].Value are not null before using them
                if (dataGridView1 != null && dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].Value != null)
                {
                    dataGridView1.DoDragDrop(dataGridView1.SelectedCells[0].Value.ToString(), DragDropEffects.Copy);
                    dataGridView1.MouseMove -= dataGridView1_MouseMove;
                }
            }
        }


        //Datagridview copy when click on items
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check if it's a valid cell
            {
                // Get the cell value
                if (e.Button == MouseButtons.Left)
                {
                    object cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (cellValue != null)
                    {
                        // Copy the cell value to the clipboard

                        TryClipboard(cellValue.ToString());
                        var cellRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        TTCopied.Show(cellValue.ToString() + " copied", this, cellRectangle.Left + cellRectangle.Width + 200, cellRectangle.Top + cellRectangle.Height, 2000);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    /*this.Hide();
                    notiCopyTool.Visible = true;*/

                    if (groupType.Contains(CBTypes.SelectedItem.ToString()))
                    {
                        if (headerGroupExpand[CBTypes.SelectedIndex])
                            contextMenuStrip2.Items["openDataFileToolStripMenuItem"].Visible = true;
                        else
                            contextMenuStrip2.Items["openDataFileToolStripMenuItem"].Visible = false;
                    }
                    else
                    {
                        if (headerExpand)
                            contextMenuStrip2.Items["openDataFileToolStripMenuItem"].Visible = true;
                        else
                            contextMenuStrip2.Items["openDataFileToolStripMenuItem"].Visible = false;
                    }

                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    int rowIndex = e.RowIndex; // Replace with the desired row index
                    int columnIndex = e.ColumnIndex; // Replace with the desired column index
                    try
                    {
                        contextMenuStrip2.Show(dataGridView1, dataGridView1.GetCellDisplayRectangle(columnIndex, rowIndex + 1, false).Location);
                    }
                    catch
                    {
                        contextMenuStrip2.Show(dataGridView1, dataGridView1.GetCellDisplayRectangle(columnIndex, rowIndex, false).Location);
                    }
                }
            }
            //Send output to textbox
            /*if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the cell value as a string
                string cellValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString();

                if (!string.IsNullOrEmpty(cellValue))
                {
                    // Focus on the DataGridView to ensure that SendKeys works
                    TBInput.Focus();
                    SendKeys.Send(Regex.Replace(dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString(), "[+^%~()]", "{$0}"));
                }
            }*/
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
        }
        //Custom Datagridview items's preview tooltip
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell != null && cell.Value != null)
            {
                if (showHint)
                    cell.ToolTipText = cell.Value.ToString() + "\n\nLeft-click to copy\nRight-click to open menu";
                else
                    cell.ToolTipText = cell.Value.ToString();
            }
            else if (e.ColumnIndex == 0)
            {
                if (cell != null)
                {
                    if (showHint) { }
                    //cell.ToolTipText = "Click to expand/shrink columns";
                    else
                        cell.ToolTipText = "";
                }
            }
            else
            {
                cell.ToolTipText = "";
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {

        }
        private void Form1_Resize(object sender, EventArgs e)
        {

        }
        private void ZalgoIntensity_ValueChanged(object sender, EventArgs e)
        {

        }
        private void RandomCapital_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LLGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "\"https://github.com/Vipxpert/WayText\"";
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    Process.Start(url);
                }
                catch
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", url);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", url);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                TryClipboard(url);
            }
        }

        public void TryClipboard(string text)
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    Clipboard.SetText(text);
                    success = true;
                }
                catch
                {
                    success = false;
                }
            }
        }

        private void TBParam_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                string droppedText = e.Data.GetData(DataFormats.Text).ToString();
                TBParam.Text = droppedText;
            }
        }

        private void TBParam_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TBInput_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                string droppedText = e.Data.GetData(DataFormats.Text).ToString();
                TBInput.Text = droppedText;
            }
        }

        private void TBInput_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TBReplace_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                string droppedText = e.Data.GetData(DataFormats.Text).ToString();
                TBReplace.Text = droppedText;
            }
        }

        private void TBReplace_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        static BackgroundWorker worker;
        private void runInCMDToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void runInCMDToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridViewCell selectedCell = dataGridView1.CurrentCell;
                if (selectedCell != null && dataGridView1 != null && dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].Value != null)
                {
                    string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string cmdContent = selectedCell.Value.ToString();
                    if (!string.IsNullOrEmpty(cmdContent))
                    {
                        worker = new BackgroundWorker();
                        worker.DoWork += RunCmd;
                        //worker.RunWorkerCompleted += CmdFinished;
                        worker.RunWorkerAsync("cd " + userPath + "\n" + cmdContent);
                    }
                }
                else
                {
                    Process.Start("cmd.exe");
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Process.Start("cmd.exe");
            }
        }

        static void RunCmd(object sender, DoWorkEventArgs e)
        {
            string cmdText = (string)e.Argument;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.RedirectStandardOutput = false;
            cmdProcess.StartInfo.CreateNoWindow = false;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine(cmdText);
            //cmdProcess.StandardInput.WriteLine("exit");

            e.Result = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
        }

        public void CmdFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                changedProgrammatically = true;
                string outputStr = (string)e.Result;
                TBInput.Text = outputStr;
            }
            catch
            {

            }
        }

        string[] currentColPath;
        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColPath = dataGridView1.Columns[dataGridView1.SelectedCells[0].ColumnIndex].HeaderText.Split("\n");
            if (types.Contains(CBTypes.SelectedItem.ToString()))
            {
                try
                {
                    try
                    {
                        Process.Start("explorer.exe", path + folderNames[types.IndexOf(CBTypes.SelectedItem.ToString())] + "\\" + fileNames[categories.IndexOf(currentColPath[0])] + ".txt");
                    }
                    catch
                    {
                        Process.Start("files.exe", path + folderNames[types.IndexOf(CBTypes.SelectedItem.ToString())] + "\\" + fileNames[categories.IndexOf(currentColPath[0])] + ".txt");
                    }
                }
                catch
                {
                    MessageBox.Show("Something went wrong");
                }
            }
            else
            {
                if (types.Contains(currentColPath[0]) && categories.Contains(currentColPath[1]))
                {
                    try
                    {
                        try
                        {
                            Process.Start("explorer.exe", path + folderNames[types.IndexOf(currentColPath[0])] + "\\" + fileNames[categories.IndexOf(currentColPath[1])] + ".txt");

                        }
                        catch
                        {
                            Process.Start("files.exe", path + folderNames[types.IndexOf(currentColPath[0])] + "\\" + fileNames[categories.IndexOf(currentColPath[1])] + ".txt");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Something went wrong");
                    }
                }
            }
        }

        private void openDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColPath = dataGridView1.Columns[dataGridView1.SelectedCells[0].ColumnIndex].HeaderText.Split("\n");
            if (types.Contains(CBTypes.SelectedItem.ToString()))
            {
                try
                {
                    try
                    {
                        Process.Start("explorer.exe", path + folderNames[types.IndexOf(CBTypes.SelectedItem.ToString())]);

                    }
                    catch
                    {
                        Process.Start("files.exe", path + folderNames[types.IndexOf(CBTypes.SelectedItem.ToString())]);
                    }
                }
                catch
                {
                    MessageBox.Show("Something went wrong");
                }
            }
            else
            {
                if (types.Contains(currentColPath[0]))
                {
                    try
                    {
                        try
                        {
                            Process.Start("explorer.exe", path + folderNames[types.IndexOf(currentColPath[0])]);

                        }
                        catch
                        {
                            Process.Start("files.exe", path + folderNames[types.IndexOf(currentColPath[0])]);

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Something went wrong");
                    }
                }
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            notiCopyTool.Visible = true;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void restartAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
            System.Diagnostics.Process.Start(Application.ExecutablePath); // Start a new instance of the application
        }

        private void openSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\" + "AppConfig.json");

            }
            catch
            {
                Process.Start("files.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\" + "AppConfig.json");

            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\" + "AppConfig.json");

            }
            catch
            {
                Process.Start("files.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WayText\\" + "AppConfig.json");

            }
        }

        private void LLManual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "README.md");

            }
            catch
            {
                Process.Start("files.exe", AppDomain.CurrentDomain.BaseDirectory + "README.md");

            }
        }

        private void TBInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*// Check if the pressed key is the Tab key ('\t')
            if (e.KeyChar == '\t')
            {
                // Handle the Tab key as a character input
                TextBox textBox = (TextBox)sender;
                int selectionStart = textBox.SelectionStart;

                // Insert the Tab character at the current cursor position
                textBox.Text = textBox.Text.Insert(selectionStart, "\t");

                // Move the cursor position after the inserted Tab character
                textBox.SelectionStart = selectionStart + 1;

                // Prevent the default Tab key behavior (focus change)
                e.Handled = true;
            }*/
        }

        private void TBParam_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*// Check if the pressed key is the Tab key ('\t')
            if (e.KeyChar == '\t')
            {
                // Handle the Tab key as a character input
                TextBox textBox = (TextBox)sender;
                int selectionStart = textBox.SelectionStart;

                // Insert the Tab character at the current cursor position
                textBox.Text = textBox.Text.Insert(selectionStart, "\t");

                // Move the cursor position after the inserted Tab character
                textBox.SelectionStart = selectionStart + 1;

                // Prevent the default Tab key behavior (focus change)
                e.Handled = true;
            }*/
        }

        private void BTRevert_Click(object sender, EventArgs e)
        {
            if (TBInput.Text == originalText)
            {
                changedProgrammatically = true;
                TBInput.Text = unoriginalText;
            }
            else if (TBInput.Text != originalText)
            {
                changedProgrammatically = true;
                TBInput.Text = originalText;
            }
        }

        //Tooltip to show textboxes infos
        private void TBParam_TextChanged(object sender, EventArgs e)
        {
            string[] words = TBParam.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\\', '(', '{', '[' }, StringSplitOptions.RemoveEmptyEntries);
            if (words != null && words.Length > 0)
                TTParam.SetToolTip(TBParam, TBParam.Text.Length.ToString() + " characters\n" + words.Length + " words\n" + TBParam.Text);
            else
            if (showHint) TTParam.SetToolTip(TBParam, "Parameters to process inputs with based on actions\nHover while typing to show contents");
            else
                TTParam.SetToolTip(TBParam, TBParam.Text.Length.ToString() + " characters\n" + words.Length + " words\n" + TBParam.Text);
        }
        private void TBInput_TextChanged(object sender, EventArgs e)
        {

            //Ignore if changes are not made by human
            if (changedProgrammatically)
            {
                changedProgrammatically = false;
            }
            else
            {
                //MessageBox.Show("hmm");
                originalText = TBInput.Text;
            }
            string[] words = TBInput.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\\', '(', '{', '[' }, StringSplitOptions.RemoveEmptyEntries);
            if (words != null && words.Length > 0)
                TTInput.SetToolTip(TBInput, " " + TBInput.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBInput.Text);
            else
            if (showHint) TTInput.SetToolTip(TBInput, "Output and input to be processed are here\nHover while typing to show contents");
            else
                TTInput.SetToolTip(TBInput, " " + TBInput.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBInput.Text);
        }
        private void TBInput_Click(object sender, EventArgs e)
        {
            changedProgrammatically = false; //Click on textbox register human input
        }

        private void TBReplace_TextChanged(object sender, EventArgs e)
        {
            string[] words = TBReplace.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\\', '(', '{', '[' }, StringSplitOptions.RemoveEmptyEntries);
            if (words != null && words.Length > 0)
                TTReplace.SetToolTip(TBReplace, " " + TBReplace.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBReplace.Text);
            else
            if (showHint) TTReplace.SetToolTip(TBReplace, "Replace which charcter?");
            else TTReplace.SetToolTip(TBReplace, " " + TBReplace.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBReplace.Text);

        }

        bool tbinputExpand = false;
        bool tbparamExpand = false;
        bool tbreplaceExpand = false;
        private void TBInput_GotFocus(object sender, EventArgs e)
        {
            if (!tbinputExpand)
            {
                TBInput.Size = new System.Drawing.Size(300, 108);
                TBInput.ScrollBars = ScrollBars.Both;
                TBInput.BringToFront();
                tbinputExpand = true;
            }
        }

        private void TBInput_LostFocus(object sender, EventArgs e)
        {
            if (tbinputExpand)
            {
                TBInput.Size = new System.Drawing.Size(300, 36);
                TBInput.ScrollBars = ScrollBars.None;
                tbinputExpand = false;
            }
        }

        private void TBParam_GotFocus(object sender, EventArgs e)
        {
            if (!tbparamExpand)
            {
                TBParam.Size = new System.Drawing.Size(300, 108);
                TBParam.ScrollBars = ScrollBars.Both;
                TBParam.BringToFront();
                tbparamExpand = true;
            }
        }

        private void TBParam_LostFocus(object sender, EventArgs e)
        {
            if (tbparamExpand)
            {
                TBParam.Size = new System.Drawing.Size(300, 36);
                TBParam.ScrollBars = ScrollBars.None;
                tbparamExpand = false;
            }
        }

        private void TBReplace_GotFocus(object sender, EventArgs e)
        {
            if (!tbreplaceExpand)
            {
                TBReplace.Size = new System.Drawing.Size(300, 108);
                TBReplace.ScrollBars = ScrollBars.Both;
                TBReplace.BringToFront();
                tbreplaceExpand = true;
            }
        }

        private void TBReplace_LostFocus(object sender, EventArgs e)
        {
            if (tbreplaceExpand)
            {
                TBReplace.Size = new System.Drawing.Size(300, 36);
                TBReplace.ScrollBars = ScrollBars.None;
                tbreplaceExpand = false;
            }
        }

        private void TBInput_MouseHover(object sender, EventArgs e)
        {
            /*int toolTipX = TBInput.Location.X + TBInput.Width - 10;
            int toolTipY = -10;

            string[] words = TBInput.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\\', '(', '{', '[' }, StringSplitOptions.RemoveEmptyEntries);
            if (words != null && words.Length > 0)
                TTInput.Show(" " + TBInput.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBInput.Text, TBInput, toolTipX, toolTipY);
            else
                TTInput.Show("Output and input to be processed are here\nHover while typing to show contents", TBInput, toolTipX, toolTipY);*/
            //this.ActiveControl = null;
            if (!tbinputExpand)
            {
                TBInput.Size = new System.Drawing.Size(300, 108);
                TBInput.ScrollBars = ScrollBars.Both;
                TBInput.BringToFront();
                tbinputExpand = true;
            }
        }

        private void TBInput_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            TBInput.Size = new System.Drawing.Size(300, 36);
            TBInput.ScrollBars = ScrollBars.None;
            tbinputExpand = false;
        }

        private void TBParam_MouseHover(object sender, EventArgs e)
        {
            if (!tbparamExpand)
            {
                TBParam.Size = new System.Drawing.Size(300, 108);
                TBParam.ScrollBars = ScrollBars.Both;
                TBParam.BringToFront();
                tbparamExpand = true;
            }
        }

        private void TBParam_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            TBParam.Size = new System.Drawing.Size(300, 36);
            TBParam.ScrollBars = ScrollBars.None;
            tbparamExpand = false;
        }

        private void TBReplace_MouseHover(object sender, EventArgs e)
        {
            if (!tbreplaceExpand)
            {
                TBReplace.Size = new System.Drawing.Size(300, 108);
                TBReplace.ScrollBars = ScrollBars.Both;
                TBReplace.BringToFront();
                tbreplaceExpand = true;
            }
        }

        private void TBReplace_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            TBReplace.Size = new System.Drawing.Size(300, 36);
            TBReplace.ScrollBars = ScrollBars.None;
            tbreplaceExpand = false;
        }

        private void BTEmpty_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void BTEmpty_MouseDown(object sender, MouseEventArgs e)
        {
            changedProgrammatically = false;
            TBInput.Text = null;
            if (e.Button == MouseButtons.Right)
            {
                TBInput.Text = null;
                TBParam.Text = null;
                TBReplace.Text = null;
            }
        }

        private void CBTypes_MouseHover(object sender, EventArgs e)
        {
            //CBTypes.DroppedDown = true;
        }
        private void CBTypes_MouseLeave(object sender, EventArgs e)
        {

            //CBTypes.DroppedDown = false;
        }

        private void CBCategories_MouseHover(object sender, EventArgs e)
        {
            //CBCategories.DroppedDown = true;
        }

        private void CBCategories_MouseLeave(object sender, EventArgs e)
        {
            //CBCategories.DroppedDown = false;
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void TBInput_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TryClipboard(TBInput.Text);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InjectCBDatagridview();
        }

        private void reloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCBTypes();
        }


        private void BTLeftScroll_Click(object sender, EventArgs e)
        {

        }

        private void BTRightScoll_Click(object sender, EventArgs e)
        {

        }

        private void BTLeftScroll_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void BTRightScoll_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void BTLeftScroll_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dataGridView1.FirstDisplayedScrollingColumnIndex > 0)
                    try
                    {
                        dataGridView1.FirstDisplayedScrollingColumnIndex -= numberOfScroll;
                    }
                    catch { dataGridView1.FirstDisplayedScrollingColumnIndex -= dataGridView1.FirstDisplayedScrollingColumnIndex; }
            }
            else
                dataGridView1.FirstDisplayedScrollingColumnIndex -= dataGridView1.FirstDisplayedScrollingColumnIndex;
        }

        private void BTRightScoll_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dataGridView1.FirstDisplayedScrollingColumnIndex < dataGridView1.ColumnCount - 1)
                    try
                    {
                        dataGridView1.FirstDisplayedScrollingColumnIndex += numberOfScroll;
                    }
                    catch { dataGridView1.FirstDisplayedScrollingColumnIndex += numberOfColumn - maxNumberOfColumnVisible; }
            }
            else { dataGridView1.FirstDisplayedScrollingColumnIndex += numberOfColumn - maxNumberOfColumnVisible; }
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            if (scrollingLeft)
            {
                ScrollLeft();
            }
            else if (scrollingRight)
            {
                ScrollRight();
            }
        }

        private void ScrollLeft()
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex > 0)
            {
                try
                {
                    dataGridView1.FirstDisplayedScrollingColumnIndex -= numberOfScroll;
                }
                catch
                {
                    dataGridView1.FirstDisplayedScrollingColumnIndex = 0;
                }
            }
        }

        private void ScrollRight()
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex < dataGridView1.ColumnCount - 1)
            {
                try
                {
                    dataGridView1.FirstDisplayedScrollingColumnIndex += numberOfScroll;
                }
                catch
                {
                    dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView1.ColumnCount - 1;
                }
            }
        }


        private void BTLeftScroll_MouseHover(object sender, EventArgs e)
        {
            scrollingLeft = true;
            scrollTimer.Start();
        }

        private void BTLeftScroll_MouseLeave(object sender, EventArgs e)
        {
            scrollingLeft = false;
            scrollTimer.Stop();
        }

        private void BTRightScoll_MouseHover(object sender, EventArgs e)
        {
            scrollingRight = true;
            scrollTimer.Start();
        }

        private void BTRightScoll_MouseLeave(object sender, EventArgs e)
        {
            scrollingRight = false;
            scrollTimer.Stop();
        }

        private void BTLeftScroll_MouseEnter(object sender, EventArgs e)
        {
            ScrollLeft();
        }

        private void BTRightScoll_MouseEnter(object sender, EventArgs e)
        {
            ScrollRight();
        }
    }
}
