using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServicesAbstractions;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
                throw new Exception("Cannot Create Or Update Basket Now, Try again later!");
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeleteBasketAsync(Key);

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
                throw new BasketNotFoundException(Key);

        }
    }
}
