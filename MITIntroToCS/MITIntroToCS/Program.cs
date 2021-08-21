using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace MITIntroToCS
{

	class Program
	{

		static void Main(string[] args)
		{
			ProblemSet3_5 pSet35 = new ProblemSet3_5();
			var wordList = pSet35.LoadWords();

			//Dictionary<char, int> input = new Dictionary<char, int> { { 'a', 1 }, { 'z', 2 }, { '*', 1 }, { 'l', 1 }, { 'u', 1 } };

			int finalScore = pSet35.PlayGame(wordList);
				
			Console.WriteLine("Games final score is : " + finalScore);
		}
	}
}


	
 