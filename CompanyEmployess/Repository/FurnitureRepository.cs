using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FurnitureRepository : RepositoryBase<Furniture>, IFurnitureRepository
    {
        public FurnitureRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Furniture> GetAllFurniture(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.Type).ToList();
    }
}
