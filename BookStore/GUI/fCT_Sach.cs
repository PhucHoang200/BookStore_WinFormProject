using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fCT_Sach : Form
    {
        private int _maSach;
        private SachBUS _bus = new SachBUS();

        public fCT_Sach(int maSach)
        {
            InitializeComponent();
            _maSach = maSach;
            LoadThongTinChiTietSach();
        }

        private void LoadThongTinChiTietSach()
        {
            // Lấy thông tin sách
            var sachInfo = _bus.GetSachDetailsById(_maSach);

            if (sachInfo != null)
            {
                lblMaSach.Text = sachInfo.MaSach.ToString();
                lblTenSach.Text = sachInfo.TenSach;
                lblTacGia.Text = sachInfo.TacGia;
                lblTheLoai.Text = sachInfo.TheLoai;
                lblNXB.Text = sachInfo.NhaXuatBan;
                lblNamXuatBan.Text = sachInfo.NamXuatBan.ToString();
                lblSoLuongTon.Text = sachInfo.SoLuongTon.ToString();
            }

            // Lấy danh sách từ CT_PhieuNhap
            dgvChiTietPhieuNhap.Rows.Clear();
            foreach (var item in sachInfo.ChiTietPhieuNhap)
            {
                dgvChiTietPhieuNhap.Rows.Add(item.TenNCC, item.SoLuongNhap,
                                             item.DonGiaNhap, item.DonGiaBan,
                                             item.NgayNhap?.ToString("dd/MM/yyyy"), item.ThanhTien);
            }
            dgvChiTietPhieuNhap.Refresh();
        }
    }
}
