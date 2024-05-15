using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopNVH
{
    public partial class EditTypeBookForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public EditTypeBookForm(TypeBook tp=null)
        {
            InitializeComponent();
            LoadData(tp);
        }
        void LoadData(TypeBook tp)
        {
            lblID.Text = tp.ID.ToString();
            txtBookTypeName.Text = tp.TheLoai;
            txtDescTypeBook.Text = tp.Mota;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.Text)) {
                MessageBox.Show("Type Book Name is not null!","Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                int ID = int.Parse(lblID.Text);
                var tp = db.TypeBooks.SingleOrDefault(x=>x.ID == ID);
                if(tp!=null)
                {
                    tp.TheLoai = txtBookTypeName.Text;
                    tp.Mota = txtDescTypeBook.Text;
                    db.SubmitChanges();
                }    
            } 
                
        }
    }
}
