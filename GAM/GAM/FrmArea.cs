using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Dapper;
using System.Configuration;

namespace GAM
{
    public partial class FrmArea : Form
    {
        private string databasename;
        public FrmArea()
        {
            InitializeComponent();
            databasename = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void initInput()
        {
            txtAreaID.Text = "";
            txtAreaName.Text = "";
            rtbAreaRemark.Text = "";
            txtAreaName.Focus();

        }
        private void DisplayData(SQLiteConnection con)
        {

            DataTable dt = new DataTable();
            string sql = "select id,areaname,remark,active from areas where delflag=0";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;

        }
        private void FrmArea_Load(object sender, EventArgs e)
        {
            initInput();
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            DisplayData(con);
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string areaname = txtAreaName.Text.Trim();
            string remark = rtbAreaRemark.Text.Trim();
            bool active = chbActive.Checked;
            con.Execute(" insert into areas(areaname,remark, active) values (@areaname,@remark,@active)"
            , new
            {
                areaname = areaname,
                remark = remark,
                active = active,

            });

            DisplayData(con);
            con.Close();
            initInput();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string areaid = txtAreaID.Text.Trim();
            string areaname = txtAreaName.Text.Trim();
            string remark = rtbAreaRemark.Text.Trim();
            bool active = chbActive.Checked;
            con.Execute("update areas set areaname=@areaname, remark=@remark, active=@active where id=@areaid ",
            new
            {
                areaid = areaid,
                areaname = areaname,
                remark = remark,
                active=active,

            });
            DisplayData(con);
            con.Close();
            initInput();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string areaid = txtAreaID.Text.Trim();
            con.Execute("update areas set delflag=@delflag where id=@areaid ",
            new
            {
                areaid = areaid,
                delflag = true
            });

            DisplayData(con);
            con.Close();
            initInput();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtAreaID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAreaName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            rtbAreaRemark.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            chbActive.Checked = Boolean.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

       

       
    }
}
