using System;
using System.Collections.Generic;
using System.Text;

namespace L15CW
{
    static class Calculator
    {
        public static bool Add<T>(T a, T b, out T res)
        {
            try
            {
                res = (T)((dynamic)a * (dynamic)b);
            }
            catch (Exception)
            {
                res = default;
                return false;
            }

            return true;
        }
    }
}
