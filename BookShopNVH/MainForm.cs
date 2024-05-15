using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopNVH
{
    public partial class MainForm : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        QLBOOKSDataContext db = new QLBOOKSDataContext();


        bool checkMax = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnMaximum_Click(object sender, EventArgs e)
        {
            if (!checkMax)
            {
                //WindowState = FormWindowState.Maximized;


                /*this.FormBorderStyle = FormBorderStyle.None;*/
                this.StartPosition = FormStartPosition.Manual;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                this.Location = new Point(0, 0);
                checkMax = true;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                this.Height = 616;
                this.Width = 1016;
                this.StartPosition=FormStartPosition.CenterScreen;
                checkMax = false;
            }

        }

        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        void LoadDataBooks()
        {
            dgvBook.DataSource = db.Books.Select(x=> new {x.ID,x.TenSach,x.Tacgia,x.Price,x.SoLuong,x.TypeBook.TheLoai}).ToList();
        }
        void LoadDataTypeBooks()
        {
            dgvTypeBook.DataSource = db.TypeBooks.ToList();
        }
        void LoadUsers()
        {
            dgvUser.DataSource = db.Users.ToList();
        }

        private void btnCreateButton_Click(object sender, EventArgs e)
        {
            AddBookForm frm = new AddBookForm();
            frm.ShowDialog();
            LoadDataBooks();
        }
        void LoadDataCategoriesBooks()
        {
            cbbCategoriesBook.DataSource = db.TypeBooks.ToList();
            cbbCategoriesBook.DisplayMember = "TheLoai";
            cbbCategoriesBook.ValueMember = "ID";
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
           
            LoadDataBooks();
            LoadDataCategoriesBooks();
            LoadDataTypeBooks();
            LoadUsers();
        }

        private void cbbCategoriesBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategoriesBook.SelectedItem != null)
            {
                // Kiểm tra và parse giá trị của SelectedValue
                int idtypebook;
                if (int.TryParse(cbbCategoriesBook.SelectedValue.ToString(), out idtypebook))
                {
                    if (idtypebook != 2)
                    {
                        var books = db.Books.Where(x => x.LoaiSach == idtypebook).ToList();
                        dgvBook.DataSource = books;
                    }
                    else
                    {
                        LoadDataBooks();
                    }
                }
            }
        }

        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dgvBook.Columns[e.ColumnIndex].Name=="Edit" && e.RowIndex>=0)
            {
                int ID = int.Parse(dgvBook["ID", e.RowIndex].Value.ToString());
                var book = db.Books.SingleOrDefault(x => x.ID == ID);
                EditBookForm frm = new EditBookForm(book);
                frm.Show();
                LoadDataBooks();
            }
            else if (dgvBook.Columns[e.ColumnIndex].Name == "Delete"&& e.RowIndex >= 0)
            {
                if (MessageBox.Show("Are you sure Delete row ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ID = int.Parse(dgvBook["ID", e.RowIndex].Value.ToString());
                    var book = db.Books.SingleOrDefault(x => x.ID == ID);
                    if (book != null)
                    {
                        db.Books.DeleteOnSubmit(book);
                        db.SubmitChanges();
                        LoadDataBooks();
                    }

                }
            }
        }

        private void btnCreateTypeBooks_Click(object sender, EventArgs e)
        {
            AddTypeBookForm frm = new AddTypeBookForm();
            frm.ShowDialog();
            LoadDataTypeBooks();
        }

        private void dgvTypeBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTypeBook.Columns[e.ColumnIndex].Name == "EditTypeBook" && e.RowIndex >= 0)
            {
                int ID = int.Parse(dgvTypeBook["IDTypeBook", e.RowIndex].Value.ToString());
                var typebook = db.TypeBooks.SingleOrDefault(x => x.ID == ID);
                EditTypeBookForm frm = new EditTypeBookForm(typebook);
                frm.Show();
                LoadDataTypeBooks();
            }
            else if (dgvTypeBook.Columns[e.ColumnIndex].Name == "DeleteTypeBook" && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Are you sure Delete row ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ID = int.Parse(dgvTypeBook["IDTypeBook", e.RowIndex].Value.ToString());
                    var typebook = db.TypeBooks.SingleOrDefault(x => x.ID == ID);
                    if (typebook != null)
                    {
                        db.TypeBooks.DeleteOnSubmit(typebook);
                        db.SubmitChanges();
                        LoadDataTypeBooks();
                    }

                }
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            AddUserForm frm = new AddUserForm();
            frm.ShowDialog();
            LoadUsers();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUser.Columns[e.ColumnIndex].Name == "EditUser" && e.RowIndex >= 0)
            {
                int ID = int.Parse(dgvUser["IDTypeBook", e.RowIndex].Value.ToString());
                var user = db.Users.SingleOrDefault(x => x.ID == ID);
                EditUserForm frm = new EditUserForm(user);
                frm.ShowDialog();
                LoadDataTypeBooks();
            }
            else if (dgvUser.Columns[e.ColumnIndex].Name == "DeleteUser" && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Are you sure Delete row ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ID = int.Parse(dgvUser["IDTypeBook", e.RowIndex].Value.ToString());
                    var user = db.Users.SingleOrDefault(x => x.ID == ID);
                    if (user != null)
                    {
                        db.Users.DeleteOnSubmit(user);
                        db.SubmitChanges();
                        LoadDataTypeBooks();
                    }

                }
            }
        }
    }
}
