using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServiceManager
    {
        //ProductService
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyProductService.Value;

        //BasketService
        private readonly Lazy<IBasketService> _lazybasketService = new Lazy<IBasketService> (() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _lazybasketService.Value;

        //AuthenticationService
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, mapper));

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        //orderService
        private readonly Lazy<IOrderService> _lazyorderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));
        public IOrderService orderService => _lazyorderService.Value;
    }
}
