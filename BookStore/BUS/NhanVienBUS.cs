using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        // Lấy danh sách nhân viên
        public List<NhanVien> GetAllNhanVien()
        {
            return nhanVienDAL.GetAllNhanVien();
        }

        // Thêm nhân viên mới với logic kiểm tra
        public string AddNhanVien(string hoTen, DateTime ngayBatDauCongViec, string soDienThoai, Decimal luong, int idTaiKhoan)
        {
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(ngayBatDauCongViec.ToString()) || string.IsNullOrWhiteSpace(soDienThoai) || string.IsNullOrWhiteSpace(luong.ToString()) || string.IsNullOrWhiteSpace(idTaiKhoan.ToString()))
            {
                return "Vui lòng điền đầy đủ thông tin!";
            }


            // Kiểm tra trùng lặp số điện thoại
            var existingPhone = nhanVienDAL.GetAllNhanVien().FirstOrDefault(i => i.SoDienThoai == soDienThoai);
            if (existingPhone != null)
            {
                return "Số điện thoại đã tồn tại.";
            }


            var nhanVien = new NhanVien
            {
                HoTenNV = hoTen,
                NgayBatDauNhanViec = ngayBatDauCongViec,
                SoDienThoai = soDienThoai,
                Luong = luong,
                IdTaiKhoan = idTaiKhoan
            };

            nhanVienDAL.AddNhanVien(nhanVien);
            return "Thêm nhân viên thành công!";
        }

        public string UpdateNhanVien(int Id, string hoTen, DateTime ngayBatDauCongViec, string soDienThoai, Decimal luong)
        {
            var existingNhanVien = nhanVienDAL.GetAllNhanVien().FirstOrDefault(i => i.Id == Id);
            if (existingNhanVien != null)
            {
                if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(ngayBatDauCongViec.ToString()) || string.IsNullOrWhiteSpace(soDienThoai) || string.IsNullOrWhiteSpace(luong.ToString()))
                {
                    return "Không được để trống thông tin!";
                }


                // Kiểm tra số điện thoại trùng lặp, loại trừ khách hàng hiện tại
                var existingPhone = nhanVienDAL.GetAllNhanVien()
                                        .FirstOrDefault(i => i.SoDienThoai == soDienThoai && i.Id != Id);
                if (existingPhone != null)
                {
                    return "Số điện thoại đã tồn tại.";
                }

                var nhanVien = new NhanVien
                {
                    Id = Id,
                    HoTenNV = hoTen,
                    NgayBatDauNhanViec = ngayBatDauCongViec,
                    SoDienThoai = soDienThoai,
                    Luong = luong
                };

                bool kq = nhanVienDAL.UpdateNhanVien(nhanVien);
                if (kq)
                {
                    return "Cập nhật thông tin nhân viên thành công";
                }
                else
                {
                    return "Cập nhật thông tin nhân viên thất bại";
                }
            }
            else
            {
                return "Mã nhân viên không tồn tại";
            }
        }

        public string DeleteNhanVien(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int parsedId))
            {
                return "Mã nhân viên không hợp lệ";
            }

            bool kq = nhanVienDAL.DeleteNhanVien(parsedId);
            if (kq)
            {
                return "Xóa nhân viên thành công";
            }
            else
            {
                return "Xóa nhân viên thất bại!";
            }
        }

        public List<NhanVien> FindNhanVienByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<NhanVien>();
            }
            return nhanVienDAL.FindNhanVienByName(name);
        }

        public NhanVien FindNhanVienById(int id)
        {
            return nhanVienDAL.FindNhanVienById(id);
        }
    }
}
