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
    public partial class AddUserForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateTypeBook_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtFullname.Text)|| string.IsNullOrEmpty(txtCode.Text)|| string.IsNullOrEmpty(txtPassword.Text)||string.IsNullOrEmpty(txtUserName.Text)) {
                MessageBox.Show("All text have to infor !","Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                User user = new User();
                user.Taikhoan = txtUserName.Text;
                user.Matkhau = txtPassword.Text;    
                user.TenSinhVien = txtFullname.Text;
                user.MSSV = txtCode.Text;
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
                this.Close();
            } 
                
        }
    }
}
