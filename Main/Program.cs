using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorMethodLib;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Barsik !");
            RefactorMethod rf = new RefactorMethod();
            Console.WriteLine(rf.DelParam("#include <iostream>\nblablalba\nvoid Test(std::string&& *param, double x)\n{}","Test", "param"));
            Console.ReadKey();
        }
    }
}
