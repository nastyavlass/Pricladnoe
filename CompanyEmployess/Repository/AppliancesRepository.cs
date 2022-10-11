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
    public class AppliancesRepository : RepositoryBase<Appliances>, IAppliancesRepository
    {
        public AppliancesRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Appliances> GetAllAppliances(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.Type).ToList();
    }
}
