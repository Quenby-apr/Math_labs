using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lab_2
{
    class Lab
    {
        private double[][] myMatrix = new double[][]
        {
            new double[]{2.958, 0.147, 0.354, 0.238 },
            new double[]{0.127, 2.395, 0.256, 0.273 },
            new double[]{0.403, 0.184, 3.815, 0.416 },
            new double[]{0.259, 0.361, 0.281, 3.736 }
        };
        private double[] vectorFreeTerms = { 0.651, 0.898, 0.595, 0.389 };
        private double[] vectorX = { 0.651, 0.898, 0.595, 0.389 };
        private double errorRate = 1;
        private int numbers = 5;
        private double accuracy = 1;
        private double[,] FindDiagMatrix()
        {
            double[,] diagMatrix = new double[myMatrix.Length, myMatrix.Length];
            for (int i = 0; i < myMatrix.Length; i++)
            {
                for (int j = 0; j < myMatrix[0].Length; j++)
                {
                    if (i == j)
                    {
                        diagMatrix[i, j] = myMatrix[i][j];
                    }
                    else
                    {
                        diagMatrix[i, j] = 0;
                    }
                }
            }
            return diagMatrix;
        }
        private void FindVectorX(double[,] diagmatrix)
        {
            double[,] inverseMatrix = diagmatrix; //создание обратной диагональной матрицы
            for (int i = 0; i < Math.Sqrt(inverseMatrix.Length); i++) 
            {
                for (int j = 0; j < Math.Sqrt(inverseMatrix.Length); j++)
                {
                    if (i == j)
                    {
                        inverseMatrix[i, j] = 1 / inverseMatrix[i, j];
                    }
                    else
                    {
                        inverseMatrix[i, j] = 0;
                    }
                }
            }
            double[] FreeTerms = new double[vectorFreeTerms.Length];
            for (int i = 0; i < vectorX.Length; i++)
            {
                FreeTerms[i] = vectorX[i] * inverseMatrix[i, i];// поиск вектора свободных членов c
            }
            vectorX = FreeTerms;
        }
        private bool ConvergenceCondition()
        {
            Console.WriteLine("Проверка условия сходимости: ");
            for (int i = 0; i < myMatrix.Length; i++)
            {
                Console.Write(myMatrix[i][i] + " > ");
                bool plus = false;
                double condition = myMatrix[i][i];
                for (int j = 0; j < myMatrix[0].Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    if (plus)
                    {
                        Console.Write(" + " + myMatrix[i][j]);
                    }
                    else
                    {
                        Console.Write(myMatrix[i][j]);
                    }
                    condition -= myMatrix[i][j];
                }
                Console.WriteLine();
                if (condition <= 0)
                {
                    Console.WriteLine("Условие сходимости не выполнено");
                    return false;
                }
            }
            Console.WriteLine("Условие сходимости выполнено");
            return true;
        }
        private void FindAccuracy()
        {
            double B = double.MinValue; 
            for (int i = 0; i < myMatrix.Length; i++)
            {
                double param = -1;
                for (int j = 0; j < myMatrix.Length; j++)
                {
                    param += Math.Abs(myMatrix[i][j] / myMatrix[i][i]);
                }
                if (param > B)
                {
                    B = param;
                }
            }
            accuracy = ((1 - B) * errorRate / B);
        }
        private void JacobiMethod()
        {
            double[] Terms = new double[vectorFreeTerms.Length];
            double iterationError;
            string penultimate;
            string last = string.Empty;
            do
            {
                for (int i = 0; i < Terms.Length; i++)
                {
                    double param = 0;
                    for (int j = 0; j < myMatrix.Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        param += myMatrix[i][j] * vectorX[j];
                    }
                    Terms[i] = Math.Round(-(param - vectorFreeTerms[i]) / myMatrix[i][i], numbers + 1);
                }
                iterationError = double.MinValue;
                for (int i = 0; i < Terms.Length; i++)
                {
                    double difference = Math.Abs(vectorX[i] - Terms[i]);
                    if (difference > iterationError)
                    {
                        iterationError = difference;
                    }
                }
                for (int i = 0; i < vectorX.Length; i++)
                {
                    vectorX[i] = Terms[i];
                }
                penultimate = last;
                last = Math.Round(iterationError, numbers + 1).ToString();
            }
            while (iterationError > accuracy);
            Console.WriteLine("Предпоследняя итерационная норма разности векторов: " + penultimate);
            Console.WriteLine("Последняя итерационная норма разности векторов: " + Math.Round(iterationError, numbers + 1));
            Console.WriteLine("Необходимая погрешность: " + accuracy);
            Console.WriteLine("Вектор решений: ");
            for (int i = 0; i < vectorX.Length; i++)
            {
                Console.WriteLine(Math.Round(vectorX[i], numbers+1));
            }
        }
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
                numbers -= 2; //разряд единиц и запятая
            }
        }
        public void Work()
        {
            if (ConvergenceCondition())
            {
                Console.WriteLine("Введите допустимую погрешность: ");
                errorRate = Convert.ToDouble(Console.ReadLine());
                FindAccuracy();
                FindNumbers();
                FindVectorX(FindDiagMatrix());
                JacobiMethod();
            }
        }
    }
}
