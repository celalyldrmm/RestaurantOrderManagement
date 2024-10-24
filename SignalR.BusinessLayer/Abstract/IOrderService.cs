﻿using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IOrderService: IGenericService<Order>
    {
        decimal TLastOrderPrice();
        int TActiveOrderCount();
        int TTotalOrderCount();
        decimal TTodayTotalPrice();
    }
}
