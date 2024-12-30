using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVienDAL
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        // Lấy danh sách khách hàng
        public List<NhanVien> GetAllNhanVien()
        {
            return db.NhanViens.ToList();
        }

        // Thêm khách hàng mới
        public void AddNhanVien(NhanVien nhanVien)
        {
            db.NhanViens.Add(nhanVien);
            db.SaveChanges();
        }

        public bool UpdateNhanVien(NhanVien nhanVien)
        {
            var existingNhanVien = db.NhanViens.FirstOrDefault(i => i.Id == nhanVien.Id);
            if (existingNhanVien != null)
            {
                // Cập nhật thông tin
                existingNhanVien.HoTenNV = nhanVien.HoTenNV;
                existingNhanVien.NgayBatDauNhanViec = nhanVien.NgayBatDauNhanViec;
                existingNhanVien.SoDienThoai = nhanVien.SoDienThoai;
                existingNhanVien.Luong = nhanVien.Luong;

                db.SaveChanges();
                return true;
            }
            return false;
        }

        // Xóa khách hàng
        public bool DeleteNhanVien(int id)
        {
            var nhanVien = db.NhanViens.FirstOrDefault(i => i.Id == id);
            if (nhanVien != null)
            {
                db.NhanViens.Remove(nhanVien);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<NhanVien> FindNhanVienByName(string name)
        {
            return db.NhanViens
                     .Where(kh => kh.HoTenNV.Contains(name))
                     .ToList();
        }

        public NhanVien FindNhanVienById(int id)
        {
            var nhanVien = db.NhanViens.FirstOrDefault(i => i.Id == id);
            if (nhanVien == null)
            {
                // Xử lý khi không tìm thấy nhân viên, ví dụ: trả về một giá trị mặc định hoặc ném lỗi.
                return null;
            }
            return nhanVien;
        }
    }
}
