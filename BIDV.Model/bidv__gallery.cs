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
    
    public partial class bidv__gallery
    {
        public int id { get; set; }
        public int cat_id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public Nullable<int> changed { get; set; }
        public int created { get; set; }
        public string type { get; set; }
        public int uid { get; set; }
        public string uname { get; set; }
    
        public virtual bidv__gallery_cats bidv__gallery_cats { get; set; }
    }
}