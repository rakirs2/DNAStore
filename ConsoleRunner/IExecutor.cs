using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunner
{
    internal interface IExecutor
    {
        /// <summary>
        /// Executes the request
        /// </summary>
        void Run();
    }
}
