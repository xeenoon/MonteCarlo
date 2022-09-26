using Numpy;
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
        public static double[] Multiply(this double[] prob, int[] mask)
        {
            double[] result = new double[prob.Length];
            for (int i = 0; i < prob.Length; ++i)
            {
                if (mask[i] == 1) //Can have item
                {
                    result[i] = prob[i]; //Add the item
                }
                //Otherwise it will stay as a zero
            }
            return result;
        }
        public static double[] Divide(this double[] prob, double num)
        {
            double[] result = new double[prob.Length];
            for (int i = 0; i < prob.Length; ++i)
            {

                result[i] = prob[i] / num; //Add the item
            }
            return result;
        }
        public static List<T> Shuffle<T>(this List<T> list)
        {
            Dictionary<int, T> randomdictionary = new Dictionary<int, T>();
            Random r = new Random();
            foreach (var item in list)
            {
                int random;
                do
                {
                    random = r.Next();
                } while (randomdictionary.ContainsKey(random));
                randomdictionary.Add(random, item);
            }
            return randomdictionary.OrderBy(t => t.Key).Select(t => t.Value).ToList();
        }
        public static List<T> ToList<T>(this NDarray<T> array)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.len; ++i)
            {
                list.Add(array.item<T>(i)); //Just a foreach
            }
            return list;
        }
    }
}
