using System;
using System.Collections.Generic;

namespace MITIntroToCS
{
    class ProblemSet1
    {
        //Write a program that computes and prints the 1000th prime number.

        //Write a program that computes the sum of the logarithms of all the primes from 2 to some
        //number n, and print out the sum of the logs of the primes, the number n, and the ratio of these
        //two quantities.Test this for different values of n.



        public static int PrimeCompute(int primeNumN)
        {
            List<int> primeList = new List<int>();
            int numberInQuestion = 3; //accounting for the number 2 as a prime.

            primeList.Add(2);
            int primeCounterTotal = 1;

            while (primeCounterTotal < primeNumN)
            {

                if (numberInQuestion % 2 != 0)
                {
                    if (isPrime(numberInQuestion))
                    {
                        Console.WriteLine("Adding Prime Number," + numberInQuestion);
                        primeList.Add(numberInQuestion);
                        numberInQuestion++;
                        primeCounterTotal++;
                    }
                }

                numberInQuestion++;
            }
            return primeList[primeNumN - 1];
        }

        public static bool isPrime(int oddNum)
        {
            for (int i = 2; i <= oddNum; i++)
            {
                if (oddNum % i == 0)
                {
                    if (oddNum == i)
                        return true;
                    else
                        return false;
                }
                continue;
            }

            return false;
        }
    }
    class ProblemSet1A
    {

        public void SumOfLogs(int numNPrime)
        {
           double sumOfPrimes = 0;
           var primeList = PrimeCompute2(numNPrime);

            foreach (var x in primeList)
                sumOfPrimes += LogOfPrime(x);

            Console.WriteLine("The log sum of all primes up to and including {0} is {1}", numNPrime, sumOfPrimes);
        }
        
        public double LogOfPrime(int primeNum)
        {
        return Math.Log(primeNum);
        }


        public static List<int> PrimeCompute2(int primeNumN)
        {
            List<int> primeList = new List<int>();
            int numberInQuestion = 3; //accounting for the number 2 as a prime.

            primeList.Add(2);
            int primeCounterTotal = 1;

            while (primeCounterTotal < primeNumN)
            {

                if (numberInQuestion % 2 != 0)
                {
                    if (isPrime2(numberInQuestion))
                    {
                        Console.WriteLine("Adding Prime Number," + numberInQuestion);
                        primeList.Add(numberInQuestion);
                        numberInQuestion++;
                        primeCounterTotal++;
                    }
                }

                numberInQuestion++;
            }
            return primeList;
        }

        public static bool isPrime2(int oddNum)
        {
            for (int i = 2; i <= oddNum; i++)
            {
                if (oddNum % i == 0)
                {
                    if (oddNum == i)
                        return true;
                    else
                        return false;
                }
                continue;
            }

            return false;
        }

    }
}
    