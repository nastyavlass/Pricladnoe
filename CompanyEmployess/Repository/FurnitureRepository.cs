using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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

        public async Task<IEnumerable<Furniture>> GetAllFurniture(FurnitureParameters furnitureParameters, bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Type).Search(furnitureParameters.SearchTerm).Sort(furnitureParameters.OrderBy).ToListAsync();

        public async Task<Furniture> GetFurniture(Guid furnitureId, bool trackChanges) => await FindByCondition(c =>
            c.Id.Equals(furnitureId), trackChanges).SingleOrDefaultAsync()!;

        public void CreateFurniture(Furniture furniture) => Create(furniture);

        public async Task<IEnumerable<Furniture>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteFurniture(Furniture furniture)
        {
            Delete(furniture);
        }
    }
}
