using static System.Windows.Forms.LinkLabel;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text.Json;
using CopyToolGUI;

namespace Emoji
{
    public partial class Form1 : Form
    {

        int i = 0, numberOfColumn = 0, maxNumberOfColumnVisible = 5, formHeight = 371, startTypeIndex = 0;
        string[] excludedFolderFromAll;
        bool headerExpand = true;
        //Delimiter for folders's name. Make null if unused
        string folderDelimiter = "";
        //Delimiter for file reading
        string dataDelimiter = "\u3164";
        //Only folders/types
        List<string> folderNames = new List<string>();
        //Only files/categories
        List<string> fileNames = new List<string>();
        //Each element is an array that contain files's names/equivalent to a folder that contains files
        //Or said Types that contains Categories
        //Intuitively structure[0] store files's name's of folderNames[0]
        List<string[]> structure = new List<string[]>();

        public Form1()
        {
            string configFilePath = AppDomain.CurrentDomain.BaseDirectory + "/AppConfig.json";

            // Check if the config file exists
            if (File.Exists(configFilePath))
            {
                // Read the JSON content from the file
                string jsonContent = File.ReadAllText(configFilePath);

                // Deserialize the JSON content into the AppConfig class
                var config = JsonSerializer.Deserialize<AppConfig>(jsonContent);

                try
                {
                    folderDelimiter = config.folderDelimiter;
                    dataDelimiter = config.dataDelimiter;
                    maxNumberOfColumnVisible = config.maxNumberOfColumnVisible;
                    formHeight = config.formHeight;
                    excludedFolderFromAll = config.excludedFolderFromAll;
                    startTypeIndex = config.startTypeIndex;
                }
                catch
                {
                    MessageBox.Show("Something is wrong with the config");
                }
            }
            else
            {
                MessageBox.Show(configFilePath + " not found");
            }


            InitializeComponent();
            //Making modifications to the variables
            //Scan for folders
            foreach (string folder in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory).Select(Path.GetFileName).ToArray())
            {
                folderNames.Add(folder);
            }

            //Remove Hangul filler from folders
            if (!String.IsNullOrEmpty(folderDelimiter))
                for (i = 0; i < folderNames.Count; i++)
                {
                    //MessageBox.Show(folderNames[i]);
                    folderNames[i] = folderNames[i].Replace(folderDelimiter, null);
                }

            //Add array of array (types and categories)
            foreach (string folder in folderNames)
            {
                structure.Add(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/" + folder + folderDelimiter + "/", "*.txt").Select(Path.GetFileNameWithoutExtension).ToArray());
            }
            i = 0;
            foreach (string[] folder in structure)
            {
                foreach (string file in folder)
                {
                    fileNames.Add(file);
                }
            }
        }
        System.Windows.Forms.ToolTip toolTip3 = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip4 = new System.Windows.Forms.ToolTip();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Get cursor position
            Point cursorPos = Cursor.Position;
            int newX = cursorPos.X - (Width / 2);
            int newY = cursorPos.Y - (Height / 2);

            //Get dimension
            Rectangle virtualScreen = SystemInformation.VirtualScreen;

            //Form won't get flow off the screen
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

            //Locate form to mouse cursor
            Location = new Point(newX, newY);
            InitCBTypes();
            dataGridView1.RowHeadersVisible = false;
            Text = string.Empty;
            ControlBox = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            this.TopMost = true;

            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.ToolTipTitle = "Hint";
            //toolTip1.UseFading = true;
            //toolTip1.UseAnimation = true;
            //toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = false;
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 1500;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(panel1, "Right click to exit \nLeft click to drag");

            //toolTip3.ToolTipTitle = "Hint";
            toolTip3.ShowAlways = false;
            toolTip3.AutoPopDelay = 3000;
            toolTip3.InitialDelay = 500;
            toolTip3.ReshowDelay = 500;
            toolTip3.SetToolTip(TBInput, "Output and input to be processed are here\nHover while typing to show contents");

            //toolTip4.ToolTipTitle = "Hint";
            toolTip4.ShowAlways = false;
            toolTip4.AutoPopDelay = 3000;
            toolTip4.InitialDelay = 500;
            toolTip4.ReshowDelay = 500;
            toolTip4.SetToolTip(TBParam, "Parameters to process inputs with based on actions\nHover while typing to show contents");

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);

