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
    public partial class AddBorrowBook : Form
    {
        QLBOOKSDataContext db = new QLBOOKSDataContext();
        public AddBorrowBook()
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
        void LoadData()
        {
            dgvBook.DataSource = db.Books.Select(x => new { x.ID, x.TenSach, x.Tacgia, x.TypeBook.TheLoai, x.SoLuong, x.Price }).ToList();
            dgvBook.Columns["IDBook"].DisplayIndex = 1;
            dgvBook.Columns["Tacgia"].DisplayIndex = 2;
            dgvBook.Columns["TenSach"].DisplayIndex = 3;
            dgvBook.Columns["LoaiSach"].DisplayIndex = 4;
            dgvBook.Columns["SoLuong"].DisplayIndex = 5;
            dgvBook.Columns["Gia"].DisplayIndex = 6;
            dgvBook.Columns["Borrow"].DisplayIndex = 0;
        }

        private void AddBorrowBook_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                var lst = db.Books.Where(x=>x.TenSach.Contains(txtSearch.Text)||x.TypeBook.TheLoai.Contains(txtSearch.Text)).Select(x => new { x.ID, x.TenSach, x.Tacgia, x.TypeBook.TheLoai, x.SoLuong, x.Price }).ToList();
                dgvBook.Columns["IDBook"].DisplayIndex = 1;
                dgvBook.Columns["Tacgia"].DisplayIndex = 2;
                dgvBook.Columns["TenSach"].DisplayIndex = 3;
                dgvBook.Columns["LoaiSach"].DisplayIndex = 4;
                dgvBook.Columns["SoLuong"].DisplayIndex = 5;
                dgvBook.Columns["Gia"].DisplayIndex = 6;
                dgvBook.Columns["Borrow"].DisplayIndex = 0;
                dgvBook.DataSource = lst;

            }
            else
            {
                LoadData();
            }

        }

        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBook.Columns[e.ColumnIndex].Name == "Borrow" && e.RowIndex >= 0)
            {
                int ID = int.Parse(dgvBook["IDBook", e.RowIndex].Value.ToString());
                string tensach = dgvBook["TenSach", e.RowIndex].Value.ToString();
                int SoluongSachconlai = int.Parse(dgvBook["SoLuong", e.RowIndex].Value.ToString());
                int Soluong = 1;
                decimal gia = decimal.Parse(dgvBook["Gia", e.RowIndex].Value.ToString());

                if(SoluongSachconlai<=0)
                {
                    MessageBox.Show("Not enough quantity!","Notification",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }    
                bool bookExists = false;
                foreach (DataGridViewRow row in dgvBorrowBook.Rows)
                {
                    if (row.Cells["ID"].Value != null && (int)row.Cells["ID"].Value == ID)
                    {
                        // Sách đã tồn tại, tăng số lượng lên 1
                        row.Cells["SoLuongBorrow"].Value = (int)row.Cells["SoLuongBorrow"].Value + 1;
                        row.Cells["GiaBorrow"].Value = (decimal)row.Cells["GiaBorrow"].Value + gia;
                        bookExists = true;
                        break;
                    }
                }

                // Nếu sách chưa tồn tại, thêm dòng mới vào dgvOther
                if (!bookExists)
                {
                    dgvBorrowBook.Rows.Add(ID, tensach, Soluong, gia);
                }
                Book book = db.Books.SingleOrDefault(x => x.ID == ID);
                book.SoLuong -= 1;
                lblpriceBorrow.Text = dgvBorrowBook.Rows.Cast<DataGridViewRow>()
                                 .Where(row => row.Cells["GiaBorrow"].Value != null)
                                 .Sum(row => decimal.TryParse(row.Cells["GiaBorrow"].Value.ToString(), out decimal price) ? price : 0).ToString();
                lblQuantity.Text = dgvBorrowBook.RowCount.ToString();
                db.SubmitChanges();
            }
            LoadData();
        }

        private void btnCreateBorrowBook_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtMSSV.Text))
            {
                MessageBox.Show("MSSV is not null!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                List<Borrow> lstborrow = new List<Borrow>();
                int IDuser = db.Users.SingleOrDefault(x=>x.MSSV==txtMSSV.Text).ID;
                foreach (DataGridViewRow row in dgvBorrowBook.Rows)
                {
                    Borrow br = new Borrow();
                    if (!row.IsNewRow)
                    {
                        

                        // Lấy giá trị của ô "ID" và kiểm tra giá trị null
                        if (row.Cells["ID"].Value != null && int.TryParse(row.Cells["ID"].Value.ToString(), out int idBooks))
                        {
                            br.IDBooks = idBooks;
                        }

                        // Lấy giá trị của ô "GiaBorrow" và kiểm tra giá trị null
                        if (row.Cells["GiaBorrow"].Value != null && decimal.TryParse(row.Cells["GiaBorrow"].Value.ToString(), out decimal price))
                        {
                            br.Price = price;
                        }

                        // Lấy giá trị của ô "SoLuongBorrow" và kiểm tra giá trị null
                        if (row.Cells["SoLuongBorrow"].Value != null && int.TryParse(row.Cells["SoLuongBorrow"].Value.ToString(), out int quantity))
                        {
                            br.Quantity = quantity;
                        }

                        // Đặt giá trị khác của đối tượng Borrow
                        br.IDUser = IDuser;
                        br.fromDate = dtpkfromDate.Value;
                        br.toDate = dtpktoDate.Value;

                        // Thêm đối tượng vào danh sách
                        lstborrow.Add(br);
                    }
                }    
                db.Borrows.InsertAllOnSubmit(lstborrow);
                db.SubmitChanges();
                this.Close();
            } 
                
        }

        private void dgvBorrowBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBorrowBook.Columns[e.ColumnIndex].Name == "RemoveBook" && e.RowIndex >= 0)
            {
                // Lấy giá trị của cột "ID" từ dòng hiện tại
                int id = int.Parse(dgvBorrowBook["ID", e.RowIndex].Value.ToString());
                int Soluong = int.Parse(dgvBorrowBook["SoLuongBorrow", e.RowIndex].Value.ToString());
                
                Book book = db.Books.SingleOrDefault(x=>x.ID == id);
                if(book!=null)
                {
                    book.SoLuong += 1;
                    db.SubmitChanges();
                    if(Soluong <= 0)
                    {
                        // Xóa dòng hiện tại
                        dgvBorrowBook.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        dgvBorrowBook["SoLuongBorrow", e.RowIndex].Value = Soluong - 1;
                    } 
                        
                    LoadData();
                }
                lblpriceBorrow.Text = dgvBorrowBook.Rows.Cast<DataGridViewRow>()
                                 .Where(row => row.Cells["GiaBorrow"].Value != null)
                                 .Sum(row => decimal.TryParse(row.Cells["GiaBorrow"].Value.ToString(), out decimal price) ? price : 0).ToString();
                lblQuantity.Text = dgvBorrowBook.RowCount.ToString();

            }
        }
    }
}
