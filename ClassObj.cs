using System;
using System.Net.NetworkInformation;

namespace ClassObj
{
    //class and object


    class Employee
    {
        public string name;

        public void work(string work)
        {
            Console.WriteLine("Work: " + work);

        }
    }

    class EmployeeDrive
    {
        public static void Main(string[] args)
        {
            Employee e1 = new Employee();

            Console.WriteLine("Employee 1");

            e1.name = "vivek";
            Console.WriteLine("Name: " + e1.name);

            e1.work("Coding");

            Console.ReadLine();

        }
    }


    //Encapsulation

    public class Person
    {
        private int age;

        public void SetAge(int newAge)
        {
            if (newAge > 0)
            {
                age = newAge;
            }
        }

        public int GetAge()
        {
            return age;
        }
    }
    public class Person2
    {
        public static void Main(string[] args)
        {
            Person person = new Person();
            person.SetAge(25);
            Console.WriteLine(person.GetAge());
            Console.ReadLine();
        }

    }


    // Inheritance


    class Vehicle
    {
        public void calculateSpeed(int initialSpeed, double acceleration, double time)
        {
            double finalSpeed = initialSpeed + acceleration * time;
            Console.WriteLine("Final speed after " + time + " seconds is " + finalSpeed + " m/s.");
        }

    }
    class Car : Vehicle
    {

        public int initialSpeed = 0;

        public double acceleration = 9.8;

        public double time = 10;

        public void displayCarDetails()
        {
            Console.WriteLine("Car is moving at an initial speed of " + initialSpeed + " m/s.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car();
            myCar.displayCarDetails();
            myCar.calculateSpeed(myCar.initialSpeed, myCar.acceleration, myCar.time);
            Console.ReadLine();
        }
    }


    //Polymorphism 

    class Animal
    {
        public string name;

        public void display()
        {
            Console.WriteLine("I am an animal");
        }

    }
    class Dog : Animal
    {
        public void getName()
        {
            Console.WriteLine("My name is " + name);
        }
    }
    class Program3
    {
        static void Main(string[] args)
        {
            Dog labrador = new Dog();

            labrador.name = "Rohu";
            labrador.display();

            labrador.getName();

            Console.ReadLine();
        }

    }


    //Abstraction
    abstract class Language
    {
        public void display()
        {
            Console.WriteLine("Non abstract method");
        }
    }
    class Program2 : Language
    {
        static void Main(string[] args)
        {
            Program2 obj = new Program2();
            obj.display();
            Console.ReadLine();
        }
    }

    //Abstraction methos 
    abstract class Animal1
    {
        public abstract void makeSound();
    }
    class Dog1 : Animal1
    {
        public override void makeSound()
        {

            Console.WriteLine("Bark Bark");

        }
    }
    class Program4
    {
        static void Main(string[] args)
        {
            Dog1 obj = new Dog1();
            obj.makeSound();

            Console.ReadLine();
        }
    }

    //method 
    class Program1
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }
        public static void Greet(string name)
        {
            Console.WriteLine("Hello, " + name);
        }

        public static void display(int num)
        {
            Console.WriteLine($"Integer: {num}");
        }

        public static void display(String str)
        {
            Console.WriteLine($"String: {str}");
        }
        public static void Main(string[] args)
        {
            int sum = Add(5, 7);
            Console.WriteLine("Sum: " + sum);
            Greet("vivek");
            display(25);
            display("neel");
            Console.ReadLine();
        }
    }
}
