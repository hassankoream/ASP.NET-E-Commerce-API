using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace ServicesAbstractions
{
    public interface IProductService
    {
        //TODO

        //GET ALL Products

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        //GET Product by ID

        Task<ProductDto> GetProductByIdAsync(int id);

        //GET Product Brand
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //GET Product Type

        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
