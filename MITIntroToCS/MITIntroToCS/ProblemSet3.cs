using System;
using System.Collections.Generic;
using System.Text;

namespace MITIntroToCS
{
    class ProblemSet3
    {

        //Write two functions, called countSubStringMatch and countSubStringMatchRecursive that
        //take two arguments, a key string and a target string. These functions iteratively and recursively count
        //the number of instances of the key in the target string. You should complete definitions for
        //def countSubStringMatch(target, key):
        //and def countSubStringMatchRecursive(target, key) :
        public int CountSubStringMatch(string target, string key)
        {
            int startIndex = 0;
            int counter = 0;
            while (true)
            {
                if (target.IndexOf(key, startIndex) >= startIndex)
                {
                    counter++;
                    startIndex = target.IndexOf(key, startIndex) + 1; 
                }

                else
                    break;
            }
            return counter;
        }

        public int CountSubStringMatchRecursive(string target, string key)
        {
            int counter = 0;
            int startIndex = 0;

            if (!target.Contains(key)) //base case
            {
                  return 0;
            }

            if (target.Contains(key))
            {
                //We had prob w/ out of range exception. We were using old values of startInex that was > than total substring length.
                counter++;
                startIndex = target.IndexOf(key) + 1; 
                target = target.Substring(startIndex);
            }
            return (counter + CountSubStringMatchRecursive(target, key));
        }

        //Write the function subStringMatchExact.This function takes two arguments: a target string,
        //and a key string. It should return a tuple of the starting points of matches of the key string in the target
        //string, when indexing starts at 0. Complete the definition for
        //def subStringMatchExact(target, key):


        public List<int> SubStringMatchExact(string target, string key)
        {
            List<int> indexList = new List<int>();
            int startIndex = 0;

            if (key == "")
            {
                int counter = 0; //this was a possbile bug but lets check. Why was this -1?
                foreach (var x in target)
                {
                    counter++;
                    indexList.Add(counter);
                }
                return indexList;   
            }

            while (true)
            {
                if (target.IndexOf(key, startIndex) >= startIndex)
                {
                    indexList.Add(target.IndexOf(key, startIndex));
                    startIndex = target.IndexOf(key, startIndex) + 1;
                }

                else
                    break;    
                   
            }

            return indexList;
        }

            // Write a function, called which takes three arguments: a tuple
            //representing starting points for the first substring, a tuple representing starting points for the second
            //substring, and the length of the first substring.The function should return a tuple of all members(call
            //it n) of the first tuple for which there is an element in the second tuple(call it k) such that n+m+1 = k,
            //where m is the length of the first substring.
        public List<int> ConstrainedMatchedPair(List<int> sub1Indexers, List<int> sub2Indexers, int sub1Length)
        {            
            List<int> finalOutput = new List<int>();

            foreach (var x in sub1Indexers)
                foreach (var y in sub2Indexers)
                    if (y == x + sub1Length + 1)
                        finalOutput.Add(x);

            return finalOutput;
        }

        public List<int> SubStringMatchOneSub(string target, string key)
        {
            List<int> allAnswers = new List<int>();

            for (int i = 0; i<key.Length; i++)
            {
                var key1 = key.Substring(0, i);
                var key2 = key.Substring(i + 1);


                Console.WriteLine("breaking key: " + key + " into " + key1 + key2);
                var match1 = SubStringMatchExact(target, key1);
                var match2 = SubStringMatchExact(target, key2);

                List<int> filtered = ConstrainedMatchedPair(match1, match2, key1.Length);

                foreach (var x in filtered)
                    allAnswers.Add(x);

                Console.WriteLine("match 1: " + match1);
                Console.WriteLine("match 2: " + match2);
                Console.WriteLine("possible matches for " + key1 + key2 + " start at " + filtered);
            }
            return allAnswers;
        }

        public List<int> SubStringMatchExactlyOneSub(string target, string key)
        {

            List<int> exactList = SubStringMatchExact(target, key);
            List<int> oneSubList = SubStringMatchOneSub(target, key);
            List<int> output = new List<int>();


            foreach (var y in oneSubList)
                if (!exactList.Contains(y))
                    output.Add(y);
                        
            return output;
        }

        static void RunTestPierce()
        {
            //Custom created to generate inputs to test Constrainted Match Pair function. Not part of homework.
            ProblemSet3 probSet3 = new ProblemSet3();

            var subString1 = probSet3.SubStringMatchExact("atgcat", "atgca");
            var subString2 = probSet3.SubStringMatchExact("atgcat", "");


            foreach (var x in subString1)
                Console.WriteLine("substring 1 values:" + x);

            foreach (var x in subString2)
                Console.WriteLine("substring 2 values:" + x);

            var whatever = probSet3.ConstrainedMatchedPair(subString1, subString2, 5);

            if (whatever.Count == 0)
                Console.WriteLine("Aint no values here!");
            else
            {
                foreach (var x in whatever)
                    Console.WriteLine("Index pos of possible combo is: " + x);
            }
        }


    }
}
