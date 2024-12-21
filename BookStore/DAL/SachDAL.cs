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
    }
}
