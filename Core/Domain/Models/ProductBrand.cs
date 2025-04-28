using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductBrand : BaseEntity<int>

    {
        public string Name { get; set; } = null!;

        //public ICollection<Product> products { get; set; } // no need to do this here
    }
}
