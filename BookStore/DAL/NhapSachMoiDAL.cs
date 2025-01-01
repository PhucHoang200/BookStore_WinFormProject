using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhapSachMoiDAL
    {
        private readonly BookStoreDBEntities _context;

        public NhapSachMoiDAL()
        {
            _context = new BookStoreDBEntities();
        }

        // Kiểm tra tác giả có tồn tại trong cơ sở dữ liệu không
        public bool KiemTraTacGiaTonTai(string tacGia)
        {
            return _context.TacGias.Any(x => x.TenTG == tacGia);
        }

        // Lưu tác giả vào cơ sở dữ liệu
        public int LuuTacGia(string tacGia)
        {
            var newTacGia = new TacGia() { TenTG = tacGia };
            _context.TacGias.Add(newTacGia);
            _context.SaveChanges();
            return newTacGia.Id;
        }

        // Lấy ID của tác giả
        public int LayIdTacGia(string tacGia)
        {
            return _context.TacGias.FirstOrDefault(x => x.TenTG == tacGia).Id;
        }

        // Kiểm tra thể loại tồn tại
        public bool KiemTraTheLoaiTonTai(string theLoai)
        {
            return _context.TheLoais.Any(x => x.TenTL == theLoai);
        }

        // Lưu thể loại vào cơ sở dữ liệu
        public int LuuTheLoai(string theLoai)
        {
            var newTheLoai = new TheLoai() { TenTL = theLoai };
            _context.TheLoais.Add(newTheLoai);
            _context.SaveChanges();
            return newTheLoai.Id;
        }

        // Kiểm tra nhà xuất bản tồn tại
        public bool KiemTraNhaXBTonTai(string nhaXB)
        {
            return _context.NhaXuatBans.Any(x => x.TenNXB == nhaXB);
        }

        // Lưu nhà xuất bản vào cơ sở dữ liệu
        public int LuuNhaXB(string nhaXB)
        {
            var newNhaXB = new NhaXuatBan() { TenNXB = nhaXB };
            _context.NhaXuatBans.Add(newNhaXB);
            _context.SaveChanges();
            return newNhaXB.Id;
        }

        // Kiểm tra nhà cung cấp tồn tại
        public bool KiemTraNCCTonTai(string nhaCungCap)
        {
            return _context.NhaCungCaps.Any(x => x.TenNCC == nhaCungCap);
        }

        // Lưu nhà cung cấp vào cơ sở dữ liệu
        public int LuuNCC(string nhaCungCap)
        {
            var newNCC = new NhaCungCap() { TenNCC = nhaCungCap };
            _context.NhaCungCaps.Add(newNCC);
            _context.SaveChanges();
            return newNCC.Id;
        }

        // Lưu phiếu nhập sách vào cơ sở dữ liệu
        public void LuuPhieuNhapSach(List<ChiTietPhieuNhap> chiTietPhieuNhapList)
        {
            if (chiTietPhieuNhapList == null || !chiTietPhieuNhapList.Any())
            {
                throw new ArgumentException("Danh sách chi tiết phiếu nhập không được để trống!");
            }

            try
            {
                foreach (var chiTiet in chiTietPhieuNhapList)
                {
                    var ctPhieuNhap = new ChiTietPhieuNhap
                    {
                        TenSach = chiTiet.TenSach,
                        TacGia = chiTiet.TacGia,
                        TheLoai = chiTiet.TheLoai,
                        NhaXB = chiTiet.NhaXB,
                        NamXuatBan = chiTiet.NamXuatBan,
                        SoLuongNhap = chiTiet.SoLuongNhap,
                        GiaNhap = chiTiet.GiaNhap,
                        GiaBan = chiTiet.GiaBan,
                        NhaCungCap = chiTiet.NhaCungCap,
                        ThanhTien = chiTiet.ThanhTien
                    };

                    //_context.CT_PhieuNhap.Add(ctPhieuNhap);  // Thêm đối tượng CT_PhieuNhap vào bảng
                }

                _context.SaveChanges();  // Lưu tất cả các thay đổi
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu phiếu nhập vào cơ sở dữ liệu: " + ex.Message);
            }
        }



        // Thêm chi tiết phiếu nhập
        public void ThemChiTietPhieuNhap(CT_PhieuNhap chiTiet)
        {
            _context.CT_PhieuNhap.Add(chiTiet);
            _context.SaveChanges();
        }


    }
}
