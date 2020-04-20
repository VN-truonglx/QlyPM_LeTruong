using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyPhongMay_newvers.Class;

namespace QuanLyPhongMay_newvers
{
    public partial class frmTinh : Form
    {
        DataTable tblTinh; 
        public frmTinh()
        {
            InitializeComponent();
        }

        private void frmTinh_Load(object sender, EventArgs e)
        {
            txtMaTinh.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaTinh, TenTinh FROM tblTinh";

            tblTinh = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvTinh.DataSource = tblTinh; 
        }

        private void dgvTinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTinh.Focus();
                return;
            }

            txtMaTinh.Text = dgvTinh.CurrentRow.Cells["MaTinh"].Value.ToString();
            txtTenTinh.Text = dgvTinh.CurrentRow.Cells["TenTinh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); 
            txtMaTinh.Enabled = true; 
            txtMaTinh.Focus();
        }
        private void ResetValue()
        {
            txtMaTinh.Text = "";
            txtTenTinh.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaTinh.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã tỉnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTinh.Focus();
                return;
            }
            if (txtTenTinh.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên tỉnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenTinh.Focus();
                return;
            }
            sql = "Select MaTinh From tblTinh where MaTinh=N'" + txtMaTinh.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã tỉnh này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTinh.Focus();
                return;
            }

            sql = "INSERT INTO tblTinh VALUES(N'" + txtMaTinh.Text + "',N'" + txtTenTinh.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaTinh.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            //if (tblTinh.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (txtMaTinh.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenTinh.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên tỉnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblTinh SET TenTinh=N'" + txtTenTinh.Text.ToString() + "' WHERE MaTinh=N'" + txtMaTinh.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

            btnHuy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaTinh.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblTinh WHERE MaTinh=N'" + txtMaTinh.Text + "'";
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
            txtMaTinh.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
