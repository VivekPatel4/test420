using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loops
{
    class Program
    {
        static void Main()
        {

            int age = 25;
            double price = 99.99;
            char grade = 'A';
            bool isPassed = true;
            string name = "vivek";


            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Price: $" + price);
            Console.WriteLine("Grade: " + grade);
            Console.WriteLine("Passed: " + isPassed);




            //for loop
            for (int p = 1; p <= 5; p++)
            {
                Console.WriteLine("Iteration: " + p);
            }

            //10 table
            int number = 10;
            Console.WriteLine("Multiplication Table of " + number);

            for (int j = 1; j <= 10; j++)
            {
                Console.WriteLine(number + " x " + j + " = " + (number * j));
            }
            //while loops
            int count = 1;
            while (count <= 5)
            {
                Console.WriteLine("Count: " + count);
                count++;
            }

            //do while loop
            int num = 1;
            do
            {
                Console.WriteLine("Number: " + num);
                num++;
            } while (num <= 5);

            //6 table
            int number1 = 6;
            int i = 1; 

            Console.WriteLine("Multiplication Table of " + number1);

            while (i <= 10)
            {
                Console.WriteLine(number1 + " x " + i + " = " + (number1 * i));
                i++; // Increment counter
            }

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
}
