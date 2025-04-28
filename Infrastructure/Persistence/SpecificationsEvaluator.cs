using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationsEvaluator
    {
        //Create Query
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity: BaseEntity<Tkey>
        {
            var Query = InputQuery;

            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
                
            }
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                //foreach (var exp in specifications.IncludeExpressions)
                //{
                //    Query = Query.Include(exp);
                //}

                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }


            return Query;
        }
    }
}
