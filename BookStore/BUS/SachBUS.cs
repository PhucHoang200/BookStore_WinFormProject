using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class SachBUS
    {
        private SachDAL sachDAL = new SachDAL();

        public List<Sach> GetAllSach()
        {
            return sachDAL.GetAllSach();
        }

        public List<Sach> FindNhaXuatBanByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<Sach>();
            }
            return sachDAL.FindSachByName(name);
        }

        public List<CTSachVM> GetChiTietSachTrongKho()
        {
            return sachDAL.GetChiTietSachTrongKho();
        }

        public SachChiTietDTO GetSachDetailsById(int maSach)
        {
            return sachDAL.GetSachDetailsById(maSach);
        }

    }
}
