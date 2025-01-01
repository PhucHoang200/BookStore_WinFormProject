using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhapSachMoiBUS
    {
        private readonly NhapSachMoiDAL nhapSachMoiDAL = new NhapSachMoiDAL();
        

        // Kiểm tra tác giả tồn tại hay không
        public bool KiemTraTacGiaTonTai(string tacGia)
        {
            return nhapSachMoiDAL.KiemTraTacGiaTonTai(tacGia);
        }

        // Lưu tác giả và trả về Id
        public int LuuTacGia(string tacGia)
        {
            return nhapSachMoiDAL.LuuTacGia(tacGia);
        }

        // Lấy Id tác giả
        public int LayIdTacGia(string tacGia)
        {
            return nhapSachMoiDAL.LayIdTacGia(tacGia);
        }

        // Lưu phiếu nhập
        public void LuuPhieuNhapSach(List<ChiTietPhieuNhap> chiTietPhieuNhapList)
        {
            if (chiTietPhieuNhapList == null || !chiTietPhieuNhapList.Any())
            {
                throw new ArgumentException("Danh sách chi tiết phiếu nhập không được để trống!");
            }

            // Gọi phương thức DAL để lưu dữ liệu
            nhapSachMoiDAL.LuuPhieuNhapSach(chiTietPhieuNhapList);
        }


        // Thêm chi tiết phiếu nhập
        public void ThemChiTietPhieuNhap(CT_PhieuNhap chiTiet)
        {
            nhapSachMoiDAL.ThemChiTietPhieuNhap(chiTiet);
        }

        public bool KiemTraTheLoaiTonTai(string theLoai)
        {
            return nhapSachMoiDAL.KiemTraTheLoaiTonTai(theLoai);
        }

        public int LuuTheLoai(string theLoai)
        {
            return nhapSachMoiDAL.LuuTheLoai(theLoai);
        }

        // Kiểm tra nhà xuất bản tồn tại
        public bool KiemTraNhaXBTonTai(string nhaXB)
        {
            return nhapSachMoiDAL.KiemTraNhaXBTonTai(nhaXB);
        }

        public int LuuNhaXB(string nhaXB)
        {
            return nhapSachMoiDAL.LuuNhaXB(nhaXB);
        }

        public bool KiemTraNCCTonTai(string nhaCungCap)
        {
            return nhapSachMoiDAL.KiemTraNCCTonTai(nhaCungCap);
        }

        public int LuuNCC(string nhaCungCap)
        {
            return nhapSachMoiDAL.LuuNCC(nhaCungCap);   
        }
    }
}
