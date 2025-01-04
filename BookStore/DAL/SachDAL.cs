using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SachDAL
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        public List<Sach> GetAllSach()
        {
            return db.Saches.ToList();
        }

        public List<Sach> FindSachByName(string name)
        {
            return db.Saches
                     .Where(i => i.TenSach.Contains(name))
                     .ToList();
        }

        public List<CTSachVM> GetChiTietSachTrongKho()
        {
            var query = from s in db.Saches
                        join k in db.Khoes on s.Id equals k.IdSach
                        select new CTSachVM
                        {
                            MaSach = s.Id,
                            TenSach = s.TenSach,
                            SoLuongTon = k.SoLuongTon,
                            NgayNhapMoiNhat = (from pn in db.CT_PhieuNhap
                                               where pn.MaSach == s.Id
                                               orderby pn.PhieuNhapSach.NgayNhapSach descending
                                               select pn.PhieuNhapSach.NgayNhapSach).FirstOrDefault()
                        };

            return query.ToList();
        }

        //public SachChiTietDTO GetSachDetailsById(int maSach)
        //{
        //    var sach = db.Saches.Where(s => s.Id == maSach)
        //        .Select(s => new SachChiTietDTO
        //        {
        //            MaSach = s.Id,
        //            TenSach = s.TenSach,
        //            TacGia = string.Join(", ", s.TacGias.Select(t => t.TenTG)),
        //            TheLoai = string.Join(", ", s.TheLoais.Select(t => t.TenTL)),
        //            NhaXuatBan = s.NhaXuatBan.TenNXB,
        //            NamXuatBan = s.NamXuatBan,
        //            SoLuongTon = db.Khoes.Where(k => k.IdSach == maSach).Select(k => k.SoLuongTon).FirstOrDefault(),
        //            ChiTietPhieuNhap = db.CT_PhieuNhap
        //                .Where(p => p.MaSach == maSach)
        //                .Select(p => new ChiTietPhieuNhapDTO
        //                {
        //                    TenNCC = p.NhaCungCap.TenNCC,
        //                    SoLuongNhap = p.SoLuongNhap,
        //                    DonGiaNhap = p.DonGiaNhap,
        //                    DonGiaBan = p.DonGiaBan,
        //                    NgayNhap = p.PhieuNhapSach.NgayNhapSach,
        //                    ThanhTien = p.DonGiaNhap*p.SoLuongNhap
        //                }).ToList()
        //        }).FirstOrDefault();

        //    return sach;
        //}

        public SachChiTietDTO GetSachDetailsById(int maSach)
        {
            var sach = db.Saches
                .Where(s => s.Id == maSach)
                .Select(s => new
                {
                    MaSach = s.Id,
                    TenSach = s.TenSach,
                    TacGia = s.TacGias.Select(t => t.TenTG), // Truy vấn danh sách
                    TheLoai = s.TheLoais.Select(t => t.TenTL), // Truy vấn danh sách
                    NhaXuatBan = s.NhaXuatBan.TenNXB,
                    NamXuatBan = s.NamXuatBan,
                    SoLuongTon = db.Khoes.Where(k => k.IdSach == maSach).Select(k => k.SoLuongTon).FirstOrDefault(),
                    ChiTietPhieuNhap = db.CT_PhieuNhap
                        .Where(p => p.MaSach == maSach)
                        .Select(p => new ChiTietPhieuNhapDTO
                        {
                            //TenNCC = p.NhaCungCap.TenNCC,
                            SoLuongNhap = p.SoLuongNhap,
                            DonGiaNhap = p.DonGiaNhap,
                            DonGiaBan = p.DonGiaBan,
                            NgayNhap = p.PhieuNhapSach.NgayNhapSach,
                            ThanhTien = p.DonGiaNhap * p.SoLuongNhap
                        }).ToList()
                })
                .AsEnumerable() // Chuyển sang LINQ to Objects
                .Select(s => new SachChiTietDTO
                {
                    MaSach = s.MaSach,
                    TenSach = s.TenSach,
                    TacGia = string.Join(", ", s.TacGia), // Xử lý trong bộ nhớ
                    TheLoai = string.Join(", ", s.TheLoai), // Xử lý trong bộ nhớ
                    NhaXuatBan = s.NhaXuatBan,
                    NamXuatBan = s.NamXuatBan,
                    SoLuongTon = s.SoLuongTon,
                    ChiTietPhieuNhap = s.ChiTietPhieuNhap
                })
                .FirstOrDefault();

            return sach;
        }

    }
}
