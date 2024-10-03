using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class SpecificationEvaluator<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        //IEnumerable Vs IQueryable

        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> InputQuery , ISpecification<TEntity> specs)
        {
            var query = InputQuery;

            if(specs.Criteria is not null) 
                query = query.Where(specs.Criteria);  // query.Where(x => x.TypeId == 3)

            if (specs.OrderBy is not null)
                query = query.OrderBy(specs.OrderBy);

            if (specs.OrderByDescending is not null)
                query = query.OrderByDescending(specs.OrderByDescending);

            if (specs.IsPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Include.Aggregate(query, (Current, IncludeExpression) => Current.Include(IncludeExpression));

            return query;
        }
    }
}
