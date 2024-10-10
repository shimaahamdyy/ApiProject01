using Store.Data.Entities;
using Store.Repository.Specifications.ProductSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductWithSpecs
{
    public class ProductWithCountSpecification : BaseSpecification<Product>
    {
        public ProductWithCountSpecification(ProductSpecifications specs) :
            base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                            (!specs.TypeId.HasValue || product.BrandId == specs.TypeId.Value) &&
                            (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search)))
        {

        }

    }
}
