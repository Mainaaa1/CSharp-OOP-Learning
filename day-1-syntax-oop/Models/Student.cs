namespace CSharpOOP.Models
{
    public class Student : Person
    {
        public string Course { get; private set; }

        public Student(string name, int age, string course)
            : base(name, age)
        {
            Course = course;
        }

        public override string GetDetails()
        {
            return $"Student: {Name}, Age: {Age}, Course: {Course}";
        }

        public override void Introduce()
        {
            Console.WriteLine($"Hi, I'm {Name}, studying {Course}.");
        }
    }
}
