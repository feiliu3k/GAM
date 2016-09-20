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
using System.Data.SQLite;
using Dapper;

namespace GAM
{
    public partial class FrmEntAdd : Form
    {
        private string databasename;
        public FrmEntAdd()
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
            rtbRemark.Text = "";

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
        

        private void btnAdd_Click(object sender, EventArgs e)
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
            string remark = rtbRemark.Text.Trim();
            DateTime createAt = dtpCreateAt.Value;

            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            con.Execute(" insert into enterprise(eid,name,addr,cateid,typeid,areaid,apname,aptelphone,apcellphone,createat,remark) values (@eid,@name,@addr,@cateid,@typeid,@areaid,@apname,@aptelphone,@apcellphone,@createAt,@remark)"
           , new
           {
               eid=eid,
               name=name,
               addr=addr,
               cateid=cateid,
               typeid=typeid,
               areaid=areaid,
               apname=apname,
               aptelphone=aptelphone,
               apcellphone=apcellphone,
               createAt =createAt,
               remark =remark,
            });
            con.Close();
            DialogResult result = MessageBox.Show("是否继续添加？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                initInput();
            }
            else
            {
                this.Close();
            }

        }

        private void FrmEntAdd_Load(object sender, EventArgs e)
        {            
            initInput();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
