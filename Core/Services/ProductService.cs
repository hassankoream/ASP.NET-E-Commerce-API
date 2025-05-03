using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using Services.Specifications;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferObjects.ProductmoduleDtos;

namespace Services
{
    public class ProductService(IUnitOfWork _uniteOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _uniteOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQueryParams)
        {
            var Repo = _uniteOfWork.GetRepository<Product, int>();

            var specifications = new ProductWithBrndAndTypeSpecifications(productQueryParams);
            var Products = await Repo.GetAllAsync(specifications);
            var AllproductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductsCount = Products.Count();
            var TotalCount = await Repo.CountAsync(new ProductCountSpecification(productQueryParams));
            return new PaginatedResult<ProductDto>(productQueryParams.PageIndex, ProductsCount, TotalCount,AllproductsDto );

        }
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _uniteOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrndAndTypeSpecifications(id);

            var Product = await _uniteOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            if (Product is null)
            {
                throw new ProductNotFoundException(id);
                
            }

            var ProductToReturn = _mapper.Map<Product, ProductDto>(Product);

            return ProductToReturn;


        }


    }
}
