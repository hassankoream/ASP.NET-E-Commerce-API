using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } //GUID: Created from the [FrontEnd]
       public ICollection<BasketItem> Items { get; set; } = [];
    }
}
