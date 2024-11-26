using Microsoft.AspNetCore.Identity;

namespace eGovernmernt_Service.Models
{
    public class AppUser:IdentityUser
    {
        public string EmpID { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        
    }
}
