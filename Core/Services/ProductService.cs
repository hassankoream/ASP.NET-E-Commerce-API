using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServicesAbstractions;
using Shared.DataTransferObjects;

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
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            //var Repo = _uniteOfWork.GetRepository<Product, int>();
            var Products = await _uniteOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

        }
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _uniteOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Product = await _uniteOfWork.GetRepository<Product, int>().GetByIdAsync(id);

            return _mapper.Map<Product, ProductDto>(Product);


        }


    }
}
