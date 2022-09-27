using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;
using Game;

namespace AI
{
    public static class MonteCarloSearch
    {
        public static double ucb_score(Node parent, Node child)
        {
            var prior_score = child.prior * Math.Sqrt(parent.visitCount) / (child.visitCount + 1);
            double valuescore = 0;
            if (child.visitCount > 0)
            {
                valuescore = -child.Value();
            }
            else
            {
                valuescore = 0;
            }
            return valuescore + prior_score;
        }
        public class Node
        {
            public int visitCount;
            public int toplay;
            public double prior;
            public double valuesum;
            public Dictionary<int,Node> children; // Probably a dict
            public BackendBoard board_state;
            public Node(double prior, int toplay)
            {
                if (double.IsNaN(prior))
                {

                }
                visitCount = 0;
                this.toplay = toplay;
                this.prior = prior;
                valuesum = 0;
                children = new Dictionary<int, Node>();
            }
            public bool IsExpanded()
            {
                return children.Count != 0;
            }
            public double Value()
            {
                if (visitCount == 0)
                {
                    return 0;
                }
                return valuesum / visitCount;
            }
            public int SelectAction(int temperature)
            {

                NDarray<int> actions = np.array(children.Keys.ToArray());
                NDarray<int> visitcounts = np.array(children.Select(c=>c.Value.visitCount).ToArray()); //.values()
                int action = 0;

                if (temperature == 0)
                {
                    action = actions[np.argmax(visitcounts)].item<int>(0);                
                }
                else if (float.IsInfinity(temperature))
                {
                    action = np.random.choice(actions).item<int>(0);
                }
                else
                {
                    var visit_count_distribution = children.Select(c=>Math.Pow(c.Value.visitCount, (1/temperature))).ToArray();
                    visit_count_distribution = visit_count_distribution.Select(n=>n/ visit_count_distribution.Sum(i => i)).ToArray();
                    NDarray<double> vcd = np.array(visit_count_distribution);
                    action = np.random.choice(actions, null, true, vcd).item<int>(0);
                }
                return action;
            }

            public KeyValuePair<int,Node> SelectChild()
            {
                var best_score = float.NegativeInfinity;
                int best_action = -1;
                Node best_child = null; //AHHHH
                foreach (var child in children)
                {
                    var score = (float)ucb_score(this, child.Value);
                    if (score > best_score)
                    {
                        best_score = score;
                        best_action = child.Key;
                        best_child = child.Value;
                    }
                }
                if (best_child == null)
                {
                    throw new Exception("No children in dictionary");
                }
                return new KeyValuePair<int, Node>(best_action, best_child);
            }
            public void Expand(BackendBoard board_state, int toplay, NDarray<float> action_probs)
            {
                this.toplay = toplay;
                this.board_state = board_state.Flipp(1); //Copy it
                for (int i = 0; i < action_probs.len; ++i)
                {
                    var prob = action_probs.item<double>(i); //Just a foreach
                    if (prob != 0)
                    {
                        children[i] = new Node(prob, toplay*-1); //This is a weird way of accomplishing this
                    }
                }
            }
        }

        public class MCTS
        {
            public Dictionary<string, int> args; //Potentially add extra arguments at some points
            public MCTS(Dictionary<string, int> args)
            {
                this.args = args;
            }
            public Node Run(TorchNetwork model, BackendBoard board_state, int toplay)
            {
                var root = new Node(0,toplay);
     
                //Expanding root
                var m_action = model.Predict(board_state.board);
                var action_probs = m_action.probabilities;
                var value = m_action.v;
                var validmoves = board_state.ValidMoves();
                action_probs = action_probs.Multiply(validmoves);
                action_probs = action_probs.Divide(action_probs.Sum());
                root.Expand(board_state, toplay, np.array<float>(action_probs));

                //Run simulations
                for (int i = 0; i < args["num_simulations"]; ++i)
                {
                    var node = root;
                    int action = 0;
                    var searchpath = new List<Node>() {node};
                    while (node.IsExpanded())
                    {
                        var actionnode = node.SelectChild();
                        action = actionnode.Key;
                        node = actionnode.Value;
                        searchpath.Add(node);
                    }
                    var parent = searchpath[searchpath.Count-2]; // second last item
                    board_state = parent.board_state.Flipp(1); //This is actually a reference... could cause errors
                    //We are at a leaf node and need to expand
                    //Always play from out perspective (us:1, enemy:-1)
                    var nextboardstate = board_state.NextState(1, action); //1 is us
                    nextboardstate = nextboardstate.Flipped(); //Flip the board ot the other players perspective

                    //Get value of the board state from the perspective of the other player
                    value = nextboardstate.GetReward(1);
                    if (value == 0 && nextboardstate.empty_squares.Count >= 1)
                    {
                        //Game has not ended
                        //ZOOM IN AND ENHANCE
                        //(Expand)

                        m_action = model.Predict(nextboardstate.board);
                        action_probs = m_action.probabilities;
                        value = m_action.v;
                        validmoves = nextboardstate.ValidMoves();
                        action_probs = action_probs.Multiply(validmoves);
                        action_probs = action_probs.Divide(action_probs.Sum());
                        node.Expand(nextboardstate, parent.toplay * -1, np.array<float>(action_probs));
                    }
                    BackPropogate(searchpath, value,parent.toplay *-1);
                }
                return root;
            }

            private void BackPropogate(List<Node> searchpath, double value, int toplay)
            {
                for (int i = searchpath.Count-1; i >= 0; --i) //Go backwards
                {
                    var node = searchpath[i];
                    node.valuesum += (node.toplay == toplay ? value : -value);
                    node.visitCount++;
                }
            }
        }
    }
}
