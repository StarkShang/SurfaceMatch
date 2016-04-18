using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace testDLLCSharp
{
    class Program
    {
        [DllImport("testDLL.dll",EntryPoint ="add")]
        public static extern int add(int x, int y);

        static void Main(string[] args)
        {
            Console.WriteLine(add(1, 2));
        }
    }
}
