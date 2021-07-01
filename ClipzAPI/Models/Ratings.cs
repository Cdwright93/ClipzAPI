using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClipzAPI.Models
{
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AspNetUsers AspNetUsers { get; set; }
        public int Rating { get; set; }

    }
}
