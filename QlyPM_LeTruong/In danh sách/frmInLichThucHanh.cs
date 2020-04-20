using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;
using QuanLyPhongMay_newvers.Class;


namespace QuanLyPhongMay_newvers
{
    public partial class frmInLichThucHanh : Form
    {
        public frmInLichThucHanh()
        {
            InitializeComponent();
        }

        private void frmInLichThucHanh_Load(object sender, EventArgs e)
        {
            DataTable tbl = Class.Functions.GetDataToTable("SELECT * FROM tblLichThucHanh");
            dataGridView_InLichThucHanh.DataSource = tbl;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_InLichThucHanh.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_InLichThucHanh.EditMode = DataGridViewEditMode.EditProgrammatically;
            Load_comboBox();
        }
        private void Load_comboBox()
        {
            //Load cboMaLop
            cboMaLop.DataSource = Class.Functions.GetDataToTable("SELECT DISTINCT  MaLop FROM tblLichThucHanh");
            cboMaLop.ValueMember = "MaLop";
            cboMaLop.DisplayMember = "MaLop";
            cboMaLop.SelectedIndex = -1;
            //Load cboMaGV
            cboMaGV.DataSource = Class.Functions.GetDataToTable("SELECT DISTINCT MaGV FROM tblLichThucHanh");
            cboMaGV.ValueMember = "MaGV";
            cboMaGV.DisplayMember = "MaGV";
            cboMaGV.SelectedIndex = -1;
            //Load cboMaPhòng máy
            cboMaPM.DataSource = Class.Functions.GetDataToTable("SELECT DISTINCT MaPM FROM tblLichThucHanh");
            cboMaPM.ValueMember = "MaPM";
            cboMaPM.DisplayMember = "MaPM";
            cboMaPM.SelectedIndex = -1;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadDataGridView()
        {
            DataTable tbl;
            string sql = "Select* from tblMonThucHanh";
            tbl = Class.Functions.GetDataToTable(sql);
            dataGridView_InLichThucHanh.DataSource = tbl;

            //txt hiện thị mã môn và tên môn ở dòng đầu tiên


            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_InLichThucHanh.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_InLichThucHanh.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cboMaLop.Text == "" && cboMaGV.Text == "" && cboMaPM.Text == "")
            {
                MessageBox.Show("Vui lòng chọn điều kiện hiển thị trước!");
            }

            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblLichThucHanh;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Banking Acedemy Vietnam";
            exRange.Range["A2:E2"].MergeCells = true;
            exRange.Range["A2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:E2"].Value = "12 Chua Boc Street, Quang Trung Ward, Dong Da District, Hanoi, Vietnam";

            exRange.Range["E5:G5"].Font.Size = 20;
            exRange.Range["E5:G5"].Font.Bold = true;
            exRange.Range["E5:G5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["E5:G5"].MergeCells = true;
            exRange.Range["E5:G5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["E5:G5"].Value = "LỊCH THỰC HÀNH";

            if (cboMaLop.Text != "")
            {
                MessageBox.Show("\tVui lòng chờ...\n Đang cập nhật dữ liệu");
                sql = "SELECT MaSTT,MaPM,MaGV,MaSV,MaLop,MaMon,MaCa,Thu,NgayBD,NgayKT FROM tblLichThucHanh where MaLop=N'" + cboMaLop.SelectedValue + "'";
                tblLichThucHanh = Class.Functions.GetDataToTable(sql);
                exRange.Range["A11:K11"].Font.Bold = true;
                exRange.Range["A11:K11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["G11"].ColumnWidth = 16;
                exRange.Range["I11"].ColumnWidth = 13;
                exRange.Range["J11"].ColumnWidth = 12;
                exRange.Range["K11"].ColumnWidth = 12;
                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Mã STT";
                exRange.Range["C11:C11"].Value = "Mã phòng máy";
                exRange.Range["D11:D11"].Value = "Mã sinh viên";
                exRange.Range["E11:E11"].Value = "Mã giảng viên";
                exRange.Range["F11:F11"].Value = "Mã lớp";
                exRange.Range["G11:G11"].Value = "Mã môn thực hành";
                exRange.Range["H11:H11"].Value = "Mã ca";
                exRange.Range["I11:I11"].Value = "Thứ";
                exRange.Range["J11:J11"].Value = "Ngày bắt đầu";
                exRange.Range["K11:K11"].Value = "Ngày kết thúc";
                for (hang = 0; hang < tblLichThucHanh.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblLichThucHanh.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];

                exRange = exSheet.Cells[1][hang + 15]; //Ô A1 

                exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
                exRange.Range["A1:C1"].MergeCells = true;
                exRange.Range["A1:C1"].Font.Italic = true;
                exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

                exSheet.Name = "LỊCH THỰC HÀNH";
                exApp.Visible = true;

            }
            if (cboMaGV.Text != "")
            {
                MessageBox.Show("\tVui lòng chờ...\n Đang cập nhật dữ liệu");
                sql = "SELECT MaSTT,MaPM,MaGV,MaSV,MaLop,MaMon,MaCa,Thu,NgayBD,NgayKT FROM tblLichThucHanh where MaGV=N'" + cboMaGV.SelectedValue + "'";
                tblLichThucHanh = Class.Functions.GetDataToTable(sql);
                exRange.Range["A11:K11"].Font.Bold = true;
                exRange.Range["A11:K11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["G11"].ColumnWidth = 16;
                exRange.Range["I11"].ColumnWidth = 13;
                exRange.Range["J11"].ColumnWidth = 12;
                exRange.Range["K11"].ColumnWidth = 12;

                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Mã STT";
                exRange.Range["C11:C11"].Value = "Mã phòng máy";
                exRange.Range["D11:D11"].Value = "Mã sinh viên";
                exRange.Range["E11:E11"].Value = "Mã giảng viên";
                exRange.Range["F11:F11"].Value = "Mã lớp";
                exRange.Range["G11:G11"].Value = "Mã môn thực hành";
                exRange.Range["H11:H11"].Value = "Mã ca";
                exRange.Range["I11:I11"].Value = "Thứ";
                exRange.Range["J11:J11"].Value = "Ngày bắt đầu";
                exRange.Range["K11:K11"].Value = "Ngày kết thúc";
                for (hang = 0; hang < tblLichThucHanh.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblLichThucHanh.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];

                exRange = exSheet.Cells[1][hang + 15]; //Ô A1 

                exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
                exRange.Range["A1:C1"].MergeCells = true;
                exRange.Range["A1:C1"].Font.Italic = true;
                exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

                exSheet.Name = "LỊCH THỰC HÀNH";
                exApp.Visible = true;

            }
            if (cboMaPM.Text != "")
            {
                MessageBox.Show("\tVui lòng chờ...\n Đang cập nhật dữ liệu");
                sql = "SELECT MaSTT,MaPM,MaSV,MaGV,MaLop,MaMon,MaCa,Thu,NgayBD,NgayKT FROM tblLichThucHanh where MaPM=N'" + cboMaPM.SelectedValue + "'";
                tblLichThucHanh = Class.Functions.GetDataToTable(sql);
                exRange.Range["A11:K11"].Font.Bold = true;
                exRange.Range["A11:K11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["G11"].ColumnWidth = 16;
                exRange.Range["I11"].ColumnWidth = 13;
                exRange.Range["J11"].ColumnWidth = 12;
                exRange.Range["K11"].ColumnWidth = 12;

                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Mã STT";
                exRange.Range["C11:C11"].Value = "Mã phòng máy";
                exRange.Range["D11:D11"].Value = "Mã sinh viên";
                exRange.Range["E11:E11"].Value = "Mã giảng viên";
                exRange.Range["F11:F11"].Value = "Mã lớp";
                exRange.Range["G11:G11"].Value = "Mã môn thực hành";
                exRange.Range["H11:H11"].Value = "Mã ca";
                exRange.Range["I11:I11"].Value = "Thứ";
                exRange.Range["J11:J11"].Value = "Ngày bắt đầu";
                exRange.Range["K11:K11"].Value = "Ngày kết thúc";
                for (hang = 0; hang < tblLichThucHanh.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblLichThucHanh.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblLichThucHanh.Rows[hang][cot].ToString();
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];

                exRange = exSheet.Cells[1][hang + 15]; //Ô A1 

                exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
                exRange.Range["A1:C1"].MergeCells = true;
                exRange.Range["A1:C1"].Font.Italic = true;
                exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

                exSheet.Name = "LỊCH THỰC HÀNH";
                exApp.Visible = true;
            }

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboMaLop.Text = "";
            cboMaGV.Text = ""; cboMaPM.Text = "";
            DataTable tbl;
            string sql = "Select* from tblLichThucHanh";
            tbl = Class.Functions.GetDataToTable(sql);
            dataGridView_InLichThucHanh.DataSource = tbl;
        }

        private void cboMaLop_SelectionChangeCommitted(object sender, EventArgs e)
        {

            string sql = "Select* from tblLichThucHanh where MaLop=N'" + cboMaLop.SelectedValue + "' ";
            DataTable tbl;

            tbl = Class.Functions.GetDataToTable(sql);
            dataGridView_InLichThucHanh.DataSource = tbl;
        }

        private void cboMaGV_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sql = "Select* from tblLichThucHanh where MaGV=N'" + cboMaGV.SelectedValue + "' ";
            DataTable tbl;

            tbl = Class.Functions.GetDataToTable(sql);
            dataGridView_InLichThucHanh.DataSource = tbl;
        }

        private void cboMaPM_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sql = "Select * from tblLichThucHanh where MaPM=N'" + cboMaPM.SelectedValue + "' ";
            DataTable tbl;

            tbl = Class.Functions.GetDataToTable(sql);
            dataGridView_InLichThucHanh.DataSource = tbl;
        }
    }
}
