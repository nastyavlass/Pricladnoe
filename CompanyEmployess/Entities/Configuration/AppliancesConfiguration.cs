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
    public class AppliancesConfiguration : IEntityTypeConfiguration<Appliances>
    {
        public void Configure(EntityTypeBuilder<Appliances> builder)
        {
            builder.HasData
            (
            new Appliances
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Dishwasher",
                Purpose = "To wash the dishes",
                Price = 1000,
            },
            new Appliances
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Microwave oven",
                Purpose = "Reheat food",
                Price = 500,
            },
            new Appliances
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024715497d4a"),
                Type = "Electric kettle",
                Purpose = "Heating the water",
                Price = 300,
            });
        }
    }
}