            this.panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            this.panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
            this.panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            string[] textAction = { "Z̷̤̣̲ͮͤ͜ą̲̬̲̪̠ͮļ̞̯̺ͧ͞͡g̷̶̴̴̟ͥͫo̯̱ͪͭͩ͜͞f̡̢̤̤̞ͨ͜y̵̜̲̬̞̱͡", "uʍop ǝpısd∩", "Downside up", "esreveR", "raNDomly CApItAlizE", "TO UPPER CASE", "to lower case", "To Title Case", "Trim chrcter", "Trim string", "Simple cipher", "Simple decipher", "Regex check", "Random text generator" };
            foreach (string action in textAction)
            {
                CBTextAction.Items.Add(action);
            }
            CBTextAction.SelectedIndex = 0;

            //dataGridView1.CellToolTipTextNeeded += dataGridView1_CellToolTipTextNeeded;
            //this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
        }

        /// <summary>
        /// Prototype 2: Need to know file's name
        /// </summary>
        /// <param name="type"></param>
        public void InitDatagridView(string type, string category)
        {
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            numberOfColumn = 0;

            if (type == "All" && category == "")
            {
                if (headerExpand)
                {
                    List<string[]> list = new List<string[]>();


                    i = 0;
                    foreach (string folder in folderNames)
                    {
                        i = folderNames.IndexOf(folder);
                        //MessageBox.Show(folder);
                        foreach (string file in structure[i])
                        {
                            // Check if the folder is in the excludedFolderFromAll array
                            if (!excludedFolderFromAll.Contains(folder))
                            {
                                // Add the item to the list only if the folder is not in the excluded list
                                list.Add(NewColumn(type, folderNames[i], file));
                            }
                        }
                    }
                    int ii;
                    string[] lines;
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
                                ii = 0;
                                foreach (string line in list[i])
                                {
                                    dataGridView1.Rows[ii].Cells[i].Value = (ii < list[i].Length) ? list[i][ii] : null;
                                    ii++;
                                }
                            }
                        }
                    }
                    //Resize the form
                    //Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + panel1.Width + 60;
                    if (numberOfColumn > maxNumberOfColumnVisible)
                        dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * maxNumberOfColumnVisible + 25;
                    else
                        dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + 25;

                }
                else
                {
                    List<string[]> list = new List<string[]>();

                    i = 0;
                    foreach (string folder in folderNames)
                    {
                        list.Add(NewColumn(type, folderNames[i]));
                        i++;
                    }
                    int ii;
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
                                ii = 0;
                                foreach (string line in list[i])
                                {
                                    dataGridView1.Rows[ii].Cells[i].Value = (ii < list[i].Length) ? list[i][ii] : null;
                                    ii++;
                                }
                            }
                        }
                    }
                    //Resize the form
                    //Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + panel1.Width + 60;
                    if (numberOfColumn > maxNumberOfColumnVisible)
                        dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * maxNumberOfColumnVisible + 25;
                    else
                        dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + 25;
                }
            }

            else if (type != "All" && category == "All")
            {
                List<string[]> list = new List<string[]>();
                i = 0;
                i = folderNames.IndexOf(type);
                foreach (string file in structure[i])
                {
                    //MessageBox.Show("Folder: " + folder + " File: " + file);
                    list.Add(NewColumn(type, folderNames[i], file));
                }

                int ii;
                string[] lines;
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
                            ii = 0;
                            foreach (string line in list[i])
                            {
                                dataGridView1.Rows[ii].Cells[i].Value = (ii < list[i].Length) ? list[i][ii] : null;
                                ii++;
                            }
                        }
                    }
                }
                //Resize the form
                //Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + panel1.Width + 60;
                if (numberOfColumn > maxNumberOfColumnVisible)
                    dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * maxNumberOfColumnVisible + 25;
                else
                    dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + 25;
            }
            //All doesn't have a category
            else if (type == "All" && category != "All")
            {
                //Resize the form
                //Width = panel1.Width + 60;
                dataGridView1.Width = 0;
            }
            else if (type != "All" && category != "All")
            {
                List<string[]> list = new List<string[]>();
                i = 0;
                i = folderNames.IndexOf(type);
                //MessageBox.Show(folder);
                list.Add(NewColumn(type, type, category));

                int ii;
                string[] lines;
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
                            ii = 0;
                            foreach (string line in list[i])
                            {
                                dataGridView1.Rows[ii].Cells[i].Value = (ii < list[i].Length) ? list[i][ii] : null;
                                ii++;
                            }
                        }
                    }
                }
                //Resize the form
                //Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + panel1.Width + 60;
                if (numberOfColumn > maxNumberOfColumnVisible)
                    dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * maxNumberOfColumnVisible + 25;
                else
                    dataGridView1.Width = dataGridView1.Columns[numberOfColumn - 1].Width * (numberOfColumn) + 25;
            }
            else
            {
                MessageBox.Show("Wtf");
            }
        }

        public string[] NewColumn(string type, string folder, string file = "")
        {
            //string[] lines = File.ReadAllText(folder + "ㅤ/" + file + ".txt").Split(folderDelimiter);
            string[] lines;

            int index;
            if (file != "")
                lines = File.ReadAllText(folder + folderDelimiter + "/" + file + ".txt").Split(dataDelimiter);
            else
            {
                //MessageBox.Show(i.ToString());
                index = folderNames.IndexOf(folder);
                List<string> combinedLines = new List<string>(); // Create a list to store the combined elements

                foreach (string file2 in structure[i])
                {
                    // Read and split data from each file2
                    string[] file2Lines = File.ReadAllText(Path.Combine(folder, file2 + ".txt")).Split(dataDelimiter);

                    // Add the elements from file2Lines to combinedLines
                    combinedLines.AddRange(file2Lines);
                }

                // Convert the list to an array if needed
                lines = combinedLines.ToArray();
            }
            //MessageBox.Show(folder + "ㅤ/" + file + ".txt");
            dataGridView1.ColumnCount = numberOfColumn + 1;
            if (type == "All")
                dataGridView1.Columns[numberOfColumn].Name = Trim.TrimCharacter(folder, "0123456789") + "\n" + Trim.TrimCharacter(file, "0123456789");
            else
                dataGridView1.Columns[numberOfColumn].Name = Trim.TrimCharacter(file, "0123456789");
            numberOfColumn++;
            return lines;
        }

        public void InitCBTypes()
        {
            List<string> allTypes = new List<string>();
            allTypes.Add("All");
            for (i = 0; i < folderNames.Count; i++)
            {
                allTypes.Add(folderNames[i]);
            }
            foreach (string type in allTypes)
            {
                CBTypes.Items.Add(Trim.TrimCharacter(type, "0123456789"));
            }
            CBTypes.SelectedIndex = startTypeIndex;

            string pattern = @"(\d+)([A-Za-z]+)";
            string input = folderNames[CBTypes.SelectedIndex - 1];

            if (Regex.IsMatch(input, pattern))
            {
                InitCBCategories(CBTypes.SelectedIndex.ToString() + Trim.TrimCharacter(folderNames[CBTypes.SelectedIndex - 1], "0123456789"));
            }
            else
            {
                InitCBCategories(CBTypes.SelectedItem.ToString());
            }
        }

        public void InitCBCategories(string type)
        {
            CBCategories.Items.Clear();

            List<string> allCategories = new List<string>();

            if (type == "All")
            {
                allCategories.Add("");
                foreach (string category in allCategories)
                {
                    CBCategories.Items.Add(Trim.TrimCharacter(category, "0123456789"));
                }
                CBCategories.SelectedIndex = 0;
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
                CBCategories.SelectedIndex = 0;
            }
        }

        //CBType
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CBTypes.SelectedItem.ToString() == "All" || CBTypes.SelectedItem.ToString() == "")
            {
                InitCBCategories(CBTypes.SelectedItem.ToString());
            }
            else
            {

                //InitCBCategories(folderNames[CBTypes.SelectedIndex - 1]);
                string pattern = @"(\d+)([A-Za-z]+)";
                string input = folderNames[CBTypes.SelectedIndex - 1];

                if (Regex.IsMatch(input, pattern))
                {
                    InitCBCategories(CBTypes.SelectedIndex.ToString() + Trim.TrimCharacter(folderNames[CBTypes.SelectedIndex - 1], "0123456789"));
                }
                else
                {
                    InitCBCategories(CBTypes.SelectedItem.ToString());
                }
            }
        }

        //CBCategory
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (CBCategories.SelectedItem.ToString() == "All" || CBCategories.SelectedItem.ToString() == "")
            {

                if (CBTypes.SelectedItem.ToString() == "All" || CBTypes.SelectedItem.ToString() == "")
                {
                    InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedItem.ToString());
                }
                else
                {
                    string pattern = @"(\d+)([A-Za-z]+)";
                    string input = folderNames[CBTypes.SelectedIndex - 1];

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
            else
            {
                string pattern = @"(\d+)([A-Za-z]+)";
                string input = structure[CBTypes.SelectedIndex - 1][CBCategories.SelectedIndex - 1];
                if (Regex.IsMatch(input, pattern))
                {
                    //MessageBox.Show("meow " + input);
                    input = folderNames[CBTypes.SelectedIndex - 1];
                    //MessageBox.Show("here " + input);
                    if (Regex.IsMatch(input, pattern))
                    {
                        InitDatagridView(CBTypes.SelectedIndex.ToString() + Trim.TrimCharacter(CBTypes.SelectedItem.ToString(), "0123456789"), CBCategories.SelectedIndex.ToString() + Trim.TrimCharacter(CBCategories.SelectedItem.ToString(), "0123456789"));
                    }
                    else
                    {
                        InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedIndex.ToString() + Trim.TrimCharacter(CBCategories.SelectedItem.ToString(), "0123456789"));
                    }

                }
                else
                {
                    //MessageBox.Show("Hello");
                    input = folderNames[CBTypes.SelectedIndex - 1];
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
        private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check if it's a valid cell
            {
                // Get the cell value
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cellValue != null)
                {
                    // Copy the cell value to the clipboard
                    Clipboard.SetText(cellValue.ToString());
                    var cellRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    toolTip.Show(cellValue.ToString() + " copied", this, cellRectangle.Left + cellRectangle.Width + 200, cellRectangle.Top + cellRectangle.Height, 2000);
                }
                if (e.Button == MouseButtons.Right)
                {
                    Application.Exit();
                    //this.Close();
                }
            }
        }


        //Preparing layout
        private void CBTextAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            changedProgrammatically = true;
            if (CBTextAction.Text == "Random text generator")
            {
                BTAction.Text = "Generate";
            }
            else
            {
                BTAction.Text = CBTextAction.Text;
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
            }
            else
            {
                TBParam.Visible = true;
                LBParamCount.Visible = true;
                RegexMaxRandomLength.Visible = false;
                RegexMinRandomLength.Visible = false;
                ZalgoIntensity.Visible = false;
                label3.Visible = false;
            }
        }
        string originalText;
        bool changedProgrammatically = false;
        static Random random = new Random();



        private void BTEmpty_Click(object sender, EventArgs e)
        {
            changedProgrammatically = false;
            TBInput.Text = null;
        }
        //Action button click
        private void BTAction_Click(object sender, EventArgs e)
        {
            SimpleEncrypt simpleEncrypt = new SimpleEncrypt();
            if (CBTextAction.SelectedItem.ToString() == "Z̷̤̣̲ͮͤ͜ą̲̬̲̪̠ͮļ̞̯̺ͧ͞͡g̷̶̴̴̟ͥͫo̯̱ͪͭͩ͜͞f̡̢̤̤̞ͨ͜y̵̜̲̬̞̱͡")
            {
                if (ZalgoIntensity.Value < 1)
                    ZalgoIntensity.Value = 1;
                else if (ZalgoIntensity.Value > 10)
                    ZalgoIntensity.Value = 10;
                TBInput.Text = ZalgoStuffs.ZalgoFyText(originalText, ZalgoIntensity.Value);
                //Program changed
                changedProgrammatically = true;
            }
            else if (CBTextAction.SelectedItem.ToString() == "uʍop ǝpısd∩")
            {
                string upsideDownString = UpsideDown.MakeUpsideDown(TBInput.Text);
                TBInput.Text = upsideDownString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Downside up")
            {
                string upsideDownString = UpsideDown.MakeDownsideUp(TBInput.Text);
                TBInput.Text = upsideDownString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Mirror")
            {
                string mirroredString = UpsideDown.MirrorLeftRight(TBInput.Text);
                TBInput.Text = mirroredString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "esreveR")
            {
                string reversedString = UpsideDown.Reverse(TBInput.Text);
                TBInput.Text = reversedString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "raNDomly CApItAlizE")
            {
                TBInput.Text = RandomCapital.RandomlyCapitalize(TBInput.Text);
            }
            else if (CBTextAction.SelectedItem.ToString() == "TO UPPER CASE")
            {
                TBInput.Text = TBInput.Text.ToUpper();
            }
            else if (CBTextAction.SelectedItem.ToString() == "to lower case")
            {
                TBInput.Text = TBInput.Text.ToLower();
            }
            else if (CBTextAction.SelectedItem.ToString() == "To Title Case")
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                TBInput.Text = textInfo.ToTitleCase(TBInput.Text);
            }
            else if (CBTextAction.SelectedItem.ToString() == "Trim string")
            {
                string sentence = TBInput.Text;
                string stringToRemove = TBParam.Text;
                string trimmedSentence = Trim.TrimString(sentence, stringToRemove);
                TBInput.Text = trimmedSentence;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Trim chrcter")
            {
                string sentence = TBInput.Text;
                string stringToRemove = TBParam.Text;
                string trimmedSentence = Trim.TrimCharacter(sentence, stringToRemove);
                TBInput.Text = trimmedSentence;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Simple cipher")
            {
                string ruleFilePath = AppDomain.CurrentDomain.BaseDirectory + "/Cipher.txt";
                simpleEncrypt.LoadCipherRules(ruleFilePath);
                string originalString = TBInput.Text;
                string cipheredString = simpleEncrypt.SimpleCipher(originalString);
                TBInput.Text = cipheredString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Simple decipher")
            {
                string ruleFilePath = AppDomain.CurrentDomain.BaseDirectory + "/Cipher.txt";
                simpleEncrypt.LoadCipherRules(ruleFilePath);
                string originalString = TBInput.Text;
                string decipheredString = simpleEncrypt.SimpleDecipher(originalString);

                TBInput.Text = decipheredString;
            }
            else if (CBTextAction.SelectedItem.ToString() == "Random text generator")
            {
                if (RegexMinRandomLength.Value > RegexMaxRandomLength.Value)
                    MessageBox.Show("Min cannot greater than max");
                else
                    TBInput.Text = RandomText.RandomTextGenerate(TBParam.Text, Convert.ToInt32(RegexMinRandomLength.Value), Convert.ToInt32(RegexMaxRandomLength.Value));
            }

            else if (CBTextAction.SelectedItem.ToString() == "Regex check")
            {
                var regex = new System.Text.RegularExpressions.Regex(TBParam.Text); // Your regex here
                if (regex.IsMatch(TBInput.Text))
                {
                    MessageBox.Show(TBInput.Text + " matched");
                    //Console.WriteLine($"Generated string '{result}' matches the regex.");
                }
                else
                {
                    MessageBox.Show(TBInput.Text + " doesn't match");
                    //Console.WriteLine($"Generated string '{result}' does not match the regex.");
                }
            }
            else
            {
                MessageBox.Show("TODO");
            }
        }

        //Right click to exit
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Application.Exit();
            }
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
                //Application.Exit();
            }
        }

        //Left click to drag
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
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
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null)
            {
                cell.ToolTipText = cell.Value.ToString() + "\n\nLeft click to copy\nRight click to copy and exit";
            }
            else
            {
                cell.ToolTipText = "";
            }
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            /*if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Ensure the row and column index is valid
            {
                // Set the ToolTip text to the cell's content
                e.ToolTipText = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }*/
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Ensure the row and column index are valid
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellValue = cell.Value.ToString();

                // Create a ToolTip and associate with the Form container.
                System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();

                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;

                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                // Set up the ToolTip text for the cell.
                toolTip1.SetToolTip(dataGridView1, cellValue);
            }*/
        }

        private void TBParam_TextChanged(object sender, EventArgs e)
        {
            //LBParamCount.Text = TBParam.Text.Length.ToString();
            string[] words = TBParam.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\'', '\\', '(', '{', '[', '"' }, StringSplitOptions.RemoveEmptyEntries);
            if (words != null && words.Length > 0)
                toolTip4.SetToolTip(TBParam, TBParam.Text.Length.ToString() + " characters\n" + words.Length + " words\n" + TBParam.Text);
            else toolTip4.SetToolTip(TBParam, "Parameters to process inputs with based on actions\nHover while typing to show contents");

        }

        private void TBInput_TextChanged(object sender, EventArgs e)
        {
            //Ignore if change is not made by human
            if (!changedProgrammatically)
            {
                originalText = TBInput.Text;
            }
            //LBInputCount.Text = TBInput.Text.Length.ToString();

            string[] words = TBInput.Text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ':', ';', '!', '?', '/', '|', '\'', '\\', '(', '{', '[', '"' }, StringSplitOptions.RemoveEmptyEntries);

            if (words != null && words.Length > 0)
                toolTip3.SetToolTip(TBInput, " " + TBInput.Text.Length.ToString() + " characters\n " + words.Length + " words\n" + TBInput.Text);
            else toolTip3.SetToolTip(TBInput, "Output and input to be processed are here\nHover while typing to show contents");

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

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                string columnHeader = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                //MessageBox.Show($"TODO\n{columnHeader}");
                headerExpand = !headerExpand;
                if (CBCategories.SelectedItem.ToString() == "All" || CBCategories.SelectedItem.ToString() == "")
                    InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedItem.ToString());
                else
                    InitDatagridView(CBTypes.SelectedItem.ToString(), CBCategories.SelectedIndex.ToString() + Trim.TrimCharacter(CBCategories.SelectedItem.ToString(), "0123456789"));
            }

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
