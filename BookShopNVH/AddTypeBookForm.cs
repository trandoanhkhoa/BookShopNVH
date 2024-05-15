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
    public partial class AddTypeBookForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public AddTypeBookForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateTypeBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookTypeName.Text))
            {
                MessageBox.Show("Type Book is not null !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                TypeBook tb = new TypeBook();
                tb.TheLoai = txtBookTypeName.Text;
                tb.Mota = txtDescTypeBook.Text;
                db.TypeBooks.InsertOnSubmit(tb);
                db.SubmitChanges();
                this.Close();
            }

        }
    }
}
