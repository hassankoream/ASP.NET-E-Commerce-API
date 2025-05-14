using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.OrderModuleDtos;

namespace Presentation.Controllers
{
    [Authorize]
 

    public class OrderController(IServiceManager _serviceManager):ApiBaseController
    {
        //Create Order
        
        [HttpPost]//POST: BaseUrl/api/Order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email); //Get email from token

            //var order = await _serviceManager.orderService.CreateOrder(orderDto, Email!);

            var order = await _serviceManager.orderService.CreateOrder(orderDto, GetEmailFromToken());

            return Ok(order);
        }

        //Get Delivery Methods
        //

        [AllowAnonymous] //any one can access that endpoint
        [HttpGet("DeliveryMethods")] //GET: BaseUrl/api/Order/DeliveryMethods

        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManager.orderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }


        //Get All orders by email

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders = await _serviceManager.orderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }
        //Get Order By Id

        
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var order = await _serviceManager.orderService.GetOrderByIdAsync(Id);
            return Ok(order);
        }


    }
}
