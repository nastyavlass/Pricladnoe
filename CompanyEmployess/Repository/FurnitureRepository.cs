using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FurnitureRepository : RepositoryBase<Appliances>, Contracts.IFurnitureRepository
    {
        public FurnitureRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
