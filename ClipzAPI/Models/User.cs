using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClipzAPI.Models
{
    public class AspNetUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool Is_servicer { get; set; }
        public double Overall_rating { get; set; }
        public bool Is_working { get; set; }
        public int Service_distance { get; set; }

        public virtual ICollection<Ratings> Ratings { get; set; }
        public virtual ICollection<Services> Services { get; set; }
    }
}
