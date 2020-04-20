using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyPhongMay_newvers;

namespace QuanLyPhongMay_newvers
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
        }

        private void thôngTinSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhongMay_newvers.frmSinhVien f = new QuanLyPhongMay_newvers.frmSinhVien();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void tỉnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhongMay_newvers.frmTinh f = new QuanLyPhongMay_newvers.frmTinh();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void máyTínhToolStripMenuItem2_Click(object sender, EventArgs e)
        {
          
        }

        private void củaSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhongMay_newvers.frmInLichThucHanh f = new QuanLyPhongMay_newvers.frmInLichThucHanh();
            f.Show();
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        private void chưaKếtThúcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhongMay_newvers.frmInLTHChuaKetThuc f = new QuanLyPhongMay_newvers.frmInLTHChuaKetThuc();
            f.Show();
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        private void giảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhongMay_newvers.frmGiaoVien f = new QuanLyPhongMay_newvers.frmGiaoVien();
            f.Show();
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void caHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void phòngMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void lịchThựcHànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
