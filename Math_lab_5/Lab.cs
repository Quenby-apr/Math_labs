using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lab_5
{
    class Lab
    {
        private int numbers = 3;
        private double[] xArray = {0.083, 0.472, 1.347, 2.117, 2.947};
        private readonly double[] yArray = {-2.132, -2.013, -1.613, -0.842, 2.973};
        
        private void PolynomialCoefficientLagrange(double param)
        {
            double[] coefs = new double[xArray.Length];
            StringBuilder answer = new StringBuilder();
            double result = 0;
            for (int i = 0; i < coefs.Length; i++)
            {
                StringBuilder slag = new StringBuilder();
                double buf = 1;
                double bufResult = 1;
                for (int j=0;j< coefs.Length; j++)
                {
                    if (i!=j)
                    {
                        slag.Append(" * ");
                        buf *= (xArray[i] - xArray[j]);
                        bufResult *= (param - xArray[j]);
                        slag.Append("(x-" + Math.Round(xArray[j],numbers) +")");
                    }
                }
                coefs[i] = yArray[i] / buf;
                result += coefs[i] * bufResult;
                if (coefs[i] >= 0 && i>0)
                {
                    answer.Append(" + ");
                }
                if (coefs[i] < 0)
                {
                    answer.Append(" - ");
                }
                answer.Append(Math.Round(Math.Abs(coefs[i]),numbers));
                answer.Append(slag);
            }
            for (int i = 0; i < coefs.Length; i++)
            {
                Console.WriteLine("Коэффиент "+i+" Полинома Лагранжа равен: "+Math.Round(coefs[i],numbers));
            }

            Console.WriteLine("Аналитическая формула Лагранжа имеет вид: " + answer);
            Console.WriteLine("Полином Лагранжа в точке " + param+ " равен : " + Math.Round(result,numbers));
            Console.WriteLine();
            Console.WriteLine();
        }
        private void PolynomialCoefficientNewton(double param)
        {
            var difTable = DifTable();
            StringBuilder answer = new StringBuilder();
            double result = 0;
            for (int i=0;i<difTable.Length;i++)
            {
                StringBuilder slag = new StringBuilder();
                double bufResult = 1;
                for (int j = 0; j < i; j++)
                {
                    slag.Append(" * ");
                    bufResult *= (param - xArray[j]);
                    slag.Append("(x-" + Math.Round(xArray[j],numbers) + ")");
                }
                result += difTable[i] * bufResult;
                if (difTable[i] >= 0 && i > 0)
                {
                    answer.Append(" + ");
                }
                if (difTable[i] < 0)
                {
                    answer.Append(" - ");
                }
                answer.Append(Math.Round(Math.Abs(difTable[i]),numbers));
                answer.Append(slag);               
            }
            for (int i = 0; i < difTable.Length; i++)
            {
                Console.WriteLine("Коэффиент " + i + " Полинома Ньютона равен: " + Math.Round(difTable[i],numbers));
            }

            Console.WriteLine("Аналитическая формула Ньютона имеет вид: " + answer);
            Console.WriteLine("Полином Ньютона в точке " + param + " равен : " + Math.Round(result,numbers));
            Console.WriteLine();
            Console.WriteLine();
        }

        private double[] DifTable()
        {
            double[] answer = new double[xArray.Length];
            answer[0] = yArray[0];
            double[] difs = (double[])yArray.Clone();
            for (int i=0; i<answer.Length-1;i++)
            {
                for (int j=0;j<answer.Length-i-1;j++)
                {                  
                    difs[j] = (difs[j + 1] - difs[j]) / (xArray[j + i + 1] - xArray[j]);
                }
                answer[i + 1] = difs[0];
            }
            return answer;
        }

        private void Spline(double param)
        {
            int number = 1;
            for (int i = 0; i<xArray.Length;i++)
            {
                if (param>xArray[i])
                {
                    number = i+1;
                }
                else
                {
                    break;
                }
            }
            double[] vectorA = new double[xArray.Length - 1];
            double[] vectorB = new double[xArray.Length - 1];
            StringBuilder answer = new StringBuilder();
            answer.Append("Линейный сплайн представлен в виде системы :");
            answer.AppendLine();
            for (int i=0;i<vectorA.Length;i++)
            {
                vectorA[i] = (yArray[i + 1] - yArray[i]) / (xArray[i + 1] - xArray[i]);
                vectorB[i] = (yArray[i] - (vectorA[i]*xArray[i]));
                if (vectorA[i] < 0)
                {
                    answer.Append("-");
                }
                answer.Append(Math.Round(Math.Abs(vectorA[i]),numbers)+"x");
                if (vectorB[i] >= 0)
                {
                    answer.Append(" + ");
                }
                if (vectorB[i] < 0)
                {
                    answer.Append(" - ");
                }
                answer.Append(Math.Round(Math.Abs(vectorB[i]),numbers)+",   "+xArray[i]+"<=x<="+xArray[i+1]);
                answer.AppendLine();
            }
            if (number >= vectorA.Length)
            {
                number = vectorA.Length - 1;
            }
            double result = param * vectorA[number - 1] + vectorB[number - 1];
            for (int l =0; l<vectorA.Length;l++)
            {
                Console.WriteLine("Коэффициент a" + l + " равен: " + Math.Round(vectorA[l],numbers) + "  Коэффициент b" + l + " равен: " + Math.Round(vectorB[l],numbers));
            }
            Console.WriteLine(answer);
            Console.WriteLine("Линейный сплайн в точке " + param + " равен : " + Math.Round(result, numbers));
        }
       
        public void Work()
        {
            Console.WriteLine("Введите x0");
            double x0 = Convert.ToDouble(Console.ReadLine());
            PolynomialCoefficientLagrange(x0);
            PolynomialCoefficientNewton(x0);
            Spline(x0);
        }
    }
}
