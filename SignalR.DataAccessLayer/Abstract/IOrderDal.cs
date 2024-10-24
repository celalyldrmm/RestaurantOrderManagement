using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IOrderDal:IGenericDal<Order>
    {
        decimal LastOrderPrice();
        int ActiveOrderCount();
        int TotalOrderCount();
        decimal TodayTotalPrice();
    }
}
