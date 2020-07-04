namespace SetupCardGenerator.Forms
{
    partial class FrmAddNote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddNote));
            this.grTool = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chkWithoutTool = new System.Windows.Forms.CheckBox();
            this.nmrTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmrCoeff = new System.Windows.Forms.NumericUpDown();
            this.txtNumOp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTools = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDiam = new System.Windows.Forms.ToolStripButton();
            this.btnRad = new System.Windows.Forms.ToolStripButton();
            this.btnDegr = new System.Windows.Forms.ToolStripButton();
            this.btnAp = new System.Windows.Forms.ToolStripButton();
            this.btnAround = new System.Windows.Forms.ToolStripButton();
            this.btnPlMin = new System.Windows.Forms.ToolStripButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nmrCutCount = new System.Windows.Forms.NumericUpDown();
            this.lblResult = new System.Windows.Forms.Label();
            this.grTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrCoeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrCutCount)).BeginInit();
            this.SuspendLayout();
            // 
            // grTool
            // 
            this.grTool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grTool.Controls.Add(this.splitContainer2);
            this.grTool.Location = new System.Drawing.Point(10, 13);
            this.grTool.Name = "grTool";
            this.grTool.Size = new System.Drawing.Size(380, 220);
            this.grTool.TabIndex = 93;
            this.grTool.TabStop = false;
            this.grTool.Text = "Информация об инструменте";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chkWithoutTool);
            this.splitContainer2.Panel1.Controls.Add(this.nmrTime);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.nmrCoeff);
            this.splitContainer2.Panel1.Controls.Add(this.txtNumOp);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.cmbTools);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Size = new System.Drawing.Size(374, 201);
            this.splitContainer2.SplitterDistance = 211;
            this.splitContainer2.TabIndex = 94;
            this.splitContainer2.TabStop = false;
            // 
            // chkWithoutTool
            // 
            this.chkWithoutTool.AutoSize = true;
            this.chkWithoutTool.Location = new System.Drawing.Point(9, 50);
            this.chkWithoutTool.Name = "chkWithoutTool";
            this.chkWithoutTool.Size = new System.Drawing.Size(119, 17);
            this.chkWithoutTool.TabIndex = 95;
            this.chkWithoutTool.TabStop = false;
            this.chkWithoutTool.Text = "Без инструмента?";
            this.chkWithoutTool.UseVisualStyleBackColor = true;
            this.chkWithoutTool.CheckedChanged += new System.EventHandler(this.ShowToolInfo);
            this.chkWithoutTool.CheckedChanged += new System.EventHandler(this.CalculateTool);
            // 
            // nmrTime
            // 
            this.nmrTime.DecimalPlaces = 2;
            this.nmrTime.Location = new System.Drawing.Point(9, 171);
            this.nmrTime.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nmrTime.Name = "nmrTime";
            this.nmrTime.Size = new System.Drawing.Size(196, 20);
            this.nmrTime.TabIndex = 3;
            this.nmrTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrTime.ValueChanged += new System.EventHandler(this.CalculateTool);
            this.nmrTime.Click += new System.EventHandler(this.nmrTime_Click);
            this.nmrTime.Enter += new System.EventHandler(this.nmrTime_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Время обработки, мин";
            // 
            // nmrCoeff
            // 
            this.nmrCoeff.Location = new System.Drawing.Point(6, 127);
            this.nmrCoeff.Name = "nmrCoeff";
            this.nmrCoeff.Size = new System.Drawing.Size(199, 20);
            this.nmrCoeff.TabIndex = 2;
            this.nmrCoeff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrCoeff.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrCoeff.ValueChanged += new System.EventHandler(this.CalculateTool);
            this.nmrCoeff.Click += new System.EventHandler(this.nmrCoeff_Click);
            this.nmrCoeff.Enter += new System.EventHandler(this.nmrCoeff_Enter);
            // 
            // txtNumOp
            // 
            this.txtNumOp.Enabled = false;
            this.txtNumOp.Location = new System.Drawing.Point(6, 23);
            this.txtNumOp.Name = "txtNumOp";
            this.txtNumOp.Size = new System.Drawing.Size(199, 20);
            this.txtNumOp.TabIndex = 91;
            this.txtNumOp.TabStop = false;
            this.txtNumOp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Номер перехода";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Коэффициент стойкости";
            // 
            // cmbTools
            // 
            this.cmbTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTools.FormattingEnabled = true;
            this.cmbTools.Location = new System.Drawing.Point(6, 86);
            this.cmbTools.MaxDropDownItems = 15;
            this.cmbTools.Name = "cmbTools";
            this.cmbTools.Size = new System.Drawing.Size(199, 21);
            this.cmbTools.TabIndex = 1;
            this.cmbTools.SelectedIndexChanged += new System.EventHandler(this.ShowToolInfo);
            this.cmbTools.SelectedIndexChanged += new System.EventHandler(this.CalculateTool);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Инструмент";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 99;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(10, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 130);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Описание перехода";
            // 
            // txtNote
            // 
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNote.Location = new System.Drawing.Point(3, 41);
            this.txtNote.MaxLength = 3000;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(371, 86);
            this.txtNote.TabIndex = 5;
            this.txtNote.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDiam,
            this.btnRad,
            this.btnDegr,
            this.btnAp,
            this.btnAround,
            this.btnPlMin});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(371, 25);
            this.toolStrip1.TabIndex = 101;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDiam
            // 
            this.btnDiam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDiam.Image = ((System.Drawing.Image)(resources.GetObject("btnDiam.Image")));
            this.btnDiam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDiam.Name = "btnDiam";
            this.btnDiam.Size = new System.Drawing.Size(23, 22);
            this.btnDiam.Text = "Ø";
            this.btnDiam.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnRad
            // 
            this.btnRad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRad.Image = ((System.Drawing.Image)(resources.GetObject("btnRad.Image")));
            this.btnRad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRad.Name = "btnRad";
            this.btnRad.Size = new System.Drawing.Size(23, 22);
            this.btnRad.Text = "R";
            this.btnRad.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnDegr
            // 
            this.btnDegr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDegr.Image = ((System.Drawing.Image)(resources.GetObject("btnDegr.Image")));
            this.btnDegr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDegr.Name = "btnDegr";
            this.btnDegr.Size = new System.Drawing.Size(23, 22);
            this.btnDegr.Text = "°";
            this.btnDegr.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnAp
            // 
            this.btnAp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAp.Image = ((System.Drawing.Image)(resources.GetObject("btnAp.Image")));
            this.btnAp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAp.Name = "btnAp";
            this.btnAp.Size = new System.Drawing.Size(23, 22);
            this.btnAp.Text = "\'";
            this.btnAp.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnAround
            // 
            this.btnAround.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAround.Image = ((System.Drawing.Image)(resources.GetObject("btnAround.Image")));
            this.btnAround.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAround.Name = "btnAround";
            this.btnAround.Size = new System.Drawing.Size(23, 22);
            this.btnAround.Text = "≈";
            this.btnAround.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnPlMin
            // 
            this.btnPlMin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPlMin.Image = ((System.Drawing.Image)(resources.GetObject("btnPlMin.Image")));
            this.btnPlMin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlMin.Name = "btnPlMin";
            this.btnPlMin.Size = new System.Drawing.Size(23, 22);
            this.btnPlMin.Text = "±";
            this.btnPlMin.Click += new System.EventHandler(this.AddSpecText);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(309, 460);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(229, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblResult);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.nmrCutCount);
            this.groupBox2.Location = new System.Drawing.Point(10, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 81);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Информация о норме расхода";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(277, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Кол-во кромок на пластине (для цельного инстр. = 1)";
            // 
            // nmrCutCount
            // 
            this.nmrCutCount.Location = new System.Drawing.Point(314, 19);
            this.nmrCutCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrCutCount.Name = "nmrCutCount";
            this.nmrCutCount.Size = new System.Drawing.Size(60, 20);
            this.nmrCutCount.TabIndex = 4;
            this.nmrCutCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrCutCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrCutCount.ValueChanged += new System.EventHandler(this.CalculateTool);
            this.nmrCutCount.Click += new System.EventHandler(this.nmrCutCount_Click);
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Location = new System.Drawing.Point(12, 42);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(362, 36);
            this.lblResult.TabIndex = 105;
            this.lblResult.Text = "label7";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmAddNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grTool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAddNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Описание перехода";
            this.Load += new System.EventHandler(this.FrmAddNote_Load);
            this.grTool.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmrTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrCoeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrCutCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grTool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmrCoeff;
        private System.Windows.Forms.TextBox txtNumOp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTools;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nmrTime;
        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDiam;
        private System.Windows.Forms.ToolStripButton btnRad;
        private System.Windows.Forms.ToolStripButton btnDegr;
        private System.Windows.Forms.ToolStripButton btnAround;
        private System.Windows.Forms.ToolStripButton btnAp;
        private System.Windows.Forms.ToolStripButton btnPlMin;
        private System.Windows.Forms.CheckBox chkWithoutTool;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmrCutCount;
        private System.Windows.Forms.Label lblResult;
    }
}