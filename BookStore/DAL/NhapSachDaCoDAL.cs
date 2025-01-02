using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhapSachDaCoDAL
    {
        // Lấy danh sách sách (bao gồm Id và TenSach)
        public List<SachViewModel> GetSachList()
        {
            using (var context = new BookStoreDBEntities())
            {
                var sachList = context.Saches.Select(s => new SachViewModel
                {
                    Id = s.Id,
                    TenSach = s.TenSach
                }).ToList();

                return sachList;
            }
        }

        // Lấy thông tin chi tiết sách (tác giả, thể loại, nhà xuất bản, năm xuất bản, đơn giá nhập, đơn giá bán, nhà cung cấp)
        //public SachViewModel GetSachDetailsById(int sachId)
        //{
        //    using (var context = new BookStoreDBEntities())
        //    {
        //        var sach = context.Saches
        //            .Where(s => s.Id == sachId)
        //            .Select(s => new
        //            {
        //                s.Id,
        //                s.TenSach,
        //                TacGias = s.TacGias.Select(tg => tg.TenTG),  // Lấy danh sách tên tác giả mà không gọi ToList() trong truy vấn
        //                TheLoais = s.TheLoais.Select(tl => tl.TenTL), // Lấy danh sách tên thể loại mà không gọi ToList()
        //                s.NhaXuatBan.TenNXB,
        //                s.NamXuatBan,
        //                Kho = s.Khoes.FirstOrDefault(),
        //                NhaCungCaps = s.NhaCungCaps.Select(ncc => ncc.TenNCC) // Lấy danh sách tên nhà cung cấp mà không gọi ToList()
        //            })
        //            .FirstOrDefault();

        //        if (sach != null)
        //        {
        //            return new SachViewModel
        //            {
        //                Id = sach.Id,
        //                TenSach = sach.TenSach,
        //                TenTG = string.Join(", ", sach.TacGias),  // Nối các tên tác giả lại với nhau sau khi truy vấn
        //                TenTL = string.Join(", ", sach.TheLoais), // Nối các tên thể loại lại với nhau sau khi truy vấn
        //                TenNXB = sach.TenNXB,
        //                NamXuatBan = sach.NamXuatBan,
        //                DonGiaNhap = sach.Kho?.DonGiaNhap ?? 0,  // Kiểm tra null và cung cấp giá trị mặc định
        //                DonGiaBan = sach.Kho?.DonGiaBan ?? 0,    // Kiểm tra null và cung cấp giá trị mặc định
        //                TenNCC = string.Join(", ", sach.NhaCungCaps) // Nối các tên nhà cung cấp lại với nhau sau khi truy vấn
        //            };
        //        }

        //        return null; // Trả về null nếu không tìm thấy sách
        //    }
        //}
        public SachViewModel GetSachDetailsById(int sachId)
        {
            using (var context = new BookStoreDBEntities())
            {
                var sach = context.Saches
                    .Where(s => s.Id == sachId)
                    .Select(s => new
                    {
                        s.Id,
                        s.TenSach,
                        TacGias = s.TacGias.Select(tg => tg.TenTG),  // Lấy danh sách tên tác giả mà không gọi ToList() trong truy vấn
                        TheLoais = s.TheLoais.Select(tl => tl.TenTL), // Lấy danh sách tên thể loại mà không gọi ToList()
                        s.NhaXuatBan.TenNXB,
                        s.NamXuatBan,
                        Kho = s.Khoes.FirstOrDefault(),
                        NhaCungCapId = s.NhaCungCaps.Select(ncc => ncc.Id).FirstOrDefault() // Lấy Id của nhà cung cấp
                    })
                    .FirstOrDefault();

                if (sach != null)
                {
                    // Lấy tên nhà cung cấp từ NhaCungCapId
                    var nhaCungCap = context.NhaCungCaps
                        .Where(ncc => ncc.Id == sach.NhaCungCapId)
                        .Select(ncc => ncc.TenNCC)
                        .FirstOrDefault();

                    return new SachViewModel
                    {
                        Id = sach.Id,
                        TenSach = sach.TenSach,
                        TenTG = string.Join(", ", sach.TacGias),  // Nối các tên tác giả lại với nhau sau khi truy vấn
                        TenTL = string.Join(", ", sach.TheLoais), // Nối các tên thể loại lại với nhau sau khi truy vấn
                        TenNXB = sach.TenNXB,
                        NamXuatBan = sach.NamXuatBan,
                        DonGiaNhap = sach.Kho?.DonGiaNhap ?? 0,  // Kiểm tra null và cung cấp giá trị mặc định
                        DonGiaBan = sach.Kho?.DonGiaBan ?? 0,    // Kiểm tra null và cung cấp giá trị mặc định
                        TenNCC = nhaCungCap // Gán tên nhà cung cấp vào TenNCC
                    };
                }

                return null; // Trả về null nếu không tìm thấy sách
            }
        }

        public int SavePhieuNhap(decimal tongTien)
        {
            using (var context = new BookStoreDBEntities())
            {
                var phieuNhap = new PhieuNhapSach
                {
                    NgayNhapSach = DateTime.Now,
                    TongTienNhap = tongTien
                };

                context.PhieuNhapSaches.Add(phieuNhap);
                context.SaveChanges();
                return phieuNhap.Id;  // Trả về Id của phiếu nhập vừa được lưu
            }
        }

        




        // Lưu phiếu nhập và chi tiết phiếu nhập vào cơ sở dữ liệu
        //public int SavePhieuNhap(decimal tongTien, List<CT_PhieuNhap> chiTietPhieuNhaps)
        //{
        //    using (var context = new BookStoreDBEntities())
        //    {
        //        var phieuNhap = new PhieuNhapSach
        //        {
        //            NgayNhapSach = DateTime.Now,
        //            TongTienNhap = tongTien
        //        };

        //        context.PhieuNhapSaches.Add(phieuNhap);

        //        foreach (var chiTietPhieuNhap in chiTietPhieuNhaps)
        //        {
        //            var ctPhieuNhap = new CT_PhieuNhap
        //            {
        //                MaPhieuNhap = phieuNhap.Id,  // Lấy từ phiếu nhập sách đã lưu
        //                MaSach = chiTietPhieuNhap.MaSach,  // Lấy từ danh sách chi tiết
        //                MaNCC = chiTietPhieuNhap.MaNCC,  // Lấy từ NCC liên quan
        //                SoLuongNhap = chiTietPhieuNhap.SoLuongNhap,
        //                DonGiaNhap = chiTietPhieuNhap.DonGiaNhap,
        //                DonGiaBan = chiTietPhieuNhap.DonGiaBan,
        //                ThanhTien = chiTietPhieuNhap.ThanhTien
        //            };
        //            context.CT_PhieuNhap.Add(ctPhieuNhap);
        //        }

        //        context.SaveChanges();
        //        return phieuNhap.Id;
        //    }
        //}

        //public void UpdateKho(int sachId, int soLuongNhap, decimal donGiaNhap, decimal donGiaBan)
        //{
        //    using (var context = new BookStoreDBEntities())
        //    {
        //        var kho = context.Khoes.FirstOrDefault(k => k.IdSach == sachId);
        //        if (kho != null)
        //        {
        //            // Cập nhật thông tin trong kho
        //            kho.SoLuongTon += soLuongNhap;
        //            kho.DonGiaNhap = donGiaNhap;
        //            kho.DonGiaBan = donGiaBan;
        //        }
        //        else
        //        {
        //            // Nếu chưa có sách trong kho, thêm mới
        //            kho = new Kho
        //            {
        //                IdSach = sachId,
        //                SoLuongTon = soLuongNhap,
        //                DonGiaNhap = donGiaNhap,
        //                DonGiaBan = donGiaBan
        //            };
        //            context.Khoes.Add(kho);
        //        }

        //        context.SaveChanges();
        //    }
        //}

    }
}
