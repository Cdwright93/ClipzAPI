using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClipzAPI.Models
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        [ForeignKey("User")]
        public string ServicerId { get; set; }
        [ForeignKey("User")]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ServicerName { get; set; }
        public string Cost { get; set; }
        public string Status { get; set; }
    }
}
