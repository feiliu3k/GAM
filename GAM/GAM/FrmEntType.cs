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
using GAM.Models;
using Dapper;
using GAM.Libs;
using System.Configuration;


namespace GAM
{
    public partial class FrmEntType : Form
    {
        private string databasename;
        public FrmEntType()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void initInput()
        {
            txtTypeID.Text = ""; 
            txtTypeName.Text = ""; 
            rtbTypeRemark.Text = "";
            txtTypeName.Focus();

        }
        private void DisplayData(SQLiteConnection con)
        {
            
            DataTable dt = new DataTable();
            string sql = "select id,typename,remark from enterprise_type where delflag=0";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);          
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;

        }

        private void FrmEntType_Load(object sender, EventArgs e)
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
            string typename = txtTypeName.Text.Trim();
            string remark = rtbTypeRemark.Text.Trim();

            con.Execute(" insert into enterprise_type(typename,remark) values (@typename,@remark)"
            , new
            {
                typename = typename,
                remark = remark,               
               
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
            string typeid = txtTypeID.Text.Trim();
            con.Execute("update enterprise_type set delflag=@delflag where id=@typeid ",
            new
            {
                typeid = typeid,
                delflag =true
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
            string typeid = txtTypeID.Text.Trim();
            string typename = txtTypeName.Text.Trim();
            string remark = rtbTypeRemark.Text.Trim();
            con.Execute("update enterprise_type set typename=@typename, remark=@remark where id=@typeid ",
            new
            {
                typeid = typeid,
                typename = typename,
                remark = remark,

            });
            DisplayData(con);
            con.Close();
            initInput();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtTypeID.Text =dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTypeName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            rtbTypeRemark.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

    }
}
