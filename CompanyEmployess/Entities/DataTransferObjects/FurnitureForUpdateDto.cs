using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class FurnitureForUpdateDto
    {
        public string? Type { get; set; }
        public string? Colour { get; set; }
        public double Price { get; set; }
    }
}
