using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public interface ISpecification<T>
    {
        //Criteria  .Where(x => x.id == id )
        Expression<Func<T , bool>> Criteria { get; }

        //Includes
        List<Expression<Func<T, object>>> Include { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }


}
}
