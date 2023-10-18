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
            label4 = new Label();
            RegexMinRandomLength = new NumericUpDown();
            RegexMaxRandomLength = new NumericUpDown();
            LBRandom = new Label();
            TBParam = new TextBox();
            LBParamCount = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(309, 7);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(155, 427);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            dataGridView1.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView1.CellToolTipTextNeeded += dataGridView1_CellToolTipTextNeeded;
            dataGridView1.ColumnAdded += dataGridView1_ColumnAdded;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            // 
            // CBCategories
            // 
            CBCategories.FormattingEnabled = true;
            CBCategories.Location = new Point(109, 83);
            CBCategories.Name = "CBCategories";
            CBCategories.Size = new Size(158, 28);
            CBCategories.TabIndex = 2;
            CBCategories.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 85);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 3;
            label1.Text = "Category";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 37);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 4;
            label2.Text = "Type";
            // 
            // CBTypes
            // 
            CBTypes.FormattingEnabled = true;
            CBTypes.Location = new Point(109, 35);
            CBTypes.Name = "CBTypes";
            CBTypes.Size = new Size(158, 28);
            CBTypes.TabIndex = 5;
            CBTypes.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // TBInput
            // 
            TBInput.Location = new Point(22, 264);
            TBInput.Name = "TBInput";
            TBInput.PlaceholderText = "Input";
            TBInput.Size = new Size(245, 27);
            TBInput.TabIndex = 6;
            TBInput.Click += TBInput_Click;
            TBInput.TextChanged += TBInput_TextChanged;
            // 
            // BTAction
            // 
            BTAction.AutoSize = true;
            BTAction.Location = new Point(122, 369);
            BTAction.Name = "BTAction";
            BTAction.Size = new Size(94, 40);
            BTAction.TabIndex = 7;
            BTAction.Text = "Zalgofy";
            BTAction.UseVisualStyleBackColor = true;
            BTAction.Click += BTAction_Click;
            // 
            // ZalgoIntensity
            // 
            ZalgoIntensity.Location = new Point(99, 221);
            ZalgoIntensity.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ZalgoIntensity.Name = "ZalgoIntensity";
            ZalgoIntensity.Size = new Size(59, 27);
            ZalgoIntensity.TabIndex = 8;
            ZalgoIntensity.ValueChanged += ZalgoIntensity_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 225);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 9;
            label3.Text = "Intensity";
            // 
            // BTEmpty
            // 
            BTEmpty.AutoSize = true;
            BTEmpty.Location = new Point(22, 369);
            BTEmpty.Name = "BTEmpty";
            BTEmpty.Size = new Size(94, 40);
            BTEmpty.TabIndex = 10;
            BTEmpty.Text = "Empty";
            BTEmpty.UseVisualStyleBackColor = true;
            BTEmpty.Click += BTEmpty_Click;
            // 
            // CBTextAction
            // 
            CBTextAction.FormattingEnabled = true;
            CBTextAction.Location = new Point(77, 320);
            CBTextAction.Margin = new Padding(3, 4, 3, 4);
            CBTextAction.Name = "CBTextAction";
            CBTextAction.Size = new Size(186, 28);
            CBTextAction.TabIndex = 11;
            CBTextAction.SelectedIndexChanged += CBTextAction_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 324);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 12;
            label4.Text = "Action";
            // 
            // RegexMinRandomLength
            // 
            RegexMinRandomLength.Location = new Point(56, 173);
            RegexMinRandomLength.Name = "RegexMinRandomLength";
            RegexMinRandomLength.Size = new Size(59, 27);
            RegexMinRandomLength.TabIndex = 14;
            RegexMinRandomLength.Visible = false;
            // 
            // RegexMaxRandomLength
            // 
            RegexMaxRandomLength.Location = new Point(157, 173);
            RegexMaxRandomLength.Name = "RegexMaxRandomLength";
            RegexMaxRandomLength.Size = new Size(59, 27);
            RegexMaxRandomLength.TabIndex = 15;
            RegexMaxRandomLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMaxRandomLength.Visible = false;
            // 
            // LBRandom
            // 
            LBRandom.AutoSize = true;
            LBRandom.Location = new Point(72, 135);
            LBRandom.Name = "LBRandom";
            LBRandom.Size = new Size(140, 20);
            LBRandom.TabIndex = 16;
            LBRandom.Text = "Random text length";
            LBRandom.Visible = false;
            // 
            // TBParam
            // 
            TBParam.Location = new Point(22, 221);
            TBParam.Margin = new Padding(3, 4, 3, 4);
            TBParam.Name = "TBParam";
            TBParam.PlaceholderText = "Parameter";
            TBParam.Size = new Size(245, 27);
            TBParam.TabIndex = 17;
            TBParam.Visible = false;
            TBParam.TextChanged += TBParam_TextChanged;
            // 
            // LBParamCount
            // 
            LBParamCount.AutoSize = true;
            LBParamCount.Location = new Point(238, 232);
            LBParamCount.Name = "LBParamCount";
            LBParamCount.Size = new Size(0, 20);
            LBParamCount.TabIndex = 19;
            // 
            // panel1
            // 
            panel1.Controls.Add(LBParamCount);
            panel1.Controls.Add(TBParam);
            panel1.Controls.Add(LBRandom);
            panel1.Controls.Add(RegexMaxRandomLength);
            panel1.Controls.Add(RegexMinRandomLength);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(CBTextAction);
            panel1.Controls.Add(BTEmpty);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ZalgoIntensity);
            panel1.Controls.Add(BTAction);
            panel1.Controls.Add(TBInput);
            panel1.Controls.Add(CBTypes);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(CBCategories);
            panel1.Location = new Point(11, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(290, 427);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(470, 443);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Form1";
            Load += Form1_Load;
            MouseClick += Form1_MouseClick;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).EndInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Label label4;
        private NumericUpDown RegexMinRandomLength;
        private NumericUpDown RegexMaxRandomLength;
        private Label LBRandom;
        private TextBox TBParam;
        private Label LBParamCount;
        private Panel panel1;
    }
}