using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;

namespace Services.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecifications: BaseSpecifications<Order, Guid>
    {
        //Get All orders by email
        public OrderSpecifications(string Email): base(O=> O.UserEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesc(O=> O.OrderDate);
        }

        //Get order by id
        public OrderSpecifications(Guid id) : base(O => O.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);

        }
    }
}
