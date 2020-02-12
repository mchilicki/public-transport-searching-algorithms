using Chilicki.Ptsa.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Data.Repositories.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : BaseEntity
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }
    }
}
