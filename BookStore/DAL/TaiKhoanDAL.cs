using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.Entity;

namespace DAL
{
    public class TaiKhoanDAL
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        public TaiKhoan GetTaiKhoanByEmail(string email)
        {
            return db.TaiKhoans.FirstOrDefault(x => x.Email == email);
        }

        public int GetIdNhanVienByEmail(string email)
        {
            var taiKhoan = db.TaiKhoans.Include(tk => tk.NhanViens)
                                       .FirstOrDefault(tk => tk.Email == email);
            return taiKhoan?.NhanViens?.FirstOrDefault()?.Id ?? 0; // Trả về Id nhân viên hoặc 0 nếu không tìm thấy
        }

        public List<TaiKhoan> GetAllTaiKhoan()
        {
            return db.TaiKhoans.ToList();
        }

        public List<TaiKhoan> GetTaiKhoanChuaCoNguoiDung()
        {
            return db.TaiKhoans.Where(t => !db.NhanViens.Any(nv => nv.IdTaiKhoan == t.Id))
             .ToList();
        }

        // Thêm khách hàng mới
        public void AddTaiKhoan(TaiKhoan taiKhoan)
        {
            db.TaiKhoans.Add(taiKhoan);
            db.SaveChanges();
        }

        public bool UpdateTaiKhoan(TaiKhoan taiKhoan)
        {
            var existingTaiKhoan = db.TaiKhoans.FirstOrDefault(i => i.Id == taiKhoan.Id);
            if (existingTaiKhoan != null)
            {
                // Cập nhật thông tin
                existingTaiKhoan.Email = taiKhoan.Email;
                existingTaiKhoan.IdVaiTro = taiKhoan.IdVaiTro;

                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteTaiKhoan(int id)
        {
            var taiKhoan = db.TaiKhoans.FirstOrDefault(i => i.Id == id);
            if (taiKhoan != null)
            {
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<TaiKhoan> FindTaiKhoanByNameOfEmployee(string name)
        {
            return db.TaiKhoans
                     .Where(tk => tk.NhanViens.Any(nv => nv.HoTenNV.Contains(name)))
                     .ToList();
        }
    }
}
