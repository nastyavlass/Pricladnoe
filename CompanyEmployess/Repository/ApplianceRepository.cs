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
    public class ApplianceRepository : RepositoryBase<Appliance>, IApplianceRepository
    {
        public ApplianceRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {
        }

        public IEnumerable<Appliance> GetAllAppliances(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.Type).ToList();

        public Appliance GetAppliance(Guid applianceId, bool trackChanges) => FindByCondition(c =>
            c.Id.Equals(applianceId), trackChanges).SingleOrDefault()!;
    }
}
