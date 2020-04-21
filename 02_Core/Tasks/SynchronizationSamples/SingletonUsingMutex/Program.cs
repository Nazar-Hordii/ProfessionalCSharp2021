﻿using System;
using System.Threading;

namespace SingletonUsingMutex
{
    class Program
    {
        static void Main()
        {
            var mutex = new Mutex(false, "SingletonAppMutex", out bool mutexCreated);
            if (!mutexCreated)
            {
                Console.WriteLine("You can only start one instance of the application.");
                Console.WriteLine("Exiting.");
                return;
            }
            Console.WriteLine("Application running");
            Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }
    }
}
