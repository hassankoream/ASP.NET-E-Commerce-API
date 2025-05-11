using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace Services.MappingProfiles
{
    internal class Identityprofile: Profile
    {
        public Identityprofile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
