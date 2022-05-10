using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BIDV.BaseSecurity
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (UserType == (int)Enum.Parse(typeof(Config.TypeUser), role))
            {
                return true;    
            }
            return false;
        }

        public CustomPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        
    }
}