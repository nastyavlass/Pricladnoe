using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class AppliancesConfiguration : IEntityTypeConfiguration<Appliance>
    {
        public void Configure(EntityTypeBuilder<Appliance> builder)
        {
            builder.HasData
            (
            new Appliance
            {
                Id = new Guid("81abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Dishwasher",
                Purpose = "To wash the dishes",
                Price = 1000,
            },
            new Appliance
            {
                Id = new Guid("82abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Microwave oven",
                Purpose = "Reheat food",
                Price = 500,
            },
            new Appliance
            {
                Id = new Guid("83abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Electric kettle",
                Purpose = "Heating the water",
                Price = 300,
            });
        }
    }
}
