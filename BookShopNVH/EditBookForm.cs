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
    public partial class EditBookForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public EditBookForm(Book bk=null)
        {
            InitializeComponent();
            LoadDataCategoriesBooks();
            LoadData(bk);
        }
        void LoadDataCategoriesBooks()
        {
            cbbCategoriesBook.DataSource = db.TypeBooks.ToList();
            cbbCategoriesBook.DisplayMember = "TheLoai";
            cbbCategoriesBook.ValueMember = "ID";
        }
        void LoadData(Book bk)
        {
            lblID.Text = bk.ID.ToString();
            txtBookname.Text = bk.TenSach;
            txtDesc.Text = bk.Mota;
            txtAuthor.Text = bk.Tacgia;
            txtPrice.Text = bk.Price.ToString();
            txtQuantity.Text = bk.SoLuong.ToString();
            cbbCategoriesBook.SelectedValue = bk.LoaiSach;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveButton_Click(object sender, EventArgs e)
        {
            var book = db.Books.SingleOrDefault(x => x.ID == int.Parse(lblID.Text));
            if (book != null)
            {
                book.TenSach = txtBookname.Text;
                book.Price = decimal.Parse(txtPrice.Text);
                book.Mota = txtDesc.Text;
                book.ID = int.Parse(lblID.Text);
                book.Tacgia = txtAuthor.Text;
                book.LoaiSach = int.Parse(cbbCategoriesBook.SelectedValue.ToString());
                book.SoLuong = int.Parse(txtQuantity.Text);
                db.SubmitChanges();
            }
        }
    }
}
