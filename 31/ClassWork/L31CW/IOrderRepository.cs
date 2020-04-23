using System;
using System.Collections.Generic;
using System.Text;

namespace L31CW
{
    public interface IOrderRepository
    {
        int GetOrderCount();
        List<Tuple<int, string, DateTimeOffset, double?>> GetOrderList();
        int AddOrder(
            int customerId,
            DateTimeOffset orderDate,
            double? discount,
            List<Tuple<int, int>> productIdCountList);
    }
}
