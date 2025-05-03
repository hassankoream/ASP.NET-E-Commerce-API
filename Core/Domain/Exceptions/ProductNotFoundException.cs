using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException(int id): NotFoundException($"product with Id {id} Not Found")
    {
        //public ProductNotFoundException(string message):base(message)
        //{
            
        //}
    }
}
