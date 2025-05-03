using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferObjects.ProductmoduleDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager): ControllerBase
    {
        //Building the methods

        //GetAllProducts
        [HttpGet] //Get: BaseUrl/api/Products

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQueryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(productQueryParams);
            return Ok(products);
        }

        //GetById

        [HttpGet("{id:int}")] //Get: BaseUrl/api/Products/10

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        //GetBrands

        [HttpGet("brands")] //Get: BaseUrl/api/Products/brands

        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);  
        }

        //GetTypes

        [HttpGet("types")] //Get: BaseUrl/api/Products/types

        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
