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
    
    public partial class bidv___block
    {
        public int id { get; set; }
        public int module_id { get; set; }
        public int page_id { get; set; }
        public string region { get; set; }
        public int position { get; set; }
        public Nullable<short> mobile { get; set; }
    
        public virtual bidv___module bidv___module { get; set; }
        public virtual bidv___page bidv___page { get; set; }
    }
}
