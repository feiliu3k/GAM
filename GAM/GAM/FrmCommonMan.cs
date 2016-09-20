
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
using Dapper;
using GAM.Models;
using GAM.Libs;
using System.Configuration;
using System.IO;

namespace GAM
{
    public partial class FrmCommonMan : Form
    {
        private string databasename;
        public FrmCommonMan()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void initInput()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            cbbSex.Text = "男";
            dtpBirth.Text = System.DateTime.Now.ToString("d"); 
            rtbAddr.Text = "";
            rtbRemark.Text = "";
            txtName.Focus();

        }
        private void DisplayData(SQLiteConnection con)
        {

            DataTable dt = new DataTable();
            string sql = "select id,name,phone,sex from common_man where delflag=0";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string name = txtName.Text.Trim();
            string sex = cbbSex.Text.Trim();
            DateTime birth = dtpBirth.Value;
            string phone = txtPhone.Text.Trim();            
            string addr = rtbAddr.Text.Trim();           
            string remark = rtbRemark.Text.Trim();

            con.Execute(" insert into common_man(name,sex,birth,phone,addr,remark) values (@name,@sex,@birth,@phone,@addr,@remark)"
            , new
            {
                name=name,
                sex=sex,
                birth=birth,
                phone=phone,
                addr=addr,
                remark=remark 

            });

            DisplayData(con);
            con.Close();
            initInput();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string userid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();

            var cm = con.Query<CommonMan>("select * from common_man where id=@userid", new
            {
                userid = userid
            }).SingleOrDefault();

            txtID.Text = userid;
            txtName.Text=cm.Name;
            cbbSex.Text = cm.Sex;
            dtpBirth.Value = cm.Birth;
            txtPhone.Text = cm.Phone;           
            rtbAddr.Text=cm.Addr;
            rtbRemark.Text=cm.Remark;

            con.Close();
        }

        private void FrmCommonMan_Load(object sender, EventArgs e)
        {
            initInput();
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            DisplayData(con);
            con.Close();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            string name = txtName.Text.Trim();
            string sex = cbbSex.Text.Trim();
            DateTime birth = dtpBirth.Value;
            string phone = txtPhone.Text.Trim();
            string addr = rtbAddr.Text.Trim();
            string remark = rtbRemark.Text.Trim();
            string userid = txtID.Text.Trim();
            con.Execute(" update common_man set name=@name,sex=@sex,birth=@birth,phone=@phone,addr=@addr,remark=@remark where id=@userid"
            , new
            {
                name = name,
                sex = sex,
                birth = birth,
                phone = phone,
                addr = addr,
                remark = remark,
                userid = userid

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
            
            string userid = txtID.Text.Trim();
            con.Execute(" update common_man set delflag=@delflag where id=@userid"
            , new
            {
                delflag = true,
                userid = userid

            });

            DisplayData(con);
            con.Close();
            initInput();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "Excel文件|*.xls|Excel文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;                
                try
                {
                    using (ExcelHelper excelHelper = new ExcelHelper(fName))
                    {
                        DataTable dt = excelHelper.ExcelToDataTable("Sheet1", true);
                        //将dt保存到数据表中
                        List<CommonMan> cms = new List<CommonMan>();
                       for (int i = 0; i < dt.Rows.Count; i++)
                       {
                            CommonMan commonMan = new CommonMan();


                            commonMan.Name = dt.Rows[i][0].ToString();

                            if ((null != dt.Rows[i][1]) && (""!= dt.Rows[i][1].ToString()))
                            {
                                commonMan.Birth = Convert.ToDateTime(dt.Rows[i][1].ToString());
                            }
                            else
                            {
                                commonMan.Birth = Convert.ToDateTime("1970-01-01");
                            }
                            if (null != dt.Rows[i][2])
                            {
                                commonMan.Sex = dt.Rows[i][2].ToString();
                            }
                            if (null != dt.Rows[i][3])
                            {
                                commonMan.Addr = dt.Rows[i][3].ToString();
                            }
                            if (null != dt.Rows[i][4])
                            {
                                commonMan.Phone = dt.Rows[i][4].ToString();
                            }
                            if (null != dt.Rows[i][5])
                            {
                                commonMan.Remark = dt.Rows[i][5].ToString();
                            }

                            cms.Add(commonMan);
                           
                       }

                        SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                        sb.DataSource = databasename;
                        SQLiteConnection con = new SQLiteConnection(sb.ToString());
                        con.Open();
                        con.Execute(" insert into common_man(name,sex,birth,phone,addr,remark) values (@name,@sex,@birth,@phone,@addr,@remark)",cms);
                        DisplayData(con);
                        con.Close();
                        initInput();

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
