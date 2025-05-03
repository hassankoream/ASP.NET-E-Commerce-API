using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IServiceManager
    {
        //ProductService
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyProductService.Value;

        //BasketService
        private readonly Lazy<IBasketService> _lazybasketService = new Lazy<IBasketService> (() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _lazybasketService.Value;
    }
}
