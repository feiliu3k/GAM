using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace GAM
{
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            ReportParameter p2 = new ReportParameter("chargeMan", "aaa");
            ReportParameter p3 = new ReportParameter("commonMen", "bbb");
            ReportParameter p4 = new ReportParameter("areaName", "ccc");
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p3, p4 });

            this.reportViewer1.RefreshReport();
        }

    }
}
