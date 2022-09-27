using AI;
using Game;
using Numpy;

namespace ConsoleTester
{
    class Program
    {
        static int iters = 1000000;
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Randint...");
            int[] data1 = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            double[] data2 = new double[7] { 1, 1, 1, 1, 1, 1, 4 };

            var actions = np.array(data1);
            var visitcounts = np.array(data2);
            // var action =  np.random.choice(actions,null, true, visitcounts);
            //Console.WriteLine(Argmax(data1, data2))
            //Console.WriteLine(action);

            NDarray<int> sample_ids = np.random.randint(5, size: new int[1] { 15 });

            Console.WriteLine(sample_ids.ToList().ToArray().Write());
            Console.WriteLine(RandomSet(0,5,15).ToArray().Write());
            Console.WriteLine("Finished testing");
            Console.WriteLine("");
            return;

            Dictionary<string, int> arguments = new Dictionary<string, int>()
            {
                {"batch_size",64},
                {"numIters",500},
                {"num_simulations",100},
                {"numEps",100},
                {"numItersForTrainExamplesHistory",20},
                {"epochs",2},
            };
            var game = new BackendBoard(6, 7, 4);
            var model = new TorchNetwork("bob", 42, 7);

            var trainer = new Trainer(game, model, arguments, Write);
            trainer.Learn();
            Console.WriteLine(model.Predict(game.board).probabilities.Write());
            Console.WriteLine("Finished training");

            Console.ReadLine();
        }

        private static string EstimateProbabilities(int[] data1, double[] data2)
        {
            double[] estprob = new double[7];

            for (int i = 0; i < iters; ++i)
            {
                var rand = GetRandom(data1, data2);
                estprob[rand]++;
            }
            string result = "";
            for (int i = 0; i < 7; ++i)
            {
                estprob[i] = estprob[i] / iters;
                result += estprob[i];
                result += ",";
            }

            return result;
        }

        public static bool Write(string value)
        {
            Console.WriteLine(value);
            return false;
        }
        public static int Argmax(int[] data1, int[] data2)
        {
            return data1[Array.IndexOf(data2,data2.Max())];
        }
        static Random random = new Random();

        public static int GetRandom(int[] pool, double[] probabilities)
        {
            // get universal probability 
            double u = probabilities.Sum();

            // pick a random number between 0 and u
            double r = random.NextDouble() * u;

            double sum = 0;
            for (int i = 0; i < pool.Length; i++)
            {
                int n = pool[i];
                double p = probabilities[i];
                // loop until the random number is less than our cumulative probability
                if (r <= (sum = sum + p))
                {
                    return n;
                }
            }
            // should never get here
            return 0;
        }
        public static List<int> RandomSet(int min, int max, int length)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < length; ++i)
            {
                result.Add(random.Next(min,max));
            }
            return result;
        }
    }
}