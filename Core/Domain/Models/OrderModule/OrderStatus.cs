﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public enum OrderStatus
    {
        Pending = 0, 
        PaymentRecived = 1,
        PaymentFaild = 2,

    }
}
