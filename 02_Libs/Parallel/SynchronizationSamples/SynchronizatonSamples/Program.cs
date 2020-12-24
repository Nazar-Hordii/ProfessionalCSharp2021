﻿using System;
using System.Threading.Tasks;

namespace SynchronizatonSamples
{
    class Program
    {
        static void Main()
        {
            int numTasks = 20;
            SharedState state = new();
            Task[] tasks = new Task[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }

            Task.WaitAll(tasks);

            Console.WriteLine($"summarized {state.State}");
        }
    }
}
