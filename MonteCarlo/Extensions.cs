using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    internal static class Extensions
    {
        static Random random = new Random();
        public static T RanChoice<T>(this T[] array)
        {
            return array[random.Next(0, array.Length)];
        }
        public static List<int> Copy(this List<int> tocopy)
        {
            List<int> result = new List<int>();
            foreach (var item in tocopy)
            {
                result.Add(item);
            }
            return result;
        }
        public static float[] Multiply(this float[] prob, int[] mask)
        {
            float[] result = new float[prob.Length];
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
        public static float[] Divide(this float[] prob, float num)
        {
            float[] result = new float[prob.Length];
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
        public static float[,] TwoDimensional(this float[][] arry)
        {
            float[,] result = new float[arry.Length, arry[0].Length];
            for (int col = 0; col < arry.Length; col++)
            {
                float[] column = arry[col];
                for (int row = 0; row < column.Length; row++)
                {
                    float item = column[row];
                    result[col, row] = item;
                }
            }
            return result;
        }
        public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> toAdd)
        {
            foreach (var element in toAdd)
            {
                bag.Add(element);
            }
        }
    }
}
