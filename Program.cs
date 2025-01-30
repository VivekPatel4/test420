using System;
class Program
{
    static void Main()
    {
        int age = 5;
        double price = 99.99;
        char grade = 'A';
        bool isPassed = true;
        string name = "Patel vivek";

        Console.WriteLine("Age :" + age);
        Console.WriteLine("Price :" + price);
        Console.WriteLine("grade :" + grade);
        Console.WriteLine("ispassed :" + isPassed);
        Console.WriteLine("name :" + name);



        //for loop
        for (int i = 1; i<= 5; i++)
        {
            Console.WriteLine(i);
        }

        int number = 5;
        Console.WriteLine("Multiplication Table of " + number);

        for (int j = 1; j <= 10; j++)
        {
            Console.WriteLine(number + " x " + j + " = " + (number * j));
        }

        //while loop
        int count = 1;
        while (count <= 5)
        {
            Console.WriteLine("Count: " + count);
            count++;
        }

        int number1 = 2; // Table of 5
        int p= 1; // Counter variable

        Console.WriteLine("Multiplication Table of " + number);

        while (p <= 10)
        {
            Console.WriteLine(number + " x " + p + " = " + (number * p));
            p++; // Increment counter
        }


        //do while loop
        int num = 1;
        do
        {
            Console.WriteLine("Number: " + num);
            num++;
        } while (num <= 5);


        //foreach
        string[] fruits = { "Apple", "Banana", "Mango", "Orange" };

        Console.WriteLine("Foreach Loop Example:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine("Fruit: " + fruit);
        }

        Console.ReadLine();
    }
}