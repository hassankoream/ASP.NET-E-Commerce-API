﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace ServicesAbstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
