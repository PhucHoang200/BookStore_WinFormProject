using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class DonHangDAL
    {
        private readonly BookStoreDBEntities _context;

        public DonHangDAL()
        {
            _context = new BookStoreDBEntities();
        }

        public int LuuKhachHangVaLayMa(KhachHang khachHang)
        {
            _context.KhachHangs.Add(khachHang);
            _context.SaveChanges();
            return khachHang.Id; // Trả về mã khách hàng mới tạo
        }

        public void CapNhatKhachHang(int maKH, KhachHang khachHangMoi)
        {
            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.Id == maKH);
            if (khachHang != null)
            {
                khachHang.HoTenKH = khachHangMoi.HoTenKH;
                khachHang.Email = khachHangMoi.Email;
                khachHang.SoDienThoai = khachHangMoi.SoDienThoai;
                khachHang.DiaChi = khachHangMoi.DiaChi;
                _context.SaveChanges();
            }
        }


        public int LayMaKHTheoEmail(string email)
        {
            return _context.KhachHangs.FirstOrDefault(k => k.Email == email)?.Id ?? 0;
        }

        public void LuuDonHang(DonHang donHang, List<CT_DonHang> chiTietDonHangs)
        {
            // Lưu đơn hàng
            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            // Lưu chi tiết đơn hàng
            foreach (var ct in chiTietDonHangs)
            {
                ct.IdDonHang = donHang.Id; // Gán IdDonHang từ đơn hàng vừa lưu
                _context.CT_DonHang.Add(ct);
            }
            _context.SaveChanges();
        }


        public void CapNhatSachTrongKho(int idSach, int soLuong)
        {
            var kho = _context.Khoes.FirstOrDefault(k => k.IdSach == idSach);
            if (kho != null)
            {
                kho.SoLuongTon += soLuong;
                _context.SaveChanges();
            }
        }

        // Lấy danh sách sách từ cơ sở dữ liệu
        public List<Sach> LayDanhSachSach(string tuKhoa = null)
        {
            var query = _context.Saches.Include(s => s.TacGias).Include(s => s.TheLoais).Include(s => s.Khoes).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(s => s.TenSach.Contains(tuKhoa) ||
                                         s.TacGias.Any(tg => tg.TenTG.Contains(tuKhoa)) ||
                                         s.TheLoais.Any(tl => tl.TenTL.Contains(tuKhoa)) ||
                                         s.NhaXuatBan.TenNXB.Contains(tuKhoa));
            }

            return query.ToList();
        }



    }
}
