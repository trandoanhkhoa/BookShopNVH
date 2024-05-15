using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopNVH
{
    public partial class LoginForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            var acc = db.Users.SingleOrDefault(x => x.Taikhoan == username && x.Matkhau == password);
            if (acc!=null) // Tài khoản tồn tại
            {
                MainForm frm = new MainForm();
                frm.Show();
                this.Hide();
            }
            else // Tài khoản không tồn tại
            {
                lblnotification.Visible = true;
            } 
                
        }
    }
}
