using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifcation;

namespace Talabat.Repository.Data
{
    public static class SpecificationEvaluater<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> Spec)
        {
            var Query = inputQuery;
            if (Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }
            if (Spec.OrderBy is not null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }
            if (Spec.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDescending);
            }
            if (Spec.Includes != null)
            {
                Query = Spec.Includes.Aggregate(Query, (current, include) => current.Include(include));
            }
            if (Spec.IncludeString != null)
            {
                Query = Spec.IncludeString.Aggregate(Query, (current, include) => current.Include(include));
            }
            if (Spec.IsPagingEnabled)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }


            // Query = Spec.IncludeString.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
