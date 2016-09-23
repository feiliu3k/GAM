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

        private void initInput()
        {
            
            txtEntName.Text = "";
            txtEntAddr.Text = "";
            txtChargeManName.Text = "";          


            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            DataTable dtCate = new DataTable();
            string sqlCate = "select id,catename from enterprise_category where delflag=0";
            SQLiteCommand commandCate = new SQLiteCommand(sqlCate, con);
            SQLiteDataAdapter adapterCate = new SQLiteDataAdapter(commandCate);
            adapterCate.Fill(dtCate);
            cbbCateID.DisplayMember = "catename";//控件显示的列名  
            cbbCateID.ValueMember = "id";//控件值的列名  
            cbbCateID.DataSource = dtCate;
            DataRow drCate = dtCate.NewRow();
            drCate[0] = "0";
            drCate[1] = "请选择";
            dtCate.Rows.InsertAt(drCate, 0);
            cbbCateID.SelectedIndex = 0;

            DataTable dtArea = new DataTable();
            string sqlArea = "select id,areaname from areas where delflag=0";
            SQLiteCommand commandArea = new SQLiteCommand(sqlArea, con);
            SQLiteDataAdapter adapterArea = new SQLiteDataAdapter(commandArea);
            adapterArea.Fill(dtArea);
            cbbAreaID.DisplayMember = "areaname";//控件显示的列名  
            cbbAreaID.ValueMember = "id";//控件值的列名  
            cbbAreaID.DataSource = dtArea;
            DataRow drArea = dtArea.NewRow();
            drArea[0] = "0";
            drArea[1] = "请选择";
            dtArea.Rows.InsertAt(drArea, 0);
            cbbAreaID.SelectedIndex = 0;
           
            con.Close();

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
           // btnQuery_Click(sender, e);
        }

        private void FrmHistoryData_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dateTimePicker1.Value = DateTime.Parse(dt.AddDays(-7).ToLongDateString() + " 00:00:00");
            dateTimePicker2.Value = DateTime.Parse(dt.ToLongDateString() + " 23:59:59");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            int cateid = Convert.ToInt32(cbbCateID.SelectedValue);            
            int areaid = Convert.ToInt32(cbbAreaID.SelectedValue);
            
            string name = txtEntName.Text.Trim();
            string addr = txtEntAddr.Text.Trim();
            string chargeManName = txtChargeManName.Text.Trim();
            
            string sql = "select distinct c.id,ec.catename,ar.areaname,c.charge_num,c.common_num,c.chkent_num,c.create_at,chm.name chmname from check_action c " +
                        " join enterprise_category ec on c.cate_id = ec.id" +
                        " join areas ar on c.area_id = ar.id" +
                        " join choice_charge_man cchm on c.id = cchm.checkaction_id" +
                        " join charge_man chm on chm.id = cchm.chargeman_id" +
                        " join choice_enterprise cent on c.id = cent.checkaction_id" +
                        " join enterprise ent on ent.id = cent.enterprise_id" +
                        " where c.delflag = 0 and c.create_at between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'";

            if ("" != name)
            {
                sql += " and ent.name like '%" + name + "%'";
            }
            if ("" != addr)
            {
                sql += " and ent.addr like '%" + addr + "%'";
            }
            if ("" != chargeManName)
            {
                sql += " and chm.name like '%" + chargeManName + "%'";
            }
            
            if (areaid != 0)
            {
                sql += " and c.area_id=" + areaid;
            }
            if (cateid != 0)
            {
                sql += " and c.cate_id=" + cateid;
            }
           
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            DataTable dt = new DataTable();
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
    }
}
