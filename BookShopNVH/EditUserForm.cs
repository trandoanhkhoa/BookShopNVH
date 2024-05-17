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
    public partial class EditUserForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public EditUserForm(User us)
        {
            InitializeComponent();
            LoadData(us);
        }
        void LoadData(User us)
        {
            lblID.Text = us.ID.ToString();
            txtCode.Text = us.MSSV;
            txtFullname.Text = us.TenSinhVien;
            txtPassword.Text = "";
            txtUserName.Text = us.Taikhoan;

        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCode.Text)||string.IsNullOrEmpty(txtFullname.Text)||string.IsNullOrEmpty(txtPassword.Text)||string.IsNullOrEmpty(txtUserName.Text)) { 
                MessageBox.Show("All infor have to value!","Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                User us = db.Users.SingleOrDefault(x=>x.ID==int.Parse(lblID.Text));
                us.MSSV = txtCode.Text;
                us.TenSinhVien = txtFullname.Text;
                us.Matkhau = txtPassword.Text;
                us.Taikhoan = txtUserName.Text;
                db.SubmitChanges();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
