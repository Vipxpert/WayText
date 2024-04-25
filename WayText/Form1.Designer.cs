namespace Emoji
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            CBCategories = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            CBTypes = new ComboBox();
            TBInput = new TextBox();
            BTAction = new Button();
            ZalgoIntensity = new NumericUpDown();
            label3 = new Label();
            BTEmpty = new Button();
            CBTextAction = new ComboBox();
            LBAction = new Label();
            RegexMinRandomLength = new NumericUpDown();
            RegexMaxRandomLength = new NumericUpDown();
            LBRandom = new Label();
            TBParam = new TextBox();
            LBParamCount = new Label();
            panel1 = new Panel();
            TBReplace = new TextBox();
            BTRevert = new Button();
            LLManual = new LinkLabel();
            LLGit = new LinkLabel();
            LLData = new LinkLabel();
            notiCopyTool = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            settingsToolStripMenuItem = new ToolStripMenuItem();
            restartToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            runInCMDToolStripMenuItem = new ToolStripMenuItem();
            openDataFileToolStripMenuItem = new ToolStripMenuItem();
            openDataFolderToolStripMenuItem = new ToolStripMenuItem();
            openSettingsToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            restartAppToolStripMenuItem = new ToolStripMenuItem();
            hideToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            BTLeftScroll = new Button();
            BTRightScoll = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).BeginInit();
            panel1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(351, 5);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(194, 533);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            dataGridView1.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView1.CellToolTipTextNeeded += dataGridView1_CellToolTipTextNeeded;
            dataGridView1.ColumnAdded += dataGridView1_ColumnAdded;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            dataGridView1.MouseDown += dataGridView1_MouseDown;
            dataGridView1.MouseMove += dataGridView1_MouseMove;
            // 
            // CBCategories
            // 
            CBCategories.FormattingEnabled = true;
            CBCategories.Location = new Point(136, 103);
            CBCategories.Margin = new Padding(4, 3, 4, 3);
            CBCategories.Name = "CBCategories";
            CBCategories.Size = new Size(191, 33);
            CBCategories.TabIndex = 2;
            CBCategories.SelectedIndexChanged += CBCategory_SelectedIndexChanged;
            CBCategories.MouseLeave += CBCategories_MouseLeave;
            CBCategories.MouseHover += CBCategories_MouseHover;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 107);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 3;
            label1.Text = "Category";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 47);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(49, 25);
            label2.TabIndex = 4;
            label2.Text = "Type";
            // 
            // CBTypes
            // 
            CBTypes.FormattingEnabled = true;
            CBTypes.Location = new Point(136, 43);
            CBTypes.Margin = new Padding(4, 3, 4, 3);
            CBTypes.Name = "CBTypes";
            CBTypes.Size = new Size(191, 33);
            CBTypes.TabIndex = 5;
            CBTypes.SelectedIndexChanged += CBType_SelectedIndexChanged_1;
            CBTypes.MouseLeave += CBTypes_MouseLeave;
            CBTypes.MouseHover += CBTypes_MouseHover;
            // 
            // TBInput
            // 
            TBInput.AcceptsReturn = true;
            TBInput.AcceptsTab = true;
            TBInput.AllowDrop = true;
            TBInput.Location = new Point(25, 335);
            TBInput.Margin = new Padding(4, 3, 4, 3);
            TBInput.Multiline = true;
            TBInput.Name = "TBInput";
            TBInput.PlaceholderText = "Input/Output";
            TBInput.Size = new Size(302, 36);
            TBInput.TabIndex = 6;
            TBInput.Click += TBInput_Click;
            TBInput.MouseClick += TBInput_MouseClick;
            TBInput.TextChanged += TBInput_TextChanged;
            TBInput.DragDrop += TBInput_DragDrop;
            TBInput.DragEnter += TBInput_DragEnter;
            TBInput.KeyPress += TBInput_KeyPress;
            TBInput.MouseLeave += TBInput_MouseLeave;
            TBInput.MouseHover += TBInput_MouseHover;
            // 
            // BTAction
            // 
            BTAction.AutoSize = true;
            BTAction.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BTAction.Location = new Point(194, 455);
            BTAction.Margin = new Padding(4, 3, 4, 3);
            BTAction.Name = "BTAction";
            BTAction.Size = new Size(82, 35);
            BTAction.TabIndex = 7;
            BTAction.Text = "Zalgofy";
            BTAction.UseVisualStyleBackColor = true;
            BTAction.Click += BTAction_Click;
            // 
            // ZalgoIntensity
            // 
            ZalgoIntensity.Location = new Point(124, 277);
            ZalgoIntensity.Margin = new Padding(4, 3, 4, 3);
            ZalgoIntensity.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            ZalgoIntensity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ZalgoIntensity.Name = "ZalgoIntensity";
            ZalgoIntensity.Size = new Size(74, 31);
            ZalgoIntensity.TabIndex = 8;
            ZalgoIntensity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            ZalgoIntensity.ValueChanged += ZalgoIntensity_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 282);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(79, 25);
            label3.TabIndex = 9;
            label3.Text = "Intensity";
            // 
            // BTEmpty
            // 
            BTEmpty.AutoSize = true;
            BTEmpty.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BTEmpty.Location = new Point(33, 455);
            BTEmpty.Margin = new Padding(4, 3, 4, 3);
            BTEmpty.Name = "BTEmpty";
            BTEmpty.Size = new Size(73, 35);
            BTEmpty.TabIndex = 10;
            BTEmpty.Text = "Empty";
            BTEmpty.UseVisualStyleBackColor = true;
            BTEmpty.Click += BTEmpty_Click;
            BTEmpty.MouseClick += BTEmpty_MouseClick;
            BTEmpty.MouseDown += BTEmpty_MouseDown;
            // 
            // CBTextAction
            // 
            CBTextAction.FormattingEnabled = true;
            CBTextAction.Location = new Point(101, 388);
            CBTextAction.Margin = new Padding(4, 5, 4, 5);
            CBTextAction.Name = "CBTextAction";
            CBTextAction.Size = new Size(227, 33);
            CBTextAction.TabIndex = 11;
            CBTextAction.SelectedIndexChanged += CBTextAction_SelectedIndexChanged;
            // 
            // LBAction
            // 
            LBAction.AutoSize = true;
            LBAction.Location = new Point(27, 398);
            LBAction.Margin = new Padding(4, 0, 4, 0);
            LBAction.Name = "LBAction";
            LBAction.Size = new Size(63, 25);
            LBAction.TabIndex = 12;
            LBAction.Text = "Action";
            // 
            // RegexMinRandomLength
            // 
            RegexMinRandomLength.Location = new Point(82, 226);
            RegexMinRandomLength.Margin = new Padding(4, 3, 4, 3);
            RegexMinRandomLength.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            RegexMinRandomLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMinRandomLength.Name = "RegexMinRandomLength";
            RegexMinRandomLength.Size = new Size(74, 31);
            RegexMinRandomLength.TabIndex = 14;
            RegexMinRandomLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMinRandomLength.Visible = false;
            // 
            // RegexMaxRandomLength
            // 
            RegexMaxRandomLength.Location = new Point(202, 226);
            RegexMaxRandomLength.Margin = new Padding(4, 3, 4, 3);
            RegexMaxRandomLength.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            RegexMaxRandomLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMaxRandomLength.Name = "RegexMaxRandomLength";
            RegexMaxRandomLength.Size = new Size(74, 31);
            RegexMaxRandomLength.TabIndex = 15;
            RegexMaxRandomLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMaxRandomLength.Visible = false;
            // 
            // LBRandom
            // 
            LBRandom.AutoSize = true;
            LBRandom.Location = new Point(101, 168);
            LBRandom.Margin = new Padding(4, 0, 4, 0);
            LBRandom.Name = "LBRandom";
            LBRandom.Size = new Size(169, 25);
            LBRandom.TabIndex = 16;
            LBRandom.Text = "Random text length";
            LBRandom.Visible = false;
            // 
            // TBParam
            // 
            TBParam.AcceptsReturn = true;
            TBParam.AcceptsTab = true;
            TBParam.AllowDrop = true;
            TBParam.Location = new Point(24, 279);
            TBParam.Margin = new Padding(4, 5, 4, 5);
            TBParam.Multiline = true;
            TBParam.Name = "TBParam";
            TBParam.PlaceholderText = "Parameter";
            TBParam.Size = new Size(303, 36);
            TBParam.TabIndex = 17;
            TBParam.Visible = false;
            TBParam.TextChanged += TBParam_TextChanged;
            TBParam.DragDrop += TBParam_DragDrop;
            TBParam.DragEnter += TBParam_DragEnter;
            TBParam.KeyPress += TBParam_KeyPress;
            TBParam.MouseLeave += TBParam_MouseLeave;
            TBParam.MouseHover += TBParam_MouseHover;
            // 
            // LBParamCount
            // 
            LBParamCount.AutoSize = true;
            LBParamCount.Location = new Point(297, 290);
            LBParamCount.Margin = new Padding(4, 0, 4, 0);
            LBParamCount.Name = "LBParamCount";
            LBParamCount.Size = new Size(0, 25);
            LBParamCount.TabIndex = 19;
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(TBReplace);
            panel1.Controls.Add(TBParam);
            panel1.Controls.Add(TBInput);
            panel1.Controls.Add(BTRevert);
            panel1.Controls.Add(LLManual);
            panel1.Controls.Add(LLGit);
            panel1.Controls.Add(LLData);
            panel1.Controls.Add(LBParamCount);
            panel1.Controls.Add(LBRandom);
            panel1.Controls.Add(RegexMaxRandomLength);
            panel1.Controls.Add(RegexMinRandomLength);
            panel1.Controls.Add(LBAction);
            panel1.Controls.Add(CBTextAction);
            panel1.Controls.Add(BTEmpty);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ZalgoIntensity);
            panel1.Controls.Add(BTAction);
            panel1.Controls.Add(CBTypes);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(CBCategories);
            panel1.Location = new Point(1, 5);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(343, 533);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            panel1.MouseEnter += panel1_MouseEnter;
            panel1.MouseHover += panel1_MouseHover;
            // 
            // TBReplace
            // 
            TBReplace.AcceptsReturn = true;
            TBReplace.AcceptsTab = true;
            TBReplace.AllowDrop = true;
            TBReplace.Location = new Point(25, 221);
            TBReplace.Margin = new Padding(4, 5, 4, 5);
            TBReplace.Multiline = true;
            TBReplace.Name = "TBReplace";
            TBReplace.PlaceholderText = "Replace";
            TBReplace.Size = new Size(302, 36);
            TBReplace.TabIndex = 23;
            TBReplace.Visible = false;
            TBReplace.TextChanged += TBReplace_TextChanged;
            TBReplace.DragDrop += TBReplace_DragDrop;
            TBReplace.DragEnter += TBReplace_DragEnter;
            TBReplace.MouseLeave += TBReplace_MouseLeave;
            TBReplace.MouseHover += TBReplace_MouseHover;
            // 
            // BTRevert
            // 
            BTRevert.AutoSize = true;
            BTRevert.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BTRevert.Location = new Point(114, 455);
            BTRevert.Margin = new Padding(4, 5, 4, 5);
            BTRevert.Name = "BTRevert";
            BTRevert.Size = new Size(71, 35);
            BTRevert.TabIndex = 24;
            BTRevert.Text = "Revert";
            BTRevert.UseVisualStyleBackColor = true;
            BTRevert.Click += BTRevert_Click;
            // 
            // LLManual
            // 
            LLManual.AutoSize = true;
            LLManual.Location = new Point(101, 502);
            LLManual.Margin = new Padding(4, 0, 4, 0);
            LLManual.Name = "LLManual";
            LLManual.Size = new Size(70, 25);
            LLManual.TabIndex = 22;
            LLManual.TabStop = true;
            LLManual.Text = "Manual";
            LLManual.LinkClicked += LLManual_LinkClicked;
            // 
            // LLGit
            // 
            LLGit.AutoSize = true;
            LLGit.Location = new Point(177, 502);
            LLGit.Margin = new Padding(4, 0, 4, 0);
            LLGit.Name = "LLGit";
            LLGit.Size = new Size(68, 25);
            LLGit.TabIndex = 21;
            LLGit.TabStop = true;
            LLGit.Text = "GitHub";
            LLGit.LinkClicked += LLGit_LinkClicked;
            // 
            // LLData
            // 
            LLData.AutoSize = true;
            LLData.Location = new Point(14, 502);
            LLData.Margin = new Padding(4, 0, 4, 0);
            LLData.Name = "LLData";
            LLData.Size = new Size(84, 25);
            LLData.TabIndex = 20;
            LLData.TabStop = true;
            LLData.Text = "Directory";
            LLData.LinkClicked += LLData_LinkClicked;
            // 
            // notiCopyTool
            // 
            notiCopyTool.BalloonTipText = "Meow";
            notiCopyTool.BalloonTipTitle = "Mao";
            notiCopyTool.ContextMenuStrip = contextMenuStrip1;
            notiCopyTool.Icon = (Icon)resources.GetObject("notiCopyTool.Icon");
            notiCopyTool.Text = "Double click to open";
            notiCopyTool.BalloonTipShown += notifyIcon1_BalloonTipShown;
            notiCopyTool.DoubleClick += notifyIcon1_DoubleClick;
            notiCopyTool.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, restartToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(149, 100);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(148, 32);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new Size(148, 32);
            restartToolStripMenuItem.Text = "Restart";
            restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(148, 32);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.ImageScalingSize = new Size(24, 24);
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { runInCMDToolStripMenuItem, openDataFileToolStripMenuItem, openDataFolderToolStripMenuItem, openSettingsToolStripMenuItem, refreshToolStripMenuItem, restartAppToolStripMenuItem, hideToolStripMenuItem, exitToolStripMenuItem1 });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(221, 260);
            // 
            // runInCMDToolStripMenuItem
            // 
            runInCMDToolStripMenuItem.Name = "runInCMDToolStripMenuItem";
            runInCMDToolStripMenuItem.Size = new Size(220, 32);
            runInCMDToolStripMenuItem.Text = "Run in CMD";
            runInCMDToolStripMenuItem.Click += runInCMDToolStripMenuItem_Click;
            runInCMDToolStripMenuItem.MouseDown += runInCMDToolStripMenuItem_MouseDown;
            // 
            // openDataFileToolStripMenuItem
            // 
            openDataFileToolStripMenuItem.Name = "openDataFileToolStripMenuItem";
            openDataFileToolStripMenuItem.Size = new Size(220, 32);
            openDataFileToolStripMenuItem.Text = "Open data file";
            openDataFileToolStripMenuItem.Click += openDataFileToolStripMenuItem_Click;
            // 
            // openDataFolderToolStripMenuItem
            // 
            openDataFolderToolStripMenuItem.Name = "openDataFolderToolStripMenuItem";
            openDataFolderToolStripMenuItem.Size = new Size(220, 32);
            openDataFolderToolStripMenuItem.Text = "Open data folder";
            openDataFolderToolStripMenuItem.Click += openDataFolderToolStripMenuItem_Click;
            // 
            // openSettingsToolStripMenuItem
            // 
            openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
            openSettingsToolStripMenuItem.Size = new Size(220, 32);
            openSettingsToolStripMenuItem.Text = "Settings";
            openSettingsToolStripMenuItem.Click += openSettingsToolStripMenuItem_Click;
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(220, 32);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // restartAppToolStripMenuItem
            // 
            restartAppToolStripMenuItem.Name = "restartAppToolStripMenuItem";
            restartAppToolStripMenuItem.Size = new Size(220, 32);
            restartAppToolStripMenuItem.Text = "Restart app";
            restartAppToolStripMenuItem.Click += restartAppToolStripMenuItem_Click;
            // 
            // hideToolStripMenuItem
            // 
            hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            hideToolStripMenuItem.Size = new Size(220, 32);
            hideToolStripMenuItem.Text = "Hide app";
            hideToolStripMenuItem.Click += hideToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(220, 32);
            exitToolStripMenuItem1.Text = "Exit app";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // BTLeftScroll
            // 
            BTLeftScroll.Location = new Point(362, 471);
            BTLeftScroll.Name = "BTLeftScroll";
            BTLeftScroll.Size = new Size(28, 34);
            BTLeftScroll.TabIndex = 4;
            BTLeftScroll.Text = "<";
            BTLeftScroll.UseVisualStyleBackColor = true;
            BTLeftScroll.Click += BTLeftScroll_Click;
            BTLeftScroll.MouseClick += BTLeftScroll_MouseClick;
            BTLeftScroll.MouseDown += BTLeftScroll_MouseDown;
            // 
            // BTRightScoll
            // 
            BTRightScoll.Location = new Point(396, 471);
            BTRightScoll.Name = "BTRightScoll";
            BTRightScoll.Size = new Size(27, 34);
            BTRightScoll.TabIndex = 5;
            BTRightScoll.Text = ">";
            BTRightScoll.UseVisualStyleBackColor = true;
            BTRightScoll.Click += BTRightScoll_Click;
            BTRightScoll.MouseClick += BTRightScoll_MouseClick;
            BTRightScoll.MouseDown += BTRightScoll_MouseDown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(549, 545);
            ControlBox = false;
            Controls.Add(BTRightScoll);
            Controls.Add(BTLeftScroll);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Form1";
            Load += Form1_Load;
            MouseClick += Form1_MouseClick;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).EndInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox CBCategories;
        private Label label1;
        private Label label2;
        private ComboBox CBTypes;
        private TextBox TBInput;
        private Button BTAction;
        private NumericUpDown ZalgoIntensity;
        private Label label3;
        private Button BTEmpty;
        private ComboBox CBTextAction;
        private Label LBAction;
        private NumericUpDown RegexMinRandomLength;
        private NumericUpDown RegexMaxRandomLength;
        private Label LBRandom;
        private TextBox TBParam;
        private Label LBParamCount;
        private Panel panel1;
        private NotifyIcon notiCopyTool;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private LinkLabel LLData;
        private LinkLabel LLGit;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem runInCMDToolStripMenuItem;
        private ToolStripMenuItem hideToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem openDataFileToolStripMenuItem;
        private ToolStripMenuItem openDataFolderToolStripMenuItem;
        private ToolStripMenuItem restartAppToolStripMenuItem;
        private ToolStripMenuItem openSettingsToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private LinkLabel LLManual;
        private TextBox TBReplace;
        private Button BTRevert;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private Button BTLeftScroll;
        private Button BTRightScoll;
    }
}