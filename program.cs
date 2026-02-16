using System;
using CSharpOOP.Models;
using CSharpOOP.Services;

namespace CSharpOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# OOP Practice ===\n");

            Student student = new Student("Ian", 24, "Software Engineering");
            Instructor instructor = new Instructor("Dr. Smith", 45, "Computer Science");

            ConsolePrinter printer = new ConsolePrinter();

            printer.Print(student.GetDetails());
            printer.Print(instructor.GetDetails());

            Console.WriteLine("\nPolymorphism Demo:");
            Person personRef = student;
            personRef.Introduce();
        }
    }
}
