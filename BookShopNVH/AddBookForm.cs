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
    public partial class AddBookForm : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public AddBookForm()
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

        private void btnCreateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookname.Text) || string.IsNullOrEmpty(txtAuthor.Text))
            {
                MessageBox.Show("Book name and Author is not null !","Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            } 
            if(string.IsNullOrEmpty(txtQuantity.Text)) {

                txtQuantity.Text = "1";
            }
            Book book = new Book();
            book.Tacgia = txtAuthor.Text;
            book.TenSach = txtBookname.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Mota = txtDesc.Text;
            book.SoLuong = int.Parse(txtQuantity.Text);
            book.LoaiSach = int.Parse(cbbbooktype.SelectedValue.ToString());
            db.Books.InsertOnSubmit(book);
            db.SubmitChanges();
            this.Close();
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            cbbbooktype.DataSource = db.TypeBooks.ToList();
            cbbbooktype.DisplayMember = "TheLoai";
            cbbbooktype.ValueMember = "ID";
        }
    }
}
