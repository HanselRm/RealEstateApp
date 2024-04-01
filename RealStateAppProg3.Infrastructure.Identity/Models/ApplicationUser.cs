using Microsoft.AspNetCore.Identity;


namespace RealStateAppProg3.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name {  get; set; }
        public string LastName {  get; set; }
        public string? ImgUser { get; set; }
        public string? Identification { get; set; }
    }
}
