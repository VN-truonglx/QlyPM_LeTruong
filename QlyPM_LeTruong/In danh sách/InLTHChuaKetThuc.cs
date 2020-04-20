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
    public partial class frmInLTHChuaKetThuc : Form
    {
        public frmInLTHChuaKetThuc()
        {
            InitializeComponent();
        }

        private void frmInDanhSachMayTinh_Load(object sender, EventArgs e)
        {

            DataTable tbl = Class.Functions.GetDataToTable("SELECT * FROM tblLichThucHanh where NgayKT>GETDATE()");
            dataGridView_InDanhSachMayTinh.DataSource = tbl;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_InDanhSachMayTinh.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_InDanhSachMayTinh.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\tVui lòng chờ...\n Đang cập nhật dữ liệu");
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

            exRange.Range["F5:H5"].Font.Size = 20;
            exRange.Range["F5:H5"].Font.Bold = true;
            exRange.Range["F5:H5"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["F5:H5"].MergeCells = true;
            exRange.Range["F5:H5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["F5:H5"].Value = "LỊCH THỰC HÀNH";

            sql = "SELECT MaSTT,MaPM,MaSV,MaGV,MaLop,MaMon,MaCa,Thu,NgayBD,NgayKT FROM tblLichThucHanh where NgayKT>GETDATE()";
            tblLichThucHanh = Class.Functions.GetDataToTable(sql);
            exRange.Range["A11:K11"].Font.Bold = true;
            exRange.Range["A11:K11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["G11"].ColumnWidth = 16;
            exRange.Range["I11"].ColumnWidth = 10;
            exRange.Range["J11:K11"].ColumnWidth = 13;
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
            //exRange = exSheet.Cells[cot][hang + 14];
            //exRange.Font.Bold = true;
            //exRange.Value2 = "Mã lịch:";
            //exRange = exSheet.Cells[cot + 1][hang + 14];
            //exRange.Font.Bold = true;
            //exRange.Value2 = tblLichThucHanh.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            //exRange.Range["A1:F1"].MergeCells = true;
            //exRange.Range["A1:F1"].Font.Bold = true;
            //exRange.Range["A1:F1"].Font.Italic = true;
            //exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            //exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.tblLichThucHanh(tblLichThucHanh.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            
            exSheet.Name = "LỊCH THỰC HÀNH";
            exApp.Visible = true;
        }
    }
}
