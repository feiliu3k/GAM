using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using GAM.Models;

namespace GAM
{
    public partial class FrmReport : Form
    {

        private List<CommonMan> cms = null;
        private List<ChargeMan> chms = null;
        private Area ar = null;
        private EnterpriseCategory ec= null;
        private string createtime = null;
        private List<DbEntityEnterprise> ents = null;

        public  DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        public FrmReport(List<CommonMan> ccms, List<ChargeMan> cchms, Area ar, EnterpriseCategory ec, List<DbEntityEnterprise> cents, string createAt)
        {
            InitializeComponent();
            this.cms = ccms;
            this.chms = cchms;
            this.ar = ar;
            this.ec = ec;
            this.ents = cents;
            this.createtime = createAt;

        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            string chargeMan_name="";
            for (int i = 0; i < this.chms.Count; i++)
            {
                chargeMan_name += chms[i].Name + "  ";
            }

            string commonMan_name = "";
            for (int i = 0; i < this.cms.Count; i++)
            {
                commonMan_name += cms[i].Name + "  ";
            }
            DataTable mydt = ToDataTable<DbEntityEnterprise>(this.ents);

            ReportParameter chargeMan = new ReportParameter("chargeMan", chargeMan_name);
            ReportParameter commonMen = new ReportParameter("commonMen", commonMan_name);
            ReportParameter cateName = new ReportParameter("cateName", this.ec.Catename);
            ReportParameter areaName = new ReportParameter("areaName", this.ar.Areaname);
            ReportParameter createtime = new ReportParameter("createtime", this.createtime);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { chargeMan, commonMen, cateName, areaName,createtime });
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("myds", mydt));
            

            this.reportViewer1.RefreshReport();
        }

    }
}
