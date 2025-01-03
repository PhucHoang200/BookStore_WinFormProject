using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuNhapSachVM
    {
        public int Id { get; set; }
        public System.DateTime NgayNhapSach { get; set; }
        public Nullable<decimal> TongTienNhap { get; set; }
    }
}
