﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

namespace AI
{
    public class DoubleTuple
    {
        public float[] probabilities;
        public float v;

        public DoubleTuple(float[] x, float v)
        {
            this.probabilities = x;
            this.v = v;
        }
    }
    public class TorchNetwork : nn.Module
    {
        private Linear fc1;
        private Linear fc2;

        public Linear actionHead;
        private Linear valueHead;

        public Device device;

        public TorchNetwork(string name, int boardsize, int actionsize) : base(name)
        {
            this.fc1 = nn.Linear(boardsize, 336);
            this.fc2 = nn.Linear(336, 336);


            this.actionHead = nn.Linear(336, actionsize);
            this.valueHead = nn.Linear(336, 1);

            this.to(torch.device("cpu"));
            this.device = torch.device("cpu");

            this.name = name;

            RegisterComponents();
        }

        public struct TensorTuple
        {
            public Tensor tensor1;
            public Tensor tensor2;

            public TensorTuple(Tensor tensor1, Tensor tensor2)
            {
                this.tensor1 = tensor1;
                this.tensor2 = tensor2;
            }
        }
        public TensorTuple Foward(Tensor x)
        {
            x = nn.functional.relu(fc1.cpu().forward(x));
            x = nn.functional.relu(fc2.cpu().forward(x)); //.foward????? only method that works

            var action_logits = actionHead.forward(x);
            var value_logit = valueHead.forward(x);

            Tensor tanh_v = tanh(value_logit);
            
            Softmax softmax = nn.Softmax(x.Dimensions-1);

            Tensor tensor_v = softmax.forward(action_logits);
            softmax.Dispose();
            return new TensorTuple(tensor_v, tanh_v);
        }
        public DoubleTuple Predict(Array data)
        {
            Tensor board_tensor = from_array(data).to(device); //Could not find 'FloatTensor' after extensive research, discovered it was obselete
            no_grad(); //Disable gradients
            var tuple = Foward(board_tensor);
            enable_grad(); //Re-enable gradients
            
            var x = tuple.tensor1.cpu().data<float>().ToArray(); //Probability representation in board int[] format
            var y = tuple.tensor2.cpu().data <float>()[0]; //Array only ever has one item
         //   float[] x1 = new float[7] { 0.14285714f, 0.14285714f, 0.14285714f, 0.14285714f, 0.14285714f, 0.14285714f, 0.14285714f};
            return new DoubleTuple(x, y);
        }
    }
}
