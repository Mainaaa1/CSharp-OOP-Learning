using System;

namespace CSharpOOP.Models
{
    public abstract class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        protected Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void Introduce()
        {
            Console.WriteLine($"Hi, I'm {Name} and I'm {Age} years old.");
        }

        public abstract string GetDetails();
    }
}
