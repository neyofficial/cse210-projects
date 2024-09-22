using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for the grade percentage
        Console.Write("Enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        // Initialize the letter and sign variables
        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign (+ or -)
        int lastDigit = percentage % 10;

        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3)
        {
            sign = "-";
        }

        // Don't assign a "+" or "-" for A+ or F grades
        if (letter == "A" && sign == "+")
        {
            sign = ""; // No A+
        }
        if (letter == "F")
        {
            sign = ""; // No F+ or F-
        }

        // Print the letter grade with the sign
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Check if the user passed or failed
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Sorry, you did not pass. Better luck next time!");
        }
    }
}
