﻿using System;
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
    public partial class FrmEntCate : Form
    {
        private string databasename;
        public FrmEntCate()
        {
            InitializeComponent();
            databasename = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void initInput()
        {
            txtCateID.Text = "";
            txtCateName.Text = "";
            rtbCateRemark.Text = "";
            txtCateName.Focus();

        }
        private void DisplayData(SQLiteConnection con)
        {

            DataTable dt = new DataTable();
            string sql = "select id,catename,remark,active from enterprise_category where delflag=0";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;

        }

        private void FrmEntCate_Load(object sender, EventArgs e)
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
            string catename = txtCateName.Text.Trim();
            string remark = rtbCateRemark.Text.Trim();
            bool active = chbActive.Checked;
            con.Execute(" insert into enterprise_category(catename,remark,active) values (@catename,@remark,@active)"
            , new
            {
                catename = catename,
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
            string typeid = txtCateID.Text.Trim();
            string catename = txtCateName.Text.Trim();
            string remark = rtbCateRemark.Text.Trim();
            bool active = chbActive.Checked;
            con.Execute("update enterprise_category set catename=@catename, remark=@remark,active=@active where id=@typeid ",
            new
            {
                typeid = typeid,
                catename = catename,
                remark = remark,
                active = active,

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
            string typeid = txtCateID.Text.Trim();
            con.Execute("update enterprise_category set delflag=@delflag where id=@typeid ",
            new
            {
                typeid = typeid,
                delflag = true
            });

            DisplayData(con);
            con.Close();
            initInput();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCateID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCateName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            rtbCateRemark.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            chbActive.Checked = Boolean.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == 3)
                {

                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                    sb.DataSource = databasename;
                    SQLiteConnection con = new SQLiteConnection(sb.ToString());
                    con.Open();
                    con.Execute("update enterprise_category set  active=@active where id=@cateid ",
                    new
                    {
                        cateid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                        active = (bool)this.dataGridView1[e.ColumnIndex, e.RowIndex].Value,

                    });
                    con.Close();
                }
            }

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
