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
    
    public partial class TacGia
    {
        public TacGia()
        {
            this.Saches = new HashSet<Sach>();
        }
    
        public int Id { get; set; }
        public string TenTG { get; set; }
    
        public virtual ICollection<Sach> Saches { get; set; }
    }
}