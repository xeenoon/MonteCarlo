using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using Game;

namespace AI
{
    public static class Extensions
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
        public static List<T> ToList<T>(this NDarray<T> array)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.len; ++i)
            {
                list.Add(array.item<T>(i)); //Just a foreach
            }
            return list;
        
        }
        public static List<float> ToList(this torch.Tensor array)
        {
            List<float> list = new List<float>();
            for (int i = 0; i < array.size(0); ++i)
            {
                list.Add(array[i].item<float>()); //Just a foreach
            }
            return list;
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

        public static string Write(this Array array)
        {
            string result = "[";
            foreach (var item in array)
            {
                result += item.ToString();
                result += ",";
            }
            result = result.Substring(0,result.Length-1);
            result += "]";
            return result;
        }
    }
}
