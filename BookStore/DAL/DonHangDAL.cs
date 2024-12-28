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

        public void LuuKhachHang(KhachHang khachHang)
        {
            _context.KhachHangs.Add(khachHang);
            _context.SaveChanges();
        }

        public int LayMaKHTheoEmail(string email)
        {
            return _context.KhachHangs.FirstOrDefault(k => k.Email == email)?.Id ?? 0;
        }

        public void LuuDonHang(DonHang donHang, List<CT_DonHang> chiTietDonHangs)
        {
            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            foreach (var ctdh in chiTietDonHangs)
            {
                ctdh.IdDonHang = donHang.Id;
                _context.CT_DonHang.Add(ctdh);

                // Cập nhật tồn kho
                var kho = _context.Khoes.FirstOrDefault(k => k.IdSach == ctdh.IdSach);
                if (kho != null)
                {
                    kho.SoLuongTon -= ctdh.SoLuongBan;
                }
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
        public List<Sach> LayDanhSachSach(string tenSach = "", int? namXuatBan = null, int? idNXB = null)
        {
            var query = _context.Saches.AsQueryable();

            // Lọc theo tên sách nếu có
            if (!string.IsNullOrEmpty(tenSach))
            {
                query = query.Where(s => s.TenSach.Contains(tenSach));
            }

            // Lọc theo năm xuất bản nếu có
            if (namXuatBan.HasValue)
            {
                query = query.Where(s => s.NamXuatBan == namXuatBan.Value);
            }

            // Lọc theo IdNXB (Nhà xuất bản) nếu có
            if (idNXB.HasValue)
            {
                query = query.Where(s => s.IdNXB == idNXB.Value);
            }

            // Dùng Include để lấy các quan hệ liên quan đến sách
            query = query.Include(s => s.CT_DonHang)           // Liên kết với bảng CT_DonHang
                         .Include(s => s.CT_PhieuNhap)         // Liên kết với bảng CT_PhieuNhap
                         .Include(s => s.Khoes)                // Liên kết với bảng Kho
                         .Include(s => s.NhaXuatBan)           // Liên kết với bảng NhaXuatBan
                         .Include(s => s.NhaCungCaps)          // Liên kết với bảng NhaCungCap
                         .Include(s => s.TacGias)              // Liên kết với bảng TacGia
                         .Include(s => s.TheLoais);            // Liên kết với bảng TheLoai

            // Trả về danh sách sách sau khi đã lọc và bao gồm thông tin các quan hệ
            return query.ToList();
        }


    }
}
