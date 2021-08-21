using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MITIntroToCS
{
    //2016 MIT Intro to CS Problem Set 3
    class ProblemSet3_5
    {

        //The first step is to implement a function that calculates the score for a single word.Fill in
        //the code for ​get_word_score​ in ​ps3.py​ according to the function specifications.
        //As a reminder, here are the rules for scoring a word:
        //● The score for a word is the ​product​ of two components:
        //o First component: the sum of the points for letters in the word.
        //o Second component: either[7 * ​word_length - 3 * (​n -​word_length)] or 1,
        //whichever value is greater, where:
        //▪ word_length is the number of letters used in the word
        //▪ n is the number of letters available in the current hand
        //You should use the ​SCRABBLE_LETTER_VALUES​ dictionary defined at the top of ​ps3.py​.Do
        //not​ assume that there are always 7 letters in a hand! The parameter ​n is the total number
        //of letters in the hand when the word was entered.



        string consonants = "bcdfghjklmnpqrstvwxyz";
        string vowels = "aeiou";
        char wildCard = '*';
        int handSize = 7;
        Dictionary<char, int> scrabbleLetterValues = new Dictionary<char, int>() {{'a', 1}, {'b',3 }, {'c',3 }, {'d',2 }, {'e', 1 }, {'f', 4 }, {'g', 2 }, {'h', 4 }, {'i', 1 }, {'j', 8 },
            { 'k', 5 }, {'l', 1 }, {'m', 3 }, {'n', 1 }, {'o', 1 }, {'p', 3 }, {'q', 10 }, {'r', 1 }, {'s', 1 }, {'t', 1 }, {'u', 1 }, {'v', 4 }, {'w', 4 }, {'x', 8 }, {'y', 4 }, {'z', 10}, {'*', 0} };

        public List<string> LoadWords()
        {
            Console.WriteLine("loading word list from file...");
            List<string> inFile = new List<string>(File.ReadLines(@"C:\Users\pdnud\OneDrive\Desktop\MIT Intro Course\6-0001-fall-2016\contents\assignments\PS3\words.txt"));
            List<string> wordList = new List<string>();

            foreach (var x in inFile)
                wordList.Add(x.Trim().ToLower());

            //Console.WriteLine(" " + wordList.Count + " words loaded");

            return wordList;

        }

        public Dictionary<char, int> GetFrequencyDict(string sequence)
        {
            var lowerSequence = sequence.ToLower();
            //return a dictionary that counts the instances of the same character in a word.
            Dictionary<char, int> freq = new Dictionary<char, int>();

            foreach (var x in lowerSequence)
            {
                if (freq.ContainsKey(x))
                    freq[x] = freq[x] + 1;
                else
                    freq.Add(x, 1);
            }

            //foreach (KeyValuePair<char, int> kvp in freq)
                //Console.WriteLine(kvp);


            return freq;
        }

        public int GetWordScore(string inputWord, int n)
        {
            var word = inputWord.ToLower();
            var letterFrequency = GetFrequencyDict(word);
            int letterSum = 0;
            var wordScore = 0;

            foreach (var letter in letterFrequency)
            {
                letterSum += letter.Value * scrabbleLetterValues[letter.Key];
            }

            var comboTotal = 7 * word.Length - 3 * (n - word.Length);

            if (comboTotal > 1)
                wordScore = letterSum * comboTotal;
            else
                wordScore = letterSum * 1;

            //Console.WriteLine("wordScore for {0} is {1}", inputWord, wordScore);

            return wordScore;
        }

        public void DisplayHand(Dictionary<char, int> hand)
        {

            string output = "";
            foreach (KeyValuePair<char, int> kvp in hand)
            {
                output += new String(kvp.Key, kvp.Value);
                kvp.Key = 'a';
            }    


            Console.WriteLine("Current Hand: " + output);
        }


        //Modify the ​deal_hand​ function to support always giving one wildcard in each hand.Note
        //that ​deal_hand​ currently ensures that one third of the letters are vowels and the rest are
        //consonants. Leave the consonant count intact, and replace one of the vowel slots with the
        //wildcard. You will also need to modify one or more of the constants defined at the top of the
        //file to account for wildcards.

        public Dictionary<char, int> DealHand(int n)
        {
            Dictionary<char, int> hand = new Dictionary<char, int>();
            Random rand = new Random();
            var numVowels = (int)Math.Ceiling((double)(n / 3));

            for (int i = 0; i < numVowels; i++)
            {
                var x = vowels[rand.Next(0, vowels.Length)];

                if (!hand.ContainsKey(wildCard))
                    hand.Add(wildCard, 1);

                else if (hand.ContainsKey(x))
                    hand[x] = hand[x] + 1;
                else
                    hand.Add(x, 1);
            }

            for (int i = numVowels; i < n; i++)
            {
                var x = consonants[rand.Next(0, consonants.Length)];
                if (hand.ContainsKey(x))
                    hand[x] = hand[x] + 1;
                else
                    hand.Add(x, 1);
            }

            return hand;
        }


        //The function takes as input a positive
        //integer ​n, and returns a new dictionary representing a hand of ​n lowercase letters.Again,
        //take a few minutes to read through this function carefully and understand what it does and
        //how it works.
        //Removing letters from a hand (you implement this!)
        //The player starts with a full hand of ​n letters. As the player spells out words, letters from
        //the set are used up. For example, the player could start with the following hand: ​a, q, l, m,
        //u, i, l ​The player could choose to play the word ​quail.​ This would leave the following letters
        //in the player's hand: ​l, m.
        //You will now write a function that takes a hand and a word as inputs, uses letters from that
        //hand to spell the word, and returns a ​new​ hand containing only the remaining letters. Your
        //function should ​not​ modify the input hand.
        public Dictionary<char, int> UpdateHand(Dictionary<char, int> hand, string word)
        {

            Dictionary<char, int> updatedHand = new Dictionary<char, int>(hand);

            foreach (var x in word)
                if (updatedHand.ContainsKey(x) && updatedHand[x] > 0)
                    updatedHand[x] += -1;

            foreach (var kvp in updatedHand)
                if (kvp.Value <= 0)
                    updatedHand.Remove(kvp.Key);

            return updatedHand;
        }

        //At this point, we have not written any code to verify that a word given by a player obeys the
        //rules of the game.A ​valid word is in the word list (we ignore the case of words here) ​and​ it
        //is composed entirely of letters from the current hand.
        //Implement the ​is_valid_word​ function according to its specifications

        public bool IsValidWord(string word, Dictionary<char, int> hand, List<string> wordList)
        {
            string lowerWord = word.ToLower();

            if (!lowerWord.Contains(wildCard))
            {
                foreach (var x in lowerWord)
                {
                    if (!hand.ContainsKey(x))
                        return false;
                    else
                    {
                        hand[x] += -1;
                        if (hand[x] < 0)
                            return false;
                    }

                }
                return (BinarySearch(word, wordList));
            }

            else
            {
                int wildCardIndex = word.IndexOf(wildCard);
                StringBuilder sb = new StringBuilder(word);

                foreach (var z in vowels)
                {
                    sb[wildCardIndex] = z;
                    if (BinarySearch(sb.ToString(), wordList))
                        return true;
                }

                return false;
            }
        }

        public bool BinarySearch(string word, List<string> wordList)
        {
            string searchWord = word.ToLower();
            int low = 0;
            int high = wordList.Count - 1;


            while (high - low >= 0)
            {
                int guess = (high + low) / 2;

                if (searchWord == wordList[guess])
                    return true;

                if (String.Compare(searchWord, wordList[guess], true) > 0)
                    low = guess + 1;

                if (String.Compare(searchWord, wordList[guess], true) < 0)
                    high = guess - 1;


            }

            return false;
        }

        public int CalculateHandLength(Dictionary<char, int> hand)
        {
            return hand.Count;
        }

        //Implement the ​play_hand​ function.This function allows the user to play out a single hand.
        //You'll first need to implement the helper function ​calculate_handlen​, which can be done in
        //under five lines of code.
        //To end the hand early, the player ​must​ type "​!!​" (two exclamation points).


        public int PlayHand(Dictionary<char, int> hand, List<string> wordList)
        {
            int totalScore = 0;

            while (hand.Count > 0)
            {
                DisplayHand(hand);
                Console.WriteLine("Enter word, or !! to indicate that you are finished: ");
                var word = Console.ReadLine().ToLower();

                if (word == "!!")
                {
                    Console.WriteLine("Score for this hand: " + totalScore);
                    return totalScore;
                }


                if (IsValidWord(word, hand, wordList))
                {
                    hand = UpdateHand(hand, word);
                    int wordScore = GetWordScore(word, hand.Count);
                    totalScore += wordScore;
                    Console.WriteLine("\"{0}\" earned {1} points. Total: {2} points", word, wordScore, totalScore);
                }
                else
                {
                    hand = UpdateHand(hand, word);
                    Console.WriteLine("This is not a valid word. Please choose another word.");
                }
            }

            Console.WriteLine("Ran out of letters. Total Score: " + totalScore);
            return totalScore;
        }

        public Dictionary<char, int> SubstituteHand(Dictionary<char, int> hand, char letter)
        {
            Random rand = new Random();

            if (!hand.ContainsKey(letter))
            {
                Console.WriteLine("You don't have that letter");
                return hand;
            }

            string availableLetters = consonants + vowels;

            for (int i = 0; i < hand[letter]; i++)
            {
                var x = availableLetters[rand.Next(scrabbleLetterValues.Count)];

                while (hand.ContainsKey(x))
                    x = availableLetters[rand.Next(scrabbleLetterValues.Count)];

                //Console.WriteLine("Replacing {0}, with {1} ", letter, x );

                if (hand.ContainsKey(x))
                    hand[x] = hand[x] + 1;
                else
                    hand.Add(x, 1);
            }

            hand.Remove(letter);
            return hand;
        }
        //A game consists of playing multiple hands.We need to implement two final functions to
        //complete our wordgame.
        //Implement the ​substitute_hand ​and ​play_game​ functions according to their specifications.
        //For the game, you should use the ​HAND_SIZE​ constant to determine the number of letters in
        //a hand.
        //8Do ​not​ assume that there will always be 7 letters in a hand! Our goal is to keep the code
        //modular - if you want to try playing your word game with 10 letters or 4 letters you will be
        //able to do it by simply changing the value of ​HAND_SIZE​!

        public int PlayGame(List<string> wordList)
        {
            bool subUsed = false;
            bool replayHand = false;
            int totalScore = 0;
            
            Console.WriteLine("How many hands would you like to play?");
            var numHands = Convert.ToInt32(Console.ReadLine());

            while (numHands.GetType().ToString() != "System.Int32")
            {
                Console.WriteLine("Use an integer, 1, 2, 3, etc");
                numHands = Convert.ToInt32(Console.ReadLine());
            }
                
            for (int i = 0; i < numHands; i++)
            {
                var thisHand = DealHand(handSize);
                //DisplayHand(thisHand);

                if (subUsed == false)
                {
                    DisplayHand(thisHand);
                    Console.WriteLine("Would you like to substitute a letter? Yes or No: ");
                    string confirmSub = Console.ReadLine().ToLower();

                    while (confirmSub != "yes" && confirmSub != "no")
                    {
                        Console.WriteLine("Type either yes or no");
                        confirmSub = Console.ReadLine();
                    }

                    if (confirmSub == "yes")
                    {
                        subUsed = true;
                        Console.WriteLine("Type in the letter to substitue: ");
                        char subLetter = Char.ToLower(Console.ReadKey().KeyChar);
                        thisHand = SubstituteHand(thisHand, subLetter);
                    }
                }

               var handScore = PlayHand(thisHand, wordList);

                if (replayHand == false)
                {
                    Console.WriteLine("Would you like to replay this hand? Only once per game: ");
                    string replayAnswer = Console.ReadLine().ToLower();

                    while (replayAnswer != "yes" && replayAnswer != "no" )
                    {
                        Console.WriteLine("You must type yes or no");
                        replayAnswer = Console.ReadLine().ToLower();
                    }    
                      

                    if (replayAnswer == "yes")
                    {
                        handScore = PlayHand(thisHand, wordList);
                        replayHand = true;
                    }

                }

                totalScore += handScore;
            }

            return totalScore;
        }
    }
}
