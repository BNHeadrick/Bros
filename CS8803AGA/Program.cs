using System;
using CS8803AGA.learning;

namespace CS8803AGA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            /*Console.WriteLine("main thingy");

            ActionNode head = new ActionNode(ActionNode.EMPTY);

            ActionNode training = new ActionNode(1);
            training.addLeaf(new ActionNode(4));
            training.addLeaf(new ActionNode(2));
            training.addLeaf(new ActionNode(1));
            training.addLeaf(new ActionNode(3));

            //training.debugPrint();

            head.merge(training);

            training = new ActionNode(2);
            training.addLeaf(new ActionNode(5));
            training.addLeaf(new ActionNode(6));
            training.addLeaf(new ActionNode(1));

            head.merge(training);

            head.debugPrint();


            Console.WriteLine("end main thingy");*/

            //TestHarness.WorldBuilder.test();
            using (Engine game = new Engine())
            {
                game.Run();
            }
        }
    }
}

