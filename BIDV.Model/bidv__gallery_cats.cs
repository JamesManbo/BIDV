//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIDV.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class bidv__gallery_cats
    {
        public bidv__gallery_cats()
        {
            this.bidv__gallery = new HashSet<bidv__gallery>();
        }
    
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int created { get; set; }
        public int total { get; set; }
        public int uid { get; set; }
        public string uname { get; set; }
    
        public virtual ICollection<bidv__gallery> bidv__gallery { get; set; }
    }
}