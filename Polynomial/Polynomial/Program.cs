using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Polynomial
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            double[,] pointsOfFirstDegreePolymonial = { { 1.0, 1.3 }, { 2.0, 3.5 }, { 3.0, 4.2 }, { 4.0, 5.0 }, { 5.0, 7.0 }, { 6.0, 8.8}, {7.0, 10.1}, { 8.0, 12.5}, {9.0, 13.0}, {10.0, 15.6} };
            double[,] pointsOfMoreThenOneDegreePolymonial = { {0.0, 1.0}, {0.25, 1.284} , {0.5, 1.6487}, {0.75, 2.117} , {1, 2.7183} };

            double[,] firstDegreePolymonialTable = FillTableForFirstDegreePolynomial(pointsOfFirstDegreePolymonial);
            double[] coefficientsOfFirstDegreePolymonial =
                CountCoefficientsOfFirstDegreePolymonial(firstDegreePolymonialTable);

            Console.WriteLine("F(x)  = {0}x  {1}", coefficientsOfFirstDegreePolymonial[1], coefficientsOfFirstDegreePolymonial[0]);

            double[,] polymonialTable = FillTableForPolynomial(pointsOfMoreThenOneDegreePolymonial, 2);

            Console.WriteLine("F(x) = x^2  x  ");


            Console.ReadKey();

        }

        public static double[,] FillTableForFirstDegreePolynomial(double[,] points)
        {
            double[,] firstDegreePolymonialTable = new double[points.GetLength(0), 4];

            for (int i = 0; i < points.GetLength(0); i++)
            {
                firstDegreePolymonialTable[i, 0] = points[i, 0];
                firstDegreePolymonialTable[i, 1] = points[i, 1];
                firstDegreePolymonialTable[i, 2] = Pow(points[i, 0], 2);
                firstDegreePolymonialTable[i, 3] = points[i, 0] * points[i, 1];
                Console.WriteLine("i = {4}   {0} , {1} , {2} , {3} ", firstDegreePolymonialTable[i, 0], firstDegreePolymonialTable[i, 1], firstDegreePolymonialTable[i, 2], firstDegreePolymonialTable[i, 3], i);
            }
            
            return firstDegreePolymonialTable;
        }

        public static double[] CountCoefficientsOfFirstDegreePolymonial(double[,] firstDegreePolymonialTable)
        {
            double[] coefficient = new double[2];
            int amountOfPoints = firstDegreePolymonialTable.GetLength(0);
            
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfPowX = 0;
            double sumOfXTimesY = 0;

            for (int i = 0; i < amountOfPoints; i++)
            {
                sumOfX += firstDegreePolymonialTable[i, 0];
                sumOfY += firstDegreePolymonialTable[i, 1];
                sumOfPowX += firstDegreePolymonialTable[i, 2];
                sumOfXTimesY += firstDegreePolymonialTable[i, 3];
            }

            coefficient[0] = ((sumOfPowX * sumOfY) - (sumOfX * sumOfXTimesY)) /
                             ((amountOfPoints * sumOfPowX) - Pow(sumOfX, 2));

            coefficient[1] = (( amountOfPoints * sumOfXTimesY) - (sumOfX * sumOfY)) /
                             ((amountOfPoints * sumOfPowX) - Pow(sumOfX, 2));


            return coefficient;
        }

        public static double[,] FillTableForPolynomial(double[,] points, int polymonialDegree)
        {
            int amountOfColumns = 2 + (polymonialDegree * 2) - 1 + polymonialDegree;
            double[,] tableForPolymonial = new double[points.GetLength(0), amountOfColumns];


            for (int i = 0; i < points.GetLength(0); i++)
            {
                tableForPolymonial[i, 0] = points[i, 0];
                tableForPolymonial[i, 1] = points[i, 1];

                FillPowElementsInPolynomialTable(tableForPolymonial, polymonialDegree, i);
                FillMultiplyElementsInPolynomialTable(tableForPolymonial, polymonialDegree, i);
                
                Console.WriteLine("i = {0}   {1} , {2} , {3} , {4} , {5} , {6} , {7}",i,  tableForPolymonial[i, 0], tableForPolymonial[i, 1], tableForPolymonial[i, 2], tableForPolymonial[i, 3], tableForPolymonial[i, 4], tableForPolymonial[i, 5], tableForPolymonial[i, 6]);
            }

            return tableForPolymonial;
        }


        public static void FillPowElementsInPolynomialTable(double[,] tableForPolymonial, int polynomialDegree, int rowNumber)
        {
            int columnFromWichWeStart = 2;
            double functionArgument = tableForPolymonial[rowNumber, 0];

            for (int i = 0; i < polynomialDegree * 2 - 1; i++)
            {
                tableForPolymonial[rowNumber, columnFromWichWeStart + i] = Pow(functionArgument, i + 2);
            }
        }

        public static void FillMultiplyElementsInPolynomialTable(double[,] tableForPolynomial, int polynomialDegree, int rowNumber)
        {
            int columnFromWichWeStart = 2 + (polynomialDegree * 2) - 1;
            double functionValue = tableForPolynomial[rowNumber, 1];
            double functionArgument = tableForPolynomial[rowNumber, 0];

            for (int i = 0; i < polynomialDegree; i++)
            {
                tableForPolynomial[rowNumber, columnFromWichWeStart + i] =
                    functionValue * Pow(functionArgument, i + 1);
            }
        }


        public static double Pow(double baseNumber, int exponentNumber)
        {
            if (exponentNumber != 0)
            {
                for (int i = 0; i < exponentNumber - 1; i++)
                {
                    baseNumber = baseNumber * baseNumber;
                }

                return baseNumber;
            }

            return 1;
        }

    }
}
