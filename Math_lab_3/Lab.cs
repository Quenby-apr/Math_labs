using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lab_3
{
    class Lab
    {
        private double accuracy;
        private int numbers = 5;
        private void FindNumbers()  //метод поиска количества знаков для округления
        {
            numbers = 0;
            if (accuracy < 1 && accuracy > 0)
            {
                char[] chars = accuracy.ToString("##.############").ToCharArray();
                foreach (var item in chars)
                {
                    if (item == '0' || item == ',')
                    {
                        numbers++;
                    }
                }
            }
        }
        private double Function (double x)
        {
            return 0.5 * Math.Exp(-Math.Pow(x, 0.5)) - 0.2 * Math.Pow(x, 1.5) + 2;
        }
        public void Work()
        {
            Console.WriteLine("Введите погрешность");
            accuracy = Convert.ToDouble(Console.ReadLine());
            FindNumbers();
            Console.WriteLine("Введите x0");
            double x0 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите x1");
            double x1 = Convert.ToInt32(Console.ReadLine());
            double difference = 1;
            int count = 0;
            while (difference>accuracy)
            {
                double x = x0 - ((Function(x0) / (Function(x1) - Function(x0))) * (x1 - x0));
                difference = Math.Abs(x - x1);
                x0 = x1;
                x1 = x;
                Console.WriteLine(Math.Round(x, numbers));
                Console.WriteLine(Math.Round(difference,numbers+2));
                count++;
            }
            Console.WriteLine("Количество итераций: "+ count);
        }

    }
}
