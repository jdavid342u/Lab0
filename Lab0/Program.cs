// See https://aka.ms/new-console-template for more information

/* By David Ochoa (SAIT) */

using System;
using System.IO;

Console.WriteLine("------Low number------");
int lowNum = askLowNumber();
double lowNumDbl = (double)lowNum;
Console.WriteLine("------Low number------\n");

Console.WriteLine("------High number------");
int highNum = askHigherNumber(lowNum);
double highNumDbl = (double)highNum;
Console.WriteLine("------High number------\n");

Console.WriteLine("------Difference------");
int difference = highNum - lowNum;
double differenceDbl = (double)difference;

Console.WriteLine($"The difference between {highNum} and {lowNum} is {difference}");
Console.WriteLine("------Difference------\n");

int[] numbers = new int[difference-1];

for(int i = 0; i < numbers.Length; i++)
{
    numbers[i] = lowNum + (i+1);
}

List<int> numbersList = Enumerable.Range(lowNum+1, difference-1).ToList();
Console.WriteLine($"------List elements (numbers in between {lowNum} and {highNum})------");
foreach (int num in numbersList)
{
    Console.WriteLine(num);
}
Console.WriteLine($"------List elements (numbers in between {lowNum} and {highNum})------\n");

/* Write the in-between numbers into a text file in folder "output" */
string filePath = Path.Combine(".", "lab0.txt");

using (StreamWriter writer = new StreamWriter(filePath))
{
    if (difference == 1)
    {
        writer.WriteLine($"There aren't any integers between {highNum} and {lowNum} since they are consecutive numbers.");
    }
    else
    {
        writer.WriteLine($"These are the numbers between {highNum} and {lowNum}: \n");

        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            writer.WriteLine(numbers[i]);
        }
    }
}

/* Read contents of the txt file and sum all the numbers in it */
Console.WriteLine($"------List Sum------");
string[] lines = File.ReadAllLines(filePath);
int sum = 0;

foreach (string line in lines)
{
    int i = 0;
    if(int.TryParse(line, out i) == false) continue;
    sum += i;
}

if (difference == 1)
{
    Console.WriteLine("Since the 2 numbers provided are consecutive, there aren't integers in between nor a value for their sum");
}
else
{
    Console.Write($"Sum is {sum} for numbers between {lowNum} and {highNum} (");
    /* Finish the sentence by printing the process of the sum (its elements) */
    for (int i = 0; i < numbers.Length; i++)
    {
        Console.Write(numbers[i]);
        if (i == numbers.Length - 1) /* if it's last item */
        {
            Console.Write(")\n");
        }
        else
        {
            Console.Write(" + ");
        }
    }
}
Console.WriteLine($"------List Sum------\n");

/* Print the prime numbers in the list */
Console.WriteLine("------Prime elements in list------");
foreach (int num in numbersList)
{
    if(isPrime(num)) Console.WriteLine(num);
}
Console.WriteLine("------Prime elements in list------\n");

/* Ask to exit so that user can read the output before app closes */
bool exit = false;
while (!exit)
{
    Console.WriteLine("Exit? Y: yes");
    string answer = Console.ReadLine().ToLower();
    if (answer == "y" || answer == "yes") break;
}

/* Methods and functions of the application */
int askValidInteger(string prompt = null)
{
    if (prompt != null) Console.WriteLine(prompt);
    int validInteger = 0;
    bool inputValid = false;
    
    while (!inputValid)
    {
        inputValid = int.TryParse(Console.ReadLine(), out validInteger);
        if (inputValid) break;
        Console.WriteLine("Invalid input. An integer is needed, please try again:");
    }

    return validInteger;
}

int askLowNumber()
{
    bool numIsNegative = true;
    int lowNum = 0;
    Console.WriteLine("Hello, please enter a positive low integer:");

    while (numIsNegative)
    {
        lowNum = askValidInteger();
        if (lowNum >= 0) break;
        Console.WriteLine("Integer can not be negative, please try again:");
    }

    return lowNum;
}

int askHigherNumber(int lowNum)
{
    bool numIsHigher = false;
    int highNum = 0;
    Console.WriteLine($"Hello, please enter a positive integer higher than the previous one ({lowNum}):");

    while (!numIsHigher)
    {
        highNum = askValidInteger();
        if (highNum > lowNum) break;
        Console.WriteLine($"This integer has to be higher than the previous one ({lowNum}), please try again:");
    }

    return highNum;
}

bool isPrime(int number)
{
    bool isPrime = true;

    /* If number is exactly divisible by at least one of the divisors between 2 and (number-1) inclusive, number isn't prime */
    for(int i = number-1; i > 1; i--)
    {
        if(number % i == 0)
        {
            isPrime = false;
            break;
        }
    }

    return isPrime;
}