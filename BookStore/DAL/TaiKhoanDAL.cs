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

        public string GetTenNhanVienByEmail(string email)
        {
            var taiKhoan = db.TaiKhoans.Include(tk => tk.NhanViens) // Bao gồm thông tin nhân viên
                                .FirstOrDefault(tk => tk.Email == email);
            return taiKhoan?.NhanViens?.FirstOrDefault()?.HoTenNV; // Trả về tên nhân viên hoặc null
        }

    }
}
