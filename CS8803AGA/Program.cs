using System;
using CS8803AGA.learning;
using CS8803AGA.puzzle;

namespace CS8803AGA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //TestHarness.WorldBuilder.test();
            using (Engine game = new Engine())
            {
                game.Run();
            }
        }
    }
}

