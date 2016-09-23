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
using System.Configuration;
using Dapper;
using GAM.Models;
using GAM.Libs;

namespace GAM
{
    public partial class FrmEntLoad : Form
    {
        private string databasename;
        List<Area> areaList = null;
        List<EnterpriseType> typeList = null;
        List<EnterpriseCategory> cateList = null;
        public FrmEntLoad()
        {
            InitializeComponent();
            databasename = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "Excel文件|*.xls|Excel文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImportFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEntLoad_Load(object sender, EventArgs e)
        {
            txtImportFilePath.ReadOnly = true;

            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;


            Area model1 = new Area();
            string sql1 = @"SELECT id,areaname, remark, delflag FROM areas";
            using (IDbConnection conn = new SQLiteConnection(sb.ToString()))
            {
                areaList = conn.Query<Area>(sql1, model1).ToList<Area>();
            }

            EnterpriseCategory model2 = new EnterpriseCategory();
            string sql2 = @"SELECT id,catename, remark, delflag FROM enterprise_category";
            using (IDbConnection conn = new SQLiteConnection(sb.ToString()))
            {
                cateList = conn.Query<EnterpriseCategory>(sql2, model2).ToList<EnterpriseCategory>();
            }

            EnterpriseType model3 = new EnterpriseType();
            string sql3 = @"SELECT id,typename, remark, delflag FROM enterprise_type";
            using (IDbConnection conn = new SQLiteConnection(sb.ToString()))
            {
                typeList = conn.Query<EnterpriseType>(sql3, model3).ToList<EnterpriseType>();
            }
        }  

        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(@txtImportFilePath.Text.Trim()))
                {
                    DataTable dt = excelHelper.ExcelToDataTable("Sheet1", true);
                    //将dt保存到数据表中
                    List<DbEntityEnterprise> ents = new List<DbEntityEnterprise>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DbEntityEnterprise ent = new DbEntityEnterprise();
                        ent.Eid = dt.Rows[i][0].ToString();
                        ent.Name = dt.Rows[i][1].ToString();                      
                        if (null != dt.Rows[i][2])
                        {
                            ent.Addr = dt.Rows[i][2].ToString();
                        }
                        if (null != dt.Rows[i][3])
                        {                        
                            EnterpriseCategory resultEC = cateList.Find(delegate (EnterpriseCategory ec) {
                                return ec.Catename.Equals(dt.Rows[i][3].ToString().Trim());
                            });
                            ent.CateId = resultEC.Id;
                        }
                        if (null != dt.Rows[i][4])
                        {
                            EnterpriseType resultET = typeList.Find(delegate (EnterpriseType et) {
                                return et.Typename.Equals(dt.Rows[i][4].ToString().Trim());
                            });
                            ent.TypeId = resultET.Id;
                        }
                        if (null != dt.Rows[i][5])
                        {
                            
                            Area resultAR = areaList.Find(delegate (Area ar) {
                                return ar.Areaname.Equals(dt.Rows[i][5].ToString().Trim());
                            });
                            ent.AreaId = resultAR.Id;
                        }
                        if (null != dt.Rows[i][6])
                        {
                            ent.Apname = dt.Rows[i][6].ToString();
                        }
                        if (null != dt.Rows[i][7])
                        {
                            ent.Aptelphone = dt.Rows[i][7].ToString();
                        }
                        if (null != dt.Rows[i][8])
                        {
                            ent.Apcellphone = dt.Rows[i][8].ToString();
                        }
                        if ((null != dt.Rows[i][9]) && ("" != dt.Rows[i][9].ToString()))
                        {
                            ent.Createat = Convert.ToDateTime(dt.Rows[i][9].ToString());
                        }
                        else
                        {
                            ent.Createat = Convert.ToDateTime("1910-01-01");
                        }
                        if (null != dt.Rows[i][10])
                        {
                            ent.Remark = dt.Rows[i][10].ToString();
                        }

                        ents.Add(ent);

                    }

                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                    sb.DataSource = databasename;
                    SQLiteConnection con = new SQLiteConnection(sb.ToString());
                    con.Open();
                    con.Execute(" insert into enterprise(eid,name,addr,cateid,typeid,areaid,apname,aptelphone,apcellphone,createat,remark) values (@eid,@name,@addr,@cateid,@typeid,@areaid,@apname,@aptelphone,@apcellphone,@createAt,@remark)",ents);
                    con.Close();                   

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally{
                this.Close();
            }
        }
    }
}
