using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFurnitureRepository
    {
        Task<IEnumerable<Furniture>> GetAllFurniture(FurnitureParameters furnitureParameters, bool trackChanges);
        Task<Furniture> GetFurniture(Guid companyId, bool trackChanges);
        Task<IEnumerable<Furniture>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteFurniture(Furniture furniture);
        void CreateFurniture(Furniture furniture);
    }
}
