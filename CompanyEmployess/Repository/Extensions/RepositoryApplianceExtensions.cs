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
    public static class RepositoryApplianceExtensions
    {
        public static IQueryable<Appliance> Search(this IQueryable<Appliance> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(e => e.Type.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Appliance> Sort(this IQueryable<Appliance> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Type);

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<Appliance>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Type);
            return employees.OrderBy(orderQuery => orderQuery);
        }
    }
}
