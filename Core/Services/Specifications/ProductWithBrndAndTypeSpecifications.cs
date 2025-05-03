using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared;

namespace Services.Specifications
{
    internal class ProductWithBrndAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products
        public ProductWithBrndAndTypeSpecifications(ProductQueryParams productQueryParams) :

            //if BrandId has value return true (false || true) this means match brands, if TypeId has value return true (false || true) this means match Types
            base(
                P => (!productQueryParams.BrandId.HasValue || P.BrandId == productQueryParams.BrandId) 
                &&(!productQueryParams.TypeId.HasValue || P.TypeId == productQueryParams.TypeId)
                &&(string.IsNullOrWhiteSpace(productQueryParams.SearchValue) || P.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))

        //Where(P => P.BrandId = BrandId && P.TypeId == TypeId)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            switch(productQueryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(productQueryParams.PageSize, productQueryParams.PageIndex);
        }
        
        
        //Get product with Id
        public ProductWithBrndAndTypeSpecifications(int id) : base(P => P.Id == id)

        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
