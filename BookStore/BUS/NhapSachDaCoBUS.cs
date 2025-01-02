using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhapSachDaCoBUS
    {
        private NhapSachDaCoDAL _dal = new NhapSachDaCoDAL();

        // Lấy danh sách sách
        public List<SachViewModel> LoadSachList()
        {
            return _dal.GetSachList();
        }

        // Lấy chi tiết sách theo Id
        public SachViewModel GetSachDetails(int sachId)
        {
            return _dal.GetSachDetailsById(sachId);
        }

        public int SavePhieuNhap(decimal tongTien)
        {
            return _dal.SavePhieuNhap(tongTien);
        }

        //public void SaveCTPhieuNhap(int phieuNhapId, List<CT_PhieuNhap> chiTietPhieuNhaps)
        //{
        //     _dal.SaveCTPhieuNhap(phieuNhapId, chiTietPhieuNhaps);
        //}
        // Lưu phiếu nhập và chi tiết phiếu nhập
        //public int SavePhieuNhap(decimal tongTien, List<CT_PhieuNhap> chiTietPhieuNhaps)
        //{
        //    return _dal.SavePhieuNhap(tongTien, chiTietPhieuNhaps);
        //}

        //public void UpdateKhoAfterPhieuNhap(int sachId, int soLuongNhap, decimal donGiaNhap, decimal donGiaBan)
        //{
        //    _dal.UpdateKho(sachId, soLuongNhap, donGiaNhap, donGiaBan);
        //}

    }
}
