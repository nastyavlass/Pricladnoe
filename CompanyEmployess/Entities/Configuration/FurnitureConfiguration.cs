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
    public class FurnitureConfiguration : IEntityTypeConfiguration<Furniture>
    {
        public void Configure(EntityTypeBuilder<Furniture> builder)
        {
            builder.HasData
            (
            new Furniture
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-034715497d4a"),
                Type = "Sofa",
                Colour = "Red",
                Price = 2000,
            },
            new Furniture
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-044715497d4a"),
                Type = "Chair",
                Colour = "Green",
                Price = 200,
            },
            new Furniture
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-054715497d4a"),
                Type = "Table",
                Colour = "Red",
                Price = 700,
            });
        }
    }
}
