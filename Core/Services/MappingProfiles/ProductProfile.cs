using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared.DataTransferObjects;

namespace Services.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, Options => Options.MapFrom(Src => Src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, Options => Options.MapFrom(Src => Src.ProductType.Name))
                //.ForMember(dist => dist.PictureUrl, Options => Options.MapFrom(Src => $"https://localhost:7052/{Src.PictureUrl}"));
                .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
