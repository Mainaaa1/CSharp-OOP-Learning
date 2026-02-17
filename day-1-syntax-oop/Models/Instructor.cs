namespace CSharpOOP.Models
{
    public class Instructor : Person
    {
        public string Department { get; private set; }

        public Instructor(string name, int age, string department)
            : base(name, age)
        {
            Department = department;
        }

        public override string GetDetails()
        {
            return $"Instructor: {Name}, Age: {Age}, Department: {Department}";
        }
    }
}
