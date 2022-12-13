using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryFurnitureExtensions
    {
        public static IQueryable<Furniture> Search(this IQueryable<Furniture> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(e => e.Type.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Furniture> Sort(this IQueryable<Furniture> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Type);

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<Furniture>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Type);
            return employees.OrderBy(orderQuery => orderQuery);
        }
    }
}
