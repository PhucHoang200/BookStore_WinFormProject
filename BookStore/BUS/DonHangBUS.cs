using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms; // Để sử dụng Guna2DataGridView
using System.Windows.Forms; // Để sử dụng DataGridView và DataGridViewRow

namespace BUS
{
    public class DonHangBUS
    {
        private readonly DonHangDAL _dal;

        public DonHangBUS()
        {
            _dal = new DonHangDAL();
        }

        public void LuuKhachHang(string hoTen, string email, string sdt, string diaChi)
        {
            var kh = new KhachHang
            {
                HoTenKH = hoTen,
                Email = email,
                SoDienThoai = sdt,
                DiaChi = diaChi
            };
            _dal.LuuKhachHang(kh);
        }

        public void ThemChiTietDonHang(Guna2DataGridView dgv, int maSach, int soLuong, decimal donGia)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Convert.ToInt32(row.Cells["Column9"].Value) == maSach)
                {
                    throw new Exception("Sách đã tồn tại trong giỏ hàng, vui lòng cập nhật số lượng.");
                }
            }

            dgv.Rows.Add(maSach, "Tên sách", donGia, soLuong);
        }

        public decimal TinhTongTien(Guna2DataGridView dgv)
        {
            return dgv.Rows.Cast<DataGridViewRow>().Sum(row =>
                Convert.ToDecimal(row.Cells["Column11"].Value) *
                Convert.ToInt32(row.Cells["Column12"].Value));
        }

        public void LuuDonHang(string maNV, string maKH, decimal tongTien, List<CT_DonHang> chiTietDonHangs)
        {
            var donHang = new DonHang
            {
                IdKhachHang = int.Parse(maKH),
                TongTienBan = tongTien,
                NgayMuaHang = DateTime.Now
            };

            _dal.LuuDonHang(donHang, chiTietDonHangs);
        }

        public List<Sach> LayDanhSachSach()
        {
            return _dal.LayDanhSachSach();
        }

    }
}
