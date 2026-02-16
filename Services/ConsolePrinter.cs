using System;
using CSharpOOP.Interfaces;

namespace CSharpOOP.Services
{
    public class ConsolePrinter : IPrintable
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
