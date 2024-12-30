using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VaiTroDAL
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();
        public List<VaiTro> GetAllVaiTro()
        {
            return db.VaiTroes.ToList();
        }
    }
}
