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
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            txtMaSV.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tblSinhVien";

            DataTable tblTinh = Class.Functions.GetDataToTable(sql);
            dgvSinhVien.DataSource = tblTinh; 

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaSV.Enabled = true;
            txtMaSV.Focus();
        }
        private void ResetValue()
        {
            txtMaSV.Text = "";
            txtTenSV.Text = "";
            txtMaPM.Text = "";
            txtNamSinh.Text = "";
            txtGioiTinh.Text = "";
            txtMaTinh.Text = "";
            txtDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaSV.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTinh.Focus();
                return;
            }
            if (txtTenSV.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSV.Focus();
                return;
            }
            if (txtMaPM.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã phòng máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPM.Focus();
                return;
            }
            if (txtGioiTinh.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập giới tính sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiTinh.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập số điện thoại vào để còn liên lạc chứ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "Select MaSV From tblSinhVien where MaSV=N'" + txtMaSV.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Đã tồn tại mã sinh viên này, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return;
            }

            sql = "INSERT INTO tblSinhVien VALUES(N'" + txtMaSV.Text + "',N'" + txtTenSV.Text + "',N'" + txtMaPM.Text + "'" +
                ",N'" + txtNamSinh.Text + "',N'" + txtGioiTinh.Text + "',N'" + txtMaTinh.Text + "',N'" + txtDienThoai.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaSV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dgvSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenSV.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblSinhVien SET TenSV=N'" + txtTenSV.Text.ToString() + "',MaPM=N'" + txtMaPM.Text.ToString() + "' " +
                ",NamSinh=N'" + txtNamSinh.Text.ToString() + "',GioiTinh=N'" + txtGioiTinh.Text.ToString() + "',MaTinh=N'" + txtMaTinh.Text.ToString() + "'" +
                ",DienThoai=N'" + txtDienThoai.Text.ToString() + "' WHERE MaSV=N'" + txtMaSV.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnHuy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaSV.Text == "") 
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblSinhVien WHERE MaSV=N'" + txtMaSV.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaSV.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return;
            }

            txtMaSV.Text = dgvSinhVien.CurrentRow.Cells["MaSV"].Value.ToString();
            txtTenSV.Text = dgvSinhVien.CurrentRow.Cells["TenSV"].Value.ToString();
            txtMaPM.Text = dgvSinhVien.CurrentRow.Cells["MaPM"].Value.ToString();
            txtNamSinh.Text = dgvSinhVien.CurrentRow.Cells["GioiTinh"].Value.ToString();
            txtMaTinh.Text = dgvSinhVien.CurrentRow.Cells["MaTinh"].Value.ToString();
            txtGioiTinh.Text = dgvSinhVien.CurrentRow.Cells["GioiTinh"].Value.ToString();
            txtDienThoai.Text = dgvSinhVien.CurrentRow.Cells["DienThoai"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
    }
}
