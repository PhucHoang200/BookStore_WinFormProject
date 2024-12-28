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


    }
}
