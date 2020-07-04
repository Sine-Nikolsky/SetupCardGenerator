namespace SetupCardGenerator.Forms
{
    partial class FrmReport
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReport));
            this.projectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolDPOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.schemeDPOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gearsDPOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notesDPOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.setBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolDPOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemeDPOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearsDPOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notesDPOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // projectBindingSource
            // 
            this.projectBindingSource.DataSource = typeof(SetupCardGenerator.Project);
            // 
            // toolDPOBindingSource
            // 
            this.toolDPOBindingSource.DataSource = typeof(SetupCardGenerator.DPO.ToolDPO);
            // 
            // schemeDPOBindingSource
            // 
            this.schemeDPOBindingSource.DataSource = typeof(SetupCardGenerator.DPO.SchemeDPO);
            // 
            // gearsDPOBindingSource
            // 
            this.gearsDPOBindingSource.DataSource = typeof(SetupCardGenerator.DPO.GearsDPO);
            // 
            // notesDPOBindingSource
            // 
            this.notesDPOBindingSource.DataSource = typeof(SetupCardGenerator.DPO.NotesDPO);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ProjectDataSet";
            reportDataSource1.Value = this.projectBindingSource;
            reportDataSource2.Name = "SetsDataSet";
            reportDataSource2.Value = this.projectBindingSource;
            reportDataSource3.Name = "ToolDPODataSet";
            reportDataSource3.Value = this.toolDPOBindingSource;
            reportDataSource4.Name = "SchemeDPODataSet";
            reportDataSource4.Value = this.schemeDPOBindingSource;
            reportDataSource5.Name = "GearsDPODataSet";
            reportDataSource5.Value = this.gearsDPOBindingSource;
            reportDataSource6.Name = "NotesDPODataSet";
            reportDataSource6.Value = this.notesDPOBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SetupCardGenerator.FinalReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(673, 555);
            this.reportViewer1.TabIndex = 0;
            // 
            // setBindingSource
            // 
            this.setBindingSource.DataSource = typeof(SetupCardGenerator.Set);
            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 555);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карта наладки проекта";
            this.Load += new System.EventHandler(this.FrmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolDPOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemeDPOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearsDPOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notesDPOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource projectBindingSource;
        private System.Windows.Forms.BindingSource setBindingSource;
        private System.Windows.Forms.BindingSource toolDPOBindingSource;
        private System.Windows.Forms.BindingSource schemeDPOBindingSource;
        private System.Windows.Forms.BindingSource gearsDPOBindingSource;
        private System.Windows.Forms.BindingSource notesDPOBindingSource;
    }
}