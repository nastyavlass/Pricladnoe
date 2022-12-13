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
    public class ApplianceRepository : RepositoryBase<Appliance>, IApplianceRepository
    {
        public ApplianceRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {
        }

        public async Task<IEnumerable<Appliance>> GetAllAppliances(AppliancesParameters appliancesParameters, bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Type).Search(appliancesParameters.SearchTerm).Sort(appliancesParameters.OrderBy).ToListAsync();

        public async Task<Appliance> GetAppliance(Guid applianceId, bool trackChanges) => await FindByCondition(c =>
            c.Id.Equals(applianceId), trackChanges).SingleOrDefaultAsync()!;

        public void CreateAppliance(Appliance appliance) => Create(appliance);

        public async Task<IEnumerable<Appliance>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteAppliance(Appliance appliance)
        {
            Delete(appliance);
        }
    }
}
