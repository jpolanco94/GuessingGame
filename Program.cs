using System;
using System.Linq;
using System.Threading;

namespace BisectionalGuessGame
{
    class Program
    {
        public static int UserGuess()
        {
            try { return int.Parse(Console.ReadLine()); }
            catch (NotACorrectNumber) { return 0; }
        }
        //Find the median of the array
        public static int FindMedianOfArray(int[] arr)
        {
            int medianOfArray = 0;
            medianOfArray = arr[((int)arr.Length/2)];
            return medianOfArray;
        }
        //Method that makes an array.
        public static int[] MakeAnArray(int lowerLimit, int upperLimit)
        {
            int[] arr = new int[upperLimit + 1 - lowerLimit];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = lowerLimit + i;
            }
            return arr;
        }
        //This method splits the array into two. If splitting the array results in the correct number being removed, this method will return the original array.
        public static int[] SplitArrayUpper(int[]arr, int actualNumber)
        {
            bool actualNumberIsPartOf = false;
            int[] arr1 = arr.Skip(arr.Length / 2).ToArray();
            for (int i = 0; i <arr1.Length; i++)
            {
                if (arr1[i] == actualNumber) actualNumberIsPartOf = true;
            }
            return (actualNumberIsPartOf == true) ? arr1 : arr;
        }
        //This method splits the array into two. If splitting the array results in the correct number being removed, this method will return the original array.
        public static int[] SplitArrayLower(int[] arr, int actualNumber)
        {
            bool actualNumberIsPartOf = false;
            int[] arr1 = arr.Take(arr.Length / 2).ToArray();
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == actualNumber) actualNumberIsPartOf = true;
            }
            return (actualNumberIsPartOf == true) ? arr1 : arr;
        }
        public static void GuessNumbersComputer()
        {
            Console.WriteLine("I will let the computer play this game first just to display how this program works\n" +
                "The array starts from 1 to 1000. A random number will be generated as the correct number and the computer\n" +
                "As the computer makes its guesses the array will elimnate a bisection of the array. You will see that\n" +
                "the computer's guesses continue to get closer and closer to the actual number instead of making wild guesses.\n" +
                "I will display the array and the computers new guess just so you can see how the array gets smaller.\n" +
                "Press enter to continue.");
            Console.ReadKey();
            Random rng = new Random();
            //Working on this code I've figured out something interesting about the Random.Next Method. It seems that the minValue and the maxValue you enter
            //never get picked. This is something I didn't know and I cannot find any supporting evidence but am pretty certain of. I figured this out after 
            //having my computer guess a number between 4 elements of an array. It only ever chose the two numbers between the upper and lower bounds of that array
            //because those were the paramters I passed to the Random.Next method.
            int[] arr = MakeAnArray(1, 1000);
            int actualNumber = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
            int guess = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
            Console.WriteLine($"{actualNumber} is the correct number and {guess} is the computer's initial guess\n");
            Thread.Sleep(1000);
            //This count is used to calculate how many guess the computer makes.
            int count = 0;

            while(guess != actualNumber)
            {
                if (guess > FindMedianOfArray(arr) && actualNumber > FindMedianOfArray(arr) && guess >= arr[0] && guess <= arr[arr.Length-1])
                {
                    Console.WriteLine("Both the correct number and the computer's guess were in the lower bisection of the array.");
                    arr = SplitArrayUpper(arr, actualNumber);
                    Console.WriteLine("The new array is now" );
                    foreach (int x in arr)
                    {
                        Console.Write(x.ToString() + " ");
                    }
                    FindMedianOfArray(arr);
                    Console.WriteLine($"\nNew median is {FindMedianOfArray(arr)}");
                    guess = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
                    Console.WriteLine($"New guess is {guess}");
                    count++;
                    Console.WriteLine("Press enter to continue\n");
                    Console.ReadKey();

                }
                else if (guess <= FindMedianOfArray(arr) && actualNumber <= FindMedianOfArray(arr) && guess >= arr[0] && guess <= arr[arr.Length - 1])
                {
                    Console.WriteLine("Both the correct number and the computer's guess were in the lower bisection of the array.");
                    arr = SplitArrayLower(arr, actualNumber);
                    Console.WriteLine("The new array is now");
                    foreach (int x in arr)
                    {
                        Console.Write(x.ToString() + " ");
                    }
                    FindMedianOfArray(arr);
                    Console.WriteLine($"\nNew median is {FindMedianOfArray(arr)}");
                    guess = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
                    Console.WriteLine($"New guess is {guess}");
                    count++;
                    Console.WriteLine("Press enter to continue\n");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("The Computer's guess and the correct number were in different bisections of the array.");
                    guess = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
                    Console.WriteLine($"New guess is {guess}");
                    count++;
                    Console.WriteLine("Press enter to cotinue\n");
                    Console.ReadKey();
                    
                }
            }
            Console.WriteLine($"It took the computer {count} guesses to get the right number. I find that 20 to 25 is the average.");
        }
        public static void HumanPlays()
        {
            Random rng = new Random();
            Console.WriteLine("Now that you know how my program plays this game we will have you play\n" +
                "with numbers between 1 and 100");
            int[] arr = MakeAnArray(1, 100);
            int actualNumber = rng.Next(arr[0] - 1, arr[arr.Length - 1] + 1);
            Console.Write("Enter a number you wish to start with: ");
            int count = 0;
            int guess = UserGuess();
            while (guess != actualNumber) 
            {
                if (guess > FindMedianOfArray(arr) && actualNumber > FindMedianOfArray(arr) && guess >= arr[0] && guess <= arr[arr.Length - 1])
                {
                    Console.WriteLine("You've guessed in the correct bisection of the array.");
                    arr = SplitArrayUpper(arr, actualNumber);
                    FindMedianOfArray(arr);
                    Console.WriteLine($"Now pick a guess between {arr[0]} and {arr[arr.Length - 1]}");
                    count++;
                    guess = UserGuess();
                }
                else if (guess <= FindMedianOfArray(arr) && actualNumber <= FindMedianOfArray(arr) && guess >= arr[0] && guess <= arr[arr.Length - 1])
                {
                    Console.WriteLine("You've guessed in the correct bisection of the array.");
                    arr = SplitArrayLower(arr, actualNumber);
                    FindMedianOfArray(arr);
                    Console.WriteLine($"Now pick a guess between {arr[0]} and {arr[arr.Length - 1]}");
                    count++;
                    guess = UserGuess();
                }
                else
                {
                    Console.WriteLine("Your guess was not even in the right bisection. Try again");
                    count++;
                    guess = UserGuess();
                }
            }
            if (count == 0)
            {
                Console.WriteLine("You go the correct number in one guess. Good Job.");
            }
            else
            {
                Console.WriteLine($"It took you {count} tries to get the correct number. The average is between 10 and 15.");
            }
        }
        static void Main(string[] args)
        {
            GuessNumbersComputer();
            HumanPlays();
        }
    }
}
