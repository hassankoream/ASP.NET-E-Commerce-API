using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Services.Specifications.OrderModuleSpecifications;
using ServicesAbstractions;
using Shared.DataTransferObjects.IdentityModuleDtos;
using Shared.DataTransferObjects.OrderModuleDtos;

namespace Services
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            //Map Address to OrderAddress

            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
            //Create orderItem List

            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach(var item in Basket.Items)
            {
                var ProductDB = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                OrderItem OrderItem = CreateOrderItem(item, ProductDB);
                orderItems.Add(OrderItem);

            }


            //Get Delivery Method

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryNotFoundException(orderDto.DeliveryMethodId);
            ///Calculate Sub Total

            var SubTotal = orderItems.Sum(I => I.Quantity * I.Price);


            //Create the order object

            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal);

          await  _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
          await  _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product ProductDB)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = ProductDB.Id, ProductName = ProductDB.Name, PictureUrl = ProductDB.PictureUrl },
                Price = ProductDB.Price,
                Quantity = item.Quntity
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            var DeliveryMethodsDtos = _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);

            return DeliveryMethodsDtos;
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return  _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var Spec = new OrderSpecifications(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);


        }
    }

  
}
