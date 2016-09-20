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
using GAM.Models;

namespace GAM
{
    public partial class FrmEntUpdate : Form
    {
        private string databasename;
        private int entId;
        public FrmEntUpdate(Int32 entid)
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
            this.entId = entid;
        }

        private void initInput()
        {
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
           
            DataTable dtArea = new DataTable();
            string sqlArea = "select id,areaname from areas where delflag=0";
            SQLiteCommand commandArea = new SQLiteCommand(sqlArea, con);
            SQLiteDataAdapter adapterArea = new SQLiteDataAdapter(commandArea);
            adapterArea.Fill(dtArea);
            cbbAreaID.DisplayMember = "areaname";//控件显示的列名  
            cbbAreaID.ValueMember = "id";//控件值的列名  
            cbbAreaID.DataSource = dtArea;
           
            DataTable dtType = new DataTable();
            string sqlType = "select id,typename from enterprise_type where delflag=0";
            SQLiteCommand commandType = new SQLiteCommand(sqlType, con);
            SQLiteDataAdapter adapterType = new SQLiteDataAdapter(commandType);
            adapterType.Fill(dtType);
            cbbTypeID.DisplayMember = "typename";//控件显示的列名  
            cbbTypeID.ValueMember = "id";//控件值的列名  
            cbbTypeID.DataSource = dtType;           

            txtEntName.Focus();
            con.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEntUpdate_Load(object sender, EventArgs e)
        {
            initInput();
            string sql = "select e.id,e.eid,e.name,e.addr,e.apname,e.aptelphone,e.apcellphone,a.areaname,c.catename,t.typename from enterprise e" +
               " join areas a on e.areaid=a.id join enterprise_category c on e.cateid=c.id" +
               " join enterprise_type t on e.typeid=t.id where e.delflag=0 and e.id=@entId ";
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            var ent = con.Query<Enterprise>(sql, new
            {
                entId = entId
            }).SingleOrDefault();

            cbbCateID.Text = ent.Catename;
            cbbTypeID.Text = ent.Typename;
            cbbAreaID.Text = ent.Areaname;
            txtID.Text = ent.Id.ToString();
            txtEID.Text = ent.Eid.ToString();
            txtEntName.Text = ent.Name;
            txtEntAddr.Text = ent.Addr;
            txtAPName.Text = ent.Apname;
            txtAPTelPhone.Text = ent.Aptelphone;
            txtAPCellPhone.Text = ent.Apcellphone;
           
        }

       

        private void btnModify_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
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
            con.Execute(" update enterprise set eid=@eid,name=@name,addr=@addr,cateid=@cateid,typeid=@typeid,areaid=@areaid,apname=@apname,aptelphone=@aptelphone,apcellphone=@apcellphone,createat=@createAt,remark=@remark where id=@id"
           , new
           {
               eid = eid,
               name = name,
               addr = addr,
               cateid = cateid,
               typeid = typeid,
               areaid = areaid,
               apname = apname,
               aptelphone = aptelphone,
               apcellphone = apcellphone,
               createAt = createAt,
               remark = remark,
               id = id
           });
            con.Close();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());          

            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            con.Execute(" update enterprise set delflag=@delflag  where id=@id", new
            {
                delflag = true,
                id = id
            });
            con.Close();
            this.Close();
        }
    }
}
