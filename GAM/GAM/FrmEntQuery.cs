using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Dapper;
using System.Data.SQLite;

namespace GAM
{
    
    public partial class FrmEntQuery : Form
    {
        private string databasename;
        public FrmEntQuery()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }
        private void initInput()
        {
            txtEID.Text = "";
            txtEntName.Text = "";
            txtEntAddr.Text = "";
            txtAPName.Text = "";
            txtAPTelPhone.Text = "";
            txtAPCellPhone.Text = "";
            

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

            DataTable dtType = new DataTable();
            string sqlType = "select id,typename from enterprise_type where delflag=0";
            SQLiteCommand commandType = new SQLiteCommand(sqlType, con);
            SQLiteDataAdapter adapterType = new SQLiteDataAdapter(commandType);
            adapterType.Fill(dtType);
            cbbTypeID.DisplayMember = "typename";//控件显示的列名  
            cbbTypeID.ValueMember = "id";//控件值的列名  
            cbbTypeID.DataSource = dtType;
            DataRow drType = dtType.NewRow();
            drType[0] = "0";
            drType[1] = "请选择";
            dtType.Rows.InsertAt(drType, 0);
            cbbTypeID.SelectedIndex = 0;

            txtEID.Focus();
            con.Close();

        }

        private void FrmEntQuery_Load(object sender, EventArgs e)
        {
            initInput();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int entId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            FrmEntUpdate frmEntUpdate = new FrmEntUpdate(entId);

            DialogResult result=frmEntUpdate.ShowDialog();
           
            
            btnQuery_Click(sender,e);
          
           
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            int cateid = Convert.ToInt32(cbbCateID.SelectedValue);
            int typeid = Convert.ToInt32(cbbTypeID.SelectedValue);
            int areaid = Convert.ToInt32(cbbAreaID.SelectedValue);
            string eid = txtEID.Text.Trim();
            string name = txtEntName.Text.Trim();
            string addr = txtEntAddr.Text.Trim();
            string apname = txtAPName.Text.Trim();
            string aptelphone = txtAPTelPhone.Text.Trim();
            string apcellphone = txtAPCellPhone.Text.Trim();

            string sql = "select e.id,e.eid,e.name,e.addr,e.apname,e.aptelphone,e.apcellphone,a.areaname,c.catename,t.typename from enterprise e" +
                " join areas a on e.areaid=a.id join enterprise_category c on e.cateid=c.id" +
                " join enterprise_type t on e.typeid=t.id where e.delflag=0 ";
            if ("" != eid)
            {
                sql += "and e.eid=" + eid;
            }
            if ("" != name)
            {
                sql += "and e.name like %" + name + "%";
            }
            if ("" != addr)
            {
                sql += "and e.addr like %" + addr + "%";
            }
            if ("" != apname)
            {
                sql += "and e.apname like %" + apname + "%";
            }
            if ("" != aptelphone)
            {
                sql += "and e.aptelphone like %" + aptelphone + "%";
            }
            if ("" != apcellphone)
            {
                sql += "and e.apcellphone like %" + apcellphone + "%";
            }
            if (areaid != 0)
            {
                sql += "and e.areaid=" + areaid;
            }
            if (cateid != 0)
            {
                sql += "and e.cateid=" + cateid;
            }
            if (typeid != 0)
            {
                sql += "and e.typeid=" + typeid;
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
            con.Close();
        }
    }
}
