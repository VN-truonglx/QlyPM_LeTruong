using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyPhongMay_newvers
{
    public partial class frmGiaoVien : Form
    {
        public frmGiaoVien()
        {
            InitializeComponent();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            txtMaGV.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tblGiaoVien";

            DataTable tblTinh = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvGiaoVien.DataSource = tblTinh;
        }

        private void dgvGiaoVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaGV.Focus();
                return;
            }
            txtMaGV.Text = dgvGiaoVien.CurrentRow.Cells["MaGV"].Value.ToString();
            txtTenGV.Text = dgvGiaoVien.CurrentRow.Cells["TenGV"].Value.ToString();
            txtDienThoai.Text = dgvGiaoVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        
        private void ResetValue()
        {
            txtMaGV.Text = "";
            txtTenGV.Text = "";
            txtDienThoai.Text = "";
        }

        //Code chức năng buttons
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaGV.Enabled = true; //cho phép nhập mới
            txtMaGV.Focus();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            //if (tblTinh.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (txtMaGV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenGV.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên giáo viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblGiaoVien SET TenGV=N'" + txtTenGV.Text.ToString() + "',DienThoai=N'" + txtDienThoai.Text.ToString() + "' WHERE MaGV=N'" + txtMaGV.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

            btnHuy.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;

            if (txtMaGV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblGiaoVien WHERE MaGV=N'" + txtMaGV.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaGV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã giảng viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaGV.Focus();
                return;
            }
            if (txtTenGV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên giảng viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenGV.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "Select MaGV From tblGiaoVien where MaGV=N'" + txtMaGV.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã giáo viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaGV.Focus();
                return;
            }

            sql = "INSERT INTO tblGiaoVien VALUES(N'" + txtMaGV.Text + "',N'" + txtTenGV.Text + "',N'" + txtDienThoai.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaGV.Enabled = false;
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            ResetValue();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaGV.Enabled = false;
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
