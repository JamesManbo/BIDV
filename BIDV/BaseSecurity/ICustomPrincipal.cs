using System.Security.Principal;

namespace BIDV.BaseSecurity
{
    public interface ICustomPrincipal : IPrincipal
    {
        int ID { get; set; }
        string UserName { get; set; }
        int UserType { get; set; }
    }
}