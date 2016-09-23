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
using GAM.Models;

namespace GAM
{
    public partial class FrmChangePassword : Form
    {
        private string databasename;
        public FrmChangePassword()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
            this.AcceptButton = btnLogin;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            string password = txtPassword.Text.Trim();
            string newpassword = txtNewPassword.Text.Trim();
            string confirmpassword = txtConfirmPassword.Text.Trim();
            if ("" == newpassword)
            {
                MessageBox.Show("用户名不能为空，请重新输入!", "错误");
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtNewPassword.Focus();
            }
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            string username = "admin";
            con.Open();
            User user = con.Query<User>("select * from users where active=1 and username=@username", new { username = username }).Single();
            if ((null != user) && (user.Password == password))
            {
                if (newpassword == confirmpassword)
                {
                    con.Execute("update users set password=@newpassword where username=@username", new {
                        newpassword = newpassword,
                        username = username
                    });
                }
                else{
                    MessageBox.Show("输入的密码不一致，请检查!", "错误");
                }
            }
            else
            {
                MessageBox.Show("输入的老用户密码错误，请检查!", "错误");
            }
            con.Close();
            MessageBox.Show("密码修改已成功，请重新退出登录!", "信息");
            this.Close();
        }
    }
}
