using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Configuration;
using Dapper;


namespace GAM
{
    public partial class FrmHistoryData : Form
    {
        private string databasename;
        public FrmHistoryData()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string sql = "select c.id,ec.catename,ar.areaname,c.charge_num,c.common_num,c.chkent_num,c.create_at,chm.name chmname from check_action c "+
                        " join enterprise_category ec on c.cate_id = ec.id" +
                        " join areas ar on c.area_id = ar.id" +
                        " join choice_charge_man cchm on c.id = cchm.checkaction_id" +
                        " join charge_man chm on chm.id = cchm.chargeman_id" +
                        " where c.delflag = 0 and c.create_at between '"+ dateTimePicker1.Value +"' and '"+ dateTimePicker2.Value+"'";
            DataTable dt = new DataTable();           
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int actionId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            FrmDisplayAction frmDisplayAction = new FrmDisplayAction(actionId);

            frmDisplayAction.ShowDialog();
            btnQuery_Click(sender, e);
        }
    }
}
