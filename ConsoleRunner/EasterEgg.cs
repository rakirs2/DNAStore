using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunner
{
    internal class EasterEgg : IExecutor
    {
        public void Run()
        {
            Console.WriteLine("Welcome to DNA Store, a C# based implementation of everything I can do with bioinformatics");
            Console.WriteLine("Perhaps this is the real test to see how committed " +
                              "I am to focusing on Biology and Research and applying everything I've learned");
            Console.WriteLine("If I implement everything on project Rosalind here, I think it's safe to say I'm ready and capable");
            Console.WriteLine("Today is 4/30/2025. I don't know why but I think this is where I need to go");
        }
    }
}
