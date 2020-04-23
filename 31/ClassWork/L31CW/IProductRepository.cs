using System;
using System.Collections.Generic;
using System.Text;

namespace L31CW
{
    public interface IProductRepository
    {
        int GetProductCount();
        List<Tuple<int, string>> GetProductList();
        int AddProduct(string name, decimal price);
    }
}
