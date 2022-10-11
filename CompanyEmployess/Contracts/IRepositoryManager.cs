using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IAppliancesRepository Appliances { get; }
        IFurnitureRepository Furniture { get; }
        void Save();
    }
}
