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
    
    public partial class VaiTro
    {
        public VaiTro()
        {
            this.TaiKhoans = new HashSet<TaiKhoan>();
        }
    
        public int Id { get; set; }
        public string TenVaiTro { get; set; }
    
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}