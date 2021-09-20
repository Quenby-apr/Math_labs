using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lab_2
{
    class Program
    {
        static void Main()
        {
            Lab myLab = new Lab();
            myLab.Work();
            Console.WriteLine("Для закрытия введите любую клавишу");
            string str = Console.ReadLine();
        }
    }
}
