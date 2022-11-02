using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFurnitureRepository
    {
        IEnumerable<Furniture> GetAllFurniture(bool trackChanges);
        Furniture GetFurniture(Guid companyId, bool trackChanges);
        void CreateFurniture(Furniture furniture);
        IEnumerable<Furniture> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
