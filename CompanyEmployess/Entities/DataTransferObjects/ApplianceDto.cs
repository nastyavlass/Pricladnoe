﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ApplianceDto
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Purpose { get; set; }
        public double Price { get; set; }
    }
}
