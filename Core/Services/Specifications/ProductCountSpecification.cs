using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared;

namespace Services.Specifications
{
    internal class ProductCountSpecification: BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams productQueryParams) :
        base(
        P => (!productQueryParams.BrandId.HasValue || P.BrandId == productQueryParams.BrandId)
        && (!productQueryParams.TypeId.HasValue || P.TypeId == productQueryParams.TypeId)
        && (string.IsNullOrWhiteSpace(productQueryParams.SearchValue) || P.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))
        {
            
        }
    }
}
