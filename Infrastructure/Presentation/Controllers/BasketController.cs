using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get Basket
        [HttpGet] //Get: baseUrl/api/Basket
                  //
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(Basket);
        }
        //Create Or Update Basket
        [HttpPost] //Post: baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto); 
            return Ok(Basket);
        }
        //Delete Basket
        [HttpDelete] //Delete: baseUrl/api/Basket/fdfdd

        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }
    }
}
