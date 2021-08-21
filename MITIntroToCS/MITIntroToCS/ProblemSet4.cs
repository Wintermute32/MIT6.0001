using System;
using System.Collections.Generic;
using System.Text;

// 
namespace MITIntroToCS
{
    class ProblemSet4
    {

        //Write a function, called which takes three arguments: a tuple
        //representing starting points for the first substring, a tuple representing starting points for the second
        //substring, and the length of the first substring.The function should return a tuple of all members(call
        //it n) of the first tuple for which there is an element in the second tuple(call it k) such that n+m+1 = k,
        //where m is the length of the first substring.

        float tempValue;
        public List<float> NestEggFixed(float salary, float percentageSave, float annualGrowth, int numOfYears)
        {
            List<float> acctValue = new List<float>();

            for (int i = 0; i < numOfYears; i++)
            {
                if (i == 0)
                {
                    tempValue = (float)(salary * percentageSave * .01);
                    acctValue.Add(tempValue);
                }

                else
                {
                    tempValue = (float)(acctValue[i - 1] * (1 + .01 * annualGrowth) + salary * percentageSave * 0.01);
                    acctValue.Add(tempValue);
                }
                
            }

            return acctValue;
        }


        //Write a function, called nestEggVariable, which takes three arguments: a salary
        //(salary), a percentage of your salary to save(save), and a list of annual growth
        //percentages on investments(growthRates). The length of the last argument defines the
        //number of years you plan to work; growthRates[0] is the growth rate of the first year,
        //growthRates[1] is the growth rate of the second year, etc. (Note that because the
        //retirement fund’s initial value is 0, growthRates[0] is, in fact, irrelevant.) This function
        //should return a list, whose values are the size of your retirement account at the end of
        //each year.

        public List<float> NestEggVariable(float salary, float percentageSave, List<int> annGrowthPer)
        {
            List<float> acctValue = new List<float>();

            for (int i = 0; i < annGrowthPer.Count; i++)
            {
                if (i == 0)
                {
                    tempValue = (float)(salary * percentageSave * .01);
                    acctValue.Add(tempValue);
                }

                else
                {
                    tempValue = (float)(acctValue[i - 1] * (1 + .01 * annGrowthPer[i]) + salary * percentageSave * 0.01);
                    acctValue.Add(tempValue);
                }

            }

            return acctValue;
        }
        //Write a function, called postRetirement, which takes three arguments: an initial amount
        //of money in your retirement fund(savings), a list of annual growth percentages on
        //investments while you are retired(growthRates), and your annual expenses(expenses).
        //Assume that the increase in the investment account savings is calculated before
        //subtracting the annual expenditures(as shown in the above table). Your function should
        //return a list of fund sizes after each year of retirement, accounting for annual expenses
        //and the growth of the retirement fund.Like problem 2, the length of the growthRates
        //argument defines the number of years you plan to be retired.
        //Note that if the retirement fund balance becomes negative, expenditures should continue
        //to be subtracted, and the growth rate comes to represent the interest rate on the debt
        //(i.e.the formulas in the above table still apply).
        public List<float> PostRetirement(float savings, float expenses, List<int> annGrowthPer)
        {

            List<float> acctValue = new List<float>();

            for (int i = 0; i < annGrowthPer.Count; i++)
            {
                if (i == 0)
                {
                    tempValue = (float)(savings + savings * annGrowthPer[0] * .01) - expenses;
                    acctValue.Add(tempValue);
                }


                else
                {
                    tempValue = (float)(acctValue[i - 1] + acctValue[i - 1] * annGrowthPer[i] * .01) - expenses;
                    acctValue.Add(tempValue);
                }
         
            }
            return acctValue;
        }


        //Write a function, called findMaxExpenses, which takes five arguments: a salary
        //(salary), a percentage of your salary to save(save), a list of annual growth percentages
        //on investments while you are still working(preRetireGrowthRates), a list of annual
        //growth percentages on investments while you are retired(postRetireGrowthRates), and
        //a value for epsilon(epsilon). As with problems 2 and 3, the lengths of
        //and postRetireGrowthRates determine the number of years you
        //plan to be working and retired, respectively.
        //Use the idea of binary search to find a value for the amount of expenses you can
        //withdraw each year from your retirement fund, such that at the end of your retirement,
        //the absolute value of the amount remaining in your retirement fund is less than epsilon
        //(note that you can overdraw by a small amount). Start with a range of possible values
        //for your annual expenses between 0 and your savings at the start of your retirement
        //(HINT #1: this can be determined by utilizing your solution to problem 2). Your function
        //should print out the current estimate for the amount of expenses on each iteration
        //through the binary search (HINT #2: your binary search should make use of your
        //solution to problem 3), and should return the estimate for the amount of expenses to
        //withdraw. (HINT #3: the answer should lie between zero and the initial value of the
        //savings + epsilon.) Complete the implementation of:


        public float FindMaxExpenses(float salary, float percentSaved, List<int> preRetGrowRates, List<int> postRetGrowRates, float epsilon)
        {
            //get the total amount in retirement acount, and save it as a variable in totalRetirementFund by using function 2 and saving the last value.

            List<float> possPreExpenseList = new List<float>(NestEggVariable(salary, percentSaved, preRetGrowRates));
            float savedForRetirement = possPreExpenseList[possPreExpenseList.Count - 1];
            float low = 0;
            float high = savedForRetirement;
            float expenseGuess = savedForRetirement / postRetGrowRates.Count; //this is our midpoint average, not taking into acount postRetirementGrowthRates

            float finAccBal = savedForRetirement; //final account balance variable
       

            while (Math.Abs(finAccBal) > epsilon)
            {
                Console.WriteLine("Trying with low bound of " + low);
                Console.WriteLine("Trying with high bound of " + high);
                Console.WriteLine("Trying with expenses guess of = " + expenseGuess);

                List<float> finalAcctBalanceList = new List<float>(PostRetirement(savedForRetirement, expenseGuess, postRetGrowRates));
                finAccBal = finalAcctBalanceList[finalAcctBalanceList.Count - 1];

                Console.WriteLine("Total Remaining Balance " + finAccBal);

                if (finAccBal < 0)
                {
                    high = expenseGuess;

                }

                else
                {
                    low = expenseGuess;
                }
                expenseGuess = (high + low) /2;
            }
            return expenseGuess;
        }

    }
}
