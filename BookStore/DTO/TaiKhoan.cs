//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            this.CT_DonHang = new HashSet<CT_DonHang>();
            this.NhanViens = new HashSet<NhanVien>();
        }
    
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] MatKhau { get; set; }
        public int IdVaiTro { get; set; }
    
        public virtual ICollection<CT_DonHang> CT_DonHang { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
        public virtual VaiTro VaiTro { get; set; }
    }
}
