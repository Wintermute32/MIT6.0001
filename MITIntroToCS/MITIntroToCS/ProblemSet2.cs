using System;
using System.Collections.Generic;
using System.Text;




namespace MITIntroToCS
{
	class ProblemSet2
	//Show that it is possible to buy exactly 50, 51, 52, 53, 54, and 55 McNuggets, by finding
	//solutions to the Diophantine equation.You can solve this in your head, using paper and pencil,
	//or writing a program.However you chose to solve this problem, list the combinations of 6, 9
	//and 20 packs of McNuggets you need to buy in order to get each of the exact amounts.Given that it is possible to buy sets of 50, 51, 52, 53, 54 or 55 McNuggets by combinations of 6,
	//9 and 20 packs, show that it is possible to buy 56, 57,…, 65 McNuggets.In other words, show
	//how, given solutions for 50-55, one can derive solutions for 56-65.

	{
		public void Dictaphone(int nugCountInQuestion)
		{
			//A standard bare bones number chugger to get an output Put in total nug count, and get whether it's divisible.

			for (int a = 0; a <= (nugCountInQuestion / 6) + 1; a++)
			{
				for (int b = 0; b <= (nugCountInQuestion / 9) + 1; b++)
					for (int c = 0; c <= (nugCountInQuestion / 20) + 1; c++)
					{
						if (nugCountInQuestion == 6 * a + 9 * b + 20 * c)
						{
							Console.WriteLine("Combo Found at {0}a {1}b and {2}c for nugget value {3}", a, b, c, nugCountInQuestion);
							return;

						}

					}
			}

			Console.WriteLine("No combo found for nugget value of " + nugCountInQuestion);
		}


		//Write an iterative program that finds the largest number of McNuggets that cannot be bought in
		//exact quantity.Your program should print the answer in the following format (where the correct
		//number is provided in place of <n>):

		public void Diophantine3() //this was a bitch
		{
	
			int n = 0;
			List<int> invalidCombos = new List<int>();
			int nugCountInQuestion = 0;
			int largestNugCount = 0;
			bool isInBounds = true;

			while (isInBounds)
            {

				for (int a = 0; a <= (nugCountInQuestion / 6) + 1; a++)
				{
					for (int b = 0; b <= (nugCountInQuestion / 9) + 1; b++)
						for (int c = 0; c <= (nugCountInQuestion / 20) + 1; c++)
						{
							if (nugCountInQuestion == 6 * a + 9 * b + 20 * c)
							{
								Console.WriteLine("Combo Found at {0}a {1}b and {2}c for nugget value {3}", a, b, c, nugCountInQuestion);
								nugCountInQuestion++;
								a = 0;
								b = 0;
								c = 0;
								n++;
							}

							if (n > 5)
                            {
								Console.WriteLine("largest indivisible number is: " + largestNugCount);
								return;
							}
						}
				}
				Console.WriteLine("No combo found for nugget value of " + nugCountInQuestion);
				largestNugCount = nugCountInQuestion;
				nugCountInQuestion++;
				n = 0;

			}
		}

		//Assume that the variable packages is bound to a tuple of length 3, the values of which specify
		//the sizes of the packages, ordered from smallest to largest.Write a program that uses
		//exhaustive search to find the largest number(less than 200) of McNuggets that cannot be
		//bought in exact quantity.We limit the number to be less than 200 (although this is an arbitrary
		//choice) because in some cases there is no largest value that cannot be bought in exact quantity,
		//and we don’t want to search forever.Please use ps2b_template.py to structure your code.
		//Have your code print out its result in the following format


		public void Diophantine4(int nugCountLimit, int[] nugSizeValues)
		{
			//given a total count limit to keep from running into eternity, and a custom set of nugget sizes
			//find the largest value of nuggets that can't be made up of the given package sizes
			int nugCountInQuestion = 0;
			int largestNugCount = 0;

			while (true)
			{

				for (int a = 0; a <= (nugCountInQuestion / nugSizeValues[0]) + 1; a++)
				{
					for (int b = 0; b <= (nugCountInQuestion / nugSizeValues[1]) + 1; b++)
						for (int c = 0; c <= (nugCountInQuestion / nugSizeValues[2]) + 1; c++)
						{
							if (nugCountInQuestion == nugSizeValues[0] * a + nugSizeValues[1] * b + nugSizeValues[2] * c)
							{
								Console.WriteLine("Combo Found at {0}a {1}b and {2}c for nugget value {3}", a, b, c, nugCountInQuestion);
								nugCountInQuestion++;
								a = 0;
								b = 0;
								c = 0;
							}

							if (nugCountInQuestion > nugCountLimit) //in this method we are limited by our total nugget size, instead of consecutive divisible nugget sizes, since we are accepting variable nug package size values
							{
								Console.WriteLine("Given pacage size {0}, {1}, and {2} the largest package size that cannot be bought in exact quantity is {3}", nugSizeValues[0], nugSizeValues[1], nugSizeValues[2], largestNugCount);
								return;
							}
						}
				}
				Console.WriteLine("No combo found for nugget value of " + nugCountInQuestion);
				largestNugCount = nugCountInQuestion;
				nugCountInQuestion++;

			}

		}

	}
}
