using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Appliance
    {
        [Column("ApplianceId")]
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Purpose { get; set; }
        public double Price { get; set; }
    }
}
