using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace Shared.DataTransferObjects.OrderModuleDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;

        public int DeliveryMethodId { get; set; }

        public AddressDto Address { get; set; } = default!;
    }
}
