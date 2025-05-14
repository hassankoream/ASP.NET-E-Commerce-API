using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrderModuleDtos;

namespace Services.MappingProfiles
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;

            }
            else
            {
                //var Url = $"https://localhost:7052/{source.PictureUrl}";
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
