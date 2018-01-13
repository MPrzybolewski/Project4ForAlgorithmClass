using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Polynomial
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            double[,] pointsOfFirstDegreePolynomial = { { 1.0, 1.3 }, { 2.0, 3.5 }, { 3.0, 4.2 }, { 4.0, 5.0 }, { 5.0, 7.0 }, { 6.0, 8.8}, {7.0, 10.1}, { 8.0, 12.5}, {9.0, 13.0}, {10.0, 15.6} };
            double[,] pointsOfMoreThenOneDegreePolynomial = { {0.0, 1.0}, {0.25, 1.284} , {0.5, 1.6487}, {0.75, 2.117} , {1, 2.7183} };

            double[,] firstDegreePolynomialTable = FillTableForFirstDegreePolynomial(pointsOfFirstDegreePolynomial);
            double[] coefficientsOfFirstDegreePolynomial =
                CountCoefficientsOfFirstDegreePolynomial(firstDegreePolynomialTable);

            Console.WriteLine("F(x)  = {0}x  {1}", coefficientsOfFirstDegreePolynomial[1], coefficientsOfFirstDegreePolynomial[0]);

            double[,] PolynomialTable = FillTableForPolynomial(pointsOfMoreThenOneDegreePolynomial, 2);
            double[] coefficientsOfPolynomial = CountCoefficientsOfPolynomial(PolynomialTable, 2);


            Console.WriteLine("F(x) = x^2  x  ");


            Console.ReadKey();

        }

        public static double[,] FillTableForFirstDegreePolynomial(double[,] points)
        {
            double[,] firstDegreePolynomialTable = new double[points.GetLength(0), 4];

            for (int i = 0; i < points.GetLength(0); i++)
            {
                firstDegreePolynomialTable[i, 0] = points[i, 0];
                firstDegreePolynomialTable[i, 1] = points[i, 1];
                firstDegreePolynomialTable[i, 2] = Pow(points[i, 0], 2);
                firstDegreePolynomialTable[i, 3] = points[i, 0] * points[i, 1];
                Console.WriteLine("i = {4}   {0} , {1} , {2} , {3} ", firstDegreePolynomialTable[i, 0], firstDegreePolynomialTable[i, 1], firstDegreePolynomialTable[i, 2], firstDegreePolynomialTable[i, 3], i);
            }
            
            return firstDegreePolynomialTable;
        }

        public static double[] CountCoefficientsOfFirstDegreePolynomial(double[,] firstDegreePolynomialTable)
        {
            double[] coefficient = new double[2];
            int amountOfPoints = firstDegreePolynomialTable.GetLength(0);
            
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfPowX = 0;
            double sumOfXTimesY = 0;

            for (int i = 0; i < amountOfPoints; i++)
            {
                sumOfX += firstDegreePolynomialTable[i, 0];
                sumOfY += firstDegreePolynomialTable[i, 1];
                sumOfPowX += firstDegreePolynomialTable[i, 2];
                sumOfXTimesY += firstDegreePolynomialTable[i, 3];
            }

            coefficient[0] = ((sumOfPowX * sumOfY) - (sumOfX * sumOfXTimesY)) /
                             ((amountOfPoints * sumOfPowX) - Pow(sumOfX, 2));

            coefficient[1] = (( amountOfPoints * sumOfXTimesY) - (sumOfX * sumOfY)) /
                             ((amountOfPoints * sumOfPowX) - Pow(sumOfX, 2));


            return coefficient;
        }

        public static double[,] FillTableForPolynomial(double[,] points, int PolynomialDegree)
        {
            int amountOfColumns = 2 + (PolynomialDegree * 2) - 1 + PolynomialDegree;
            double[,] tableForPolynomial = new double[points.GetLength(0), amountOfColumns];


            for (int i = 0; i < points.GetLength(0); i++)
            {
                tableForPolynomial[i, 0] = points[i, 0];
                tableForPolynomial[i, 1] = points[i, 1];

                FillPowElementsInPolynomialTable(tableForPolynomial, PolynomialDegree, i);
                FillMultiplyElementsInPolynomialTable(tableForPolynomial, PolynomialDegree, i);
                
                Console.WriteLine("i = {0}   {1} , {2} , {3} , {4} , {5} , {6} , {7}",i,  tableForPolynomial[i, 0], tableForPolynomial[i, 1], tableForPolynomial[i, 2], tableForPolynomial[i, 3], tableForPolynomial[i, 4], tableForPolynomial[i, 5], tableForPolynomial[i, 6]);
            }

            return tableForPolynomial;
        }


        public static void FillPowElementsInPolynomialTable(double[,] tableForPolynomial, int polynomialDegree, int rowNumber)
        {
            int columnFromWichWeStart = 2;
            double functionArgument = tableForPolynomial[rowNumber, 0];

            for (int i = 0; i < polynomialDegree * 2 - 1; i++)
            {
                tableForPolynomial[rowNumber, columnFromWichWeStart + i] = Pow(functionArgument, i + 2);
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

        public static double[] CountCoefficientsOfPolynomial(double[,] tableForPolynomialOperations, int polynomialDegree)
        {
            double[] coefficient = new double[polynomialDegree + 1];
            int firstIndexOfMultiplyElements = 2 + (2 * polynomialDegree) - 1;


            int amountOfPoints = tableForPolynomialOperations.GetLength(0);

            double[,] matrixForCountCoefficient = new double[polynomialDegree + 1, polynomialDegree + 1];

            double[] sTable = new double[2 * polynomialDegree + 1];
            double[] tTable = new double[polynomialDegree + 1];

            sTable[0] = amountOfPoints;
            for (int i = 0; i < amountOfPoints; i++)
            {
                int k = 0;
                for (int j = 2; j < tableForPolynomialOperations.GetLength(1); j++)
                {
                    
                    if(j < firstIndexOfMultiplyElements)
                    {
                        sTable[j] += tableForPolynomialOperations[i, j];    
                    }
                    else
                    {
                        k++;
                        tTable[k] += tableForPolynomialOperations[i, j];
                    }

                }

                sTable[1] += tableForPolynomialOperations[i, 0];
                tTable[0] += tableForPolynomialOperations[i, 1];
            }

            Console.WriteLine("S");
            for (int i = 0; i < sTable.Length; i++)
            {
                Console.WriteLine("{0} : {1}", i+1, sTable[i]);
            }

            Console.WriteLine("T");
            for (int i = 0; i < tTable.Length; i++)
            {
                Console.WriteLine("{0} : {1}", i + 1, tTable[i]);
            }

            for (int i = 0; i < polynomialDegree + 1; i++)
            {
                int firstSTableElementInRowIndex = i;

                for (int j = 0; j < polynomialDegree + 1; j++)
                {
                    
                    matrixForCountCoefficient[i, j] = sTable[i + j];
                    firstSTableElementInRowIndex++;
                }
            }
                
            return coefficient;

        }

        public static double Pow(double baseNumber, int exponentNumber)
        {
            double result = baseNumber;
            if (exponentNumber != 0)
            {
                for (int i = 1; i < exponentNumber; i++)
                {
                    result = result * baseNumber;
                }

                return result;
            }

            return 1;
        }

    }
}
