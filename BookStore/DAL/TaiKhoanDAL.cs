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

    }
}
