namespace WOOOF
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
            timer1 = new System.Windows.Forms.Timer(components);
            btnRunScript = new Button();
            ScriptName = new TextBox();
            btnSelectScript = new Button();
            SleepTimer = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            numObjectFontsize = new NumericUpDown();
            label3 = new Label();
            numQualityFontsize = new NumericUpDown();
            panel1 = new Panel();
            button1 = new Button();
            cbHideEvents = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)SleepTimer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numObjectFontsize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQualityFontsize).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // btnRunScript
            // 
            btnRunScript.Location = new Point(427, 10);
            btnRunScript.Name = "btnRunScript";
            btnRunScript.Size = new Size(75, 23);
            btnRunScript.TabIndex = 0;
            btnRunScript.Text = "run script";
            btnRunScript.UseVisualStyleBackColor = true;
            btnRunScript.Click += btnRunScript_Click;
            // 
            // ScriptName
            // 
            ScriptName.Location = new Point(118, 10);
            ScriptName.Name = "ScriptName";
            ScriptName.Size = new Size(288, 23);
            ScriptName.TabIndex = 1;
            // 
            // btnSelectScript
            // 
            btnSelectScript.Location = new Point(9, 10);
            btnSelectScript.Name = "btnSelectScript";
            btnSelectScript.Size = new Size(103, 23);
            btnSelectScript.TabIndex = 2;
            btnSelectScript.Text = "select script";
            btnSelectScript.UseVisualStyleBackColor = true;
            btnSelectScript.Click += btnSelectScript_Click;
            // 
            // SleepTimer
            // 
            SleepTimer.Location = new Point(618, 10);
            SleepTimer.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            SleepTimer.Name = "SleepTimer";
            SleepTimer.Size = new Size(67, 23);
            SleepTimer.TabIndex = 3;
            SleepTimer.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(544, 14);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 4;
            label1.Text = "Sleep Timer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(712, 14);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 6;
            label2.Text = "Object Fontsize";
            // 
            // numObjectFontsize
            // 
            numObjectFontsize.Location = new Point(810, 10);
            numObjectFontsize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numObjectFontsize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numObjectFontsize.Name = "numObjectFontsize";
            numObjectFontsize.Size = new Size(67, 23);
            numObjectFontsize.TabIndex = 5;
            numObjectFontsize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(895, 14);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 8;
            label3.Text = "Quality Fontsize";
            // 
            // numQualityFontsize
            // 
            numQualityFontsize.Location = new Point(992, 10);
            numQualityFontsize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numQualityFontsize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numQualityFontsize.Name = "numQualityFontsize";
            numQualityFontsize.Size = new Size(67, 23);
            numQualityFontsize.TabIndex = 7;
            numQualityFontsize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(button1);
            panel1.Location = new Point(9, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(1182, 409);
            panel1.TabIndex = 9;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(1157, 383);
            button1.Name = "button1";
            button1.Size = new Size(22, 23);
            button1.TabIndex = 3;
            button1.Text = "RL";
            button1.UseVisualStyleBackColor = true;
            // 
            // cbHideEvents
            // 
            cbHideEvents.AutoSize = true;
            cbHideEvents.Location = new Point(1076, 13);
            cbHideEvents.Name = "cbHideEvents";
            cbHideEvents.Size = new Size(88, 19);
            cbHideEvents.TabIndex = 10;
            cbHideEvents.Text = "Hide Events";
            cbHideEvents.UseVisualStyleBackColor = true;
            cbHideEvents.CheckedChanged += cbHideEvents_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 450);
            Controls.Add(cbHideEvents);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(numQualityFontsize);
            Controls.Add(label2);
            Controls.Add(numObjectFontsize);
            Controls.Add(label1);
            Controls.Add(SleepTimer);
            Controls.Add(btnSelectScript);
            Controls.Add(ScriptName);
            Controls.Add(btnRunScript);
            Name = "Form1";
            Text = "Form1";
            Layout += Form1_Layout;
            ((System.ComponentModel.ISupportInitialize)SleepTimer).EndInit();
            ((System.ComponentModel.ISupportInitialize)numObjectFontsize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQualityFontsize).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Button btnRunScript;
        private TextBox ScriptName;
        private Button btnSelectScript;
        private NumericUpDown SleepTimer;
        private Label label1;
        private Label label2;
        private NumericUpDown numObjectFontsize;
        private Label label3;
        private NumericUpDown numQualityFontsize;
        private Panel panel1;
        private Button button1;
        public CheckBox cbHideEvents;
    }
}
