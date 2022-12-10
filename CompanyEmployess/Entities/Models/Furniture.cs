using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Furniture
    {
        [Column("FurnitureId")]
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Colour { get; set; }
        public double Price { get; set; }
    }
}
