using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateAppProg3.Infrastructure.Identity.Models
{
    public class ApplicationUser
    {
        public string Name {  get; set; }
        public string LastName {  get; set; }
        public string? ImgUser { get; set; }
        public string? Identification { get; set; }
    }
}
