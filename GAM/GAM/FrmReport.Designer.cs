namespace GAM
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GGYWDataSet = new GAM.GGYWDataSet();
            this.enterpriseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.enterpriseTableAdapter = new GAM.GGYWDataSetTableAdapters.enterpriseTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.GGYWDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enterpriseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.AutoScroll = true;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "myds";
            reportDataSource1.Value = this.enterpriseBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GAM.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(738, 753);
            this.reportViewer1.TabIndex = 0;
            // 
            // GGYWDataSet
            // 
            this.GGYWDataSet.DataSetName = "GGYWDataSet";
            this.GGYWDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // enterpriseBindingSource
            // 
            this.enterpriseBindingSource.DataMember = "enterprise";
            this.enterpriseBindingSource.DataSource = this.GGYWDataSet;
            // 
            // enterpriseTableAdapter
            // 
            this.enterpriseTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 753);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监管行动报表";
            this.Load += new System.EventHandler(this.FrmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GGYWDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enterpriseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource enterpriseBindingSource;
        private GGYWDataSet GGYWDataSet;
        private GGYWDataSetTableAdapters.enterpriseTableAdapter enterpriseTableAdapter;
    }
}