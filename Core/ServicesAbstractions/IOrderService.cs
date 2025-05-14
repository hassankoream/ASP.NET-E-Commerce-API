using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrderModuleDtos;

namespace ServicesAbstractions
{
    public interface IOrderService
    {
        //Create Order
        //Creating Order Will Take Basket Id , Shipping Address , Delivery Method Id , Customer Email
        //And Return Order Details
        //(Id , UserName , OrderDate , Items [Product Name - Picture Url - Price - Quantity]
        //, Address , Delivery Method Name , Order Status Value , Sub Total , Total Price  )

        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email);

        //GetALlDeliveryMethods
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        //GetAllOrders

        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);

        //GetOrderById
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
