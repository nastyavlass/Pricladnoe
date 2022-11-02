using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IApplianceRepository
    {
        IEnumerable<Appliance> GetAllAppliances(bool trackChanges);
        Appliance GetAppliance(Guid companyId, bool trackChanges);
        void CreateAppliance(Appliance appliance);
        IEnumerable<Appliance> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
