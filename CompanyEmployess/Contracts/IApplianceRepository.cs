using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IApplianceRepository
    {
        Task<IEnumerable<Appliance>> GetAllAppliances(AppliancesParameters appliancesParameters, bool trackChanges);
        Task<Appliance> GetAppliance(Guid companyId, bool trackChanges);
        void CreateAppliance(Appliance appliance);
        Task<IEnumerable<Appliance>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteAppliance(Appliance appliance);
    }
}
