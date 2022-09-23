using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    internal static class Extensions
    {
        public static List<int> Copy(this List<int> tocopy)
        {
            List<int> result = new List<int>();
            foreach (var item in tocopy)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
