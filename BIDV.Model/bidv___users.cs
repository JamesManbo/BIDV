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
    
    public partial class bidv___users
    {
        public bidv___users()
        {
            this.bidv___logs = new HashSet<bidv___logs>();
            this.bidv___user_block = new HashSet<bidv___user_block>();
            this.bidv___roles = new HashSet<bidv___roles>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int created { get; set; }
        public short is_active { get; set; }
        public string reg_ip { get; set; }
        public string last_ip { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> last_action { get; set; }
        public Nullable<int> last_changepass { get; set; }
        public Nullable<int> last_login { get; set; }
        public Nullable<short> gender { get; set; }
        public string mobile_phone { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public short status { get; set; }
        public string image { get; set; }
    
        public virtual ICollection<bidv___logs> bidv___logs { get; set; }
        public virtual ICollection<bidv___user_block> bidv___user_block { get; set; }
        public virtual ICollection<bidv___roles> bidv___roles { get; set; }
    }
}