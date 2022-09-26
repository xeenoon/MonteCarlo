using MonteCarlo;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> arguments = new Dictionary<string, int>()
            {
                {"batch_size",64},
                {"numIters",500},
                {"num_simulations",100},
                {"numEps",100},
                {"numItersForTrainExamplesHistory",20},
                {"epochs",2},
            };
            var game = new BackendBoard(1, 4, 2);
            var model = new TorchNetwork("bob", 4, 4);

            var trainer = new Trainer(game, model, arguments);
            trainer.Learn();
            Console.WriteLine(model.Predict(game.board).probabilities.Write());
            Console.WriteLine("Finished training");

            Console.ReadLine();
        }
    }
}