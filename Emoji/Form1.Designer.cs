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
            panel1 = new Panel();
            TBParam = new TextBox();
            LBRandom = new Label();
            RegexMaxRandomLength = new NumericUpDown();
            RegexMinRandomLength = new NumericUpDown();
            label4 = new Label();
            CBTextAction = new ComboBox();
            BTEmpty = new Button();
            label3 = new Label();
            ZalgoIntensity = new NumericUpDown();
            BTAction = new Button();
            TBInput = new TextBox();
            CBTypes = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(270, 5);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(136, 320);
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
            CBCategories.Location = new Point(95, 62);
            CBCategories.Margin = new Padding(3, 2, 3, 2);
            CBCategories.Name = "CBCategories";
            CBCategories.Size = new Size(136, 23);
            CBCategories.TabIndex = 2;
            CBCategories.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // panel1
            // 
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
            panel1.Location = new Point(10, 5);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(254, 320);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // TBParam
            // 
            TBParam.Location = new Point(19, 166);
            TBParam.Name = "TBParam";
            TBParam.PlaceholderText = "Parameter";
            TBParam.Size = new Size(212, 23);
            TBParam.TabIndex = 17;
            TBParam.Visible = false;
            TBParam.TextChanged += TBParam_TextChanged;
            // 
            // LBRandom
            // 
            LBRandom.AutoSize = true;
            LBRandom.Location = new Point(63, 101);
            LBRandom.Name = "LBRandom";
            LBRandom.Size = new Size(112, 15);
            LBRandom.TabIndex = 16;
            LBRandom.Text = "Random text length";
            LBRandom.Visible = false;
            // 
            // RegexMaxRandomLength
            // 
            RegexMaxRandomLength.Location = new Point(137, 130);
            RegexMaxRandomLength.Margin = new Padding(3, 2, 3, 2);
            RegexMaxRandomLength.Name = "RegexMaxRandomLength";
            RegexMaxRandomLength.Size = new Size(52, 23);
            RegexMaxRandomLength.TabIndex = 15;
            RegexMaxRandomLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            RegexMaxRandomLength.Visible = false;
            // 
            // RegexMinRandomLength
            // 
            RegexMinRandomLength.Location = new Point(49, 130);
            RegexMinRandomLength.Margin = new Padding(3, 2, 3, 2);
            RegexMinRandomLength.Name = "RegexMinRandomLength";
            RegexMinRandomLength.Size = new Size(52, 23);
            RegexMinRandomLength.TabIndex = 14;
            RegexMinRandomLength.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 243);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 12;
            label4.Text = "Action";
            // 
            // CBTextAction
            // 
            CBTextAction.FormattingEnabled = true;
            CBTextAction.Location = new Point(67, 240);
            CBTextAction.Name = "CBTextAction";
            CBTextAction.Size = new Size(163, 23);
            CBTextAction.TabIndex = 11;
            CBTextAction.SelectedIndexChanged += CBTextAction_SelectedIndexChanged;
            // 
            // BTEmpty
            // 
            BTEmpty.AutoSize = true;
            BTEmpty.Location = new Point(19, 277);
            BTEmpty.Margin = new Padding(3, 2, 3, 2);
            BTEmpty.Name = "BTEmpty";
            BTEmpty.Size = new Size(82, 30);
            BTEmpty.TabIndex = 10;
            BTEmpty.Text = "Empty";
            BTEmpty.UseVisualStyleBackColor = true;
            BTEmpty.Click += BTEmpty_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 169);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 9;
            label3.Text = "Intensity";
            // 
            // ZalgoIntensity
            // 
            ZalgoIntensity.Location = new Point(87, 166);
            ZalgoIntensity.Margin = new Padding(3, 2, 3, 2);
            ZalgoIntensity.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ZalgoIntensity.Name = "ZalgoIntensity";
            ZalgoIntensity.Size = new Size(52, 23);
            ZalgoIntensity.TabIndex = 8;
            ZalgoIntensity.ValueChanged += ZalgoIntensity_ValueChanged;
            // 
            // BTAction
            // 
            BTAction.AutoSize = true;
            BTAction.Location = new Point(107, 277);
            BTAction.Margin = new Padding(3, 2, 3, 2);
            BTAction.Name = "BTAction";
            BTAction.Size = new Size(82, 30);
            BTAction.TabIndex = 7;
            BTAction.Text = "Zalgofy";
            BTAction.UseVisualStyleBackColor = true;
            BTAction.Click += BTAction_Click;
            // 
            // TBInput
            // 
            TBInput.Location = new Point(19, 198);
            TBInput.Margin = new Padding(3, 2, 3, 2);
            TBInput.Name = "TBInput";
            TBInput.PlaceholderText = "Input";
            TBInput.Size = new Size(212, 23);
            TBInput.TabIndex = 6;
            TBInput.TextChanged += TBInput_TextChanged;
            // 
            // CBTypes
            // 
            CBTypes.FormattingEnabled = true;
            CBTypes.Location = new Point(95, 26);
            CBTypes.Margin = new Padding(3, 2, 3, 2);
            CBTypes.Name = "CBTypes";
            CBTypes.Size = new Size(136, 23);
            CBTypes.TabIndex = 5;
            CBTypes.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 28);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 4;
            label2.Text = "Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 64);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "Category";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(411, 332);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Form1";
            Load += Form1_Load;
            MouseClick += Form1_MouseClick;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RegexMaxRandomLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)RegexMinRandomLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZalgoIntensity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox CBCategories;
        private Panel panel1;
        private Label label1;
        private ComboBox CBTypes;
        private Label label2;
        private Button BTAction;
        private TextBox TBInput;
        private Label label3;
        private NumericUpDown ZalgoIntensity;
        private Button BTEmpty;
        private ComboBox CBTextAction;
        private Label label4;
        private Label LBRandom;
        private NumericUpDown RegexMaxRandomLength;
        private NumericUpDown RegexMinRandomLength;
        private TextBox TBParam;
    }
}