using System;
using System.Reflection;

namespace ReflectionExample
{
    // Клас, який містить 5 полів і 3 методи
    public class ExampleClass
    {
        public int Id;
        private string name;
        protected bool isActive;
        internal DateTime createdDate;
        protected internal double rating;

        public ExampleClass(int id, string name, bool isActive, DateTime createdDate, double rating)
        {
            this.Id = id;
            this.name = name;
            this.isActive = isActive;
            this.createdDate = createdDate;
            this.rating = rating;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {name}, Active: {isActive}, Created Date: {createdDate}, Rating: {rating}");
        }

        public int CalculateAge()
        {
            return DateTime.Now.Year - createdDate.Year;
        }

        private void SetRating(double newRating)
        {
            rating = newRating;
            Console.WriteLine($"New Rating: {rating}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExampleClass example = new ExampleClass(1, "My Name", true, new DateTime(2024, 1, 11), 4.5);

            // Використання Type та TypeInfo
            Type type = typeof(ExampleClass);
            Console.WriteLine("Type Name: " + type.Name);
            Console.WriteLine("Namespace: " + type.Namespace);
            
            // Використання MemberInfo для виведення інформації про члени класу
            MemberInfo[] members = type.GetMembers();
            Console.WriteLine("\nMembers of ExampleClass:");
            foreach (var member in members)
            {
                Console.WriteLine($"{member.MemberType}: {member.Name}");
            }

            // Використання FieldInfo для доступу до полів класу
            Console.WriteLine("\nField Info:");
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name} - {field.FieldType}");
            }

            // Використання MethodInfo для виклику методу через Reflection
            Console.WriteLine("\nMethod Info:");
            MethodInfo method = type.GetMethod("SetRating", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(example, new object[] { 5.0 });

            // Виклик публічного методу через Reflection
            Console.WriteLine("\nCalling public method through reflection:");
            MethodInfo printMethod = type.GetMethod("PrintInfo");
            printMethod.Invoke(example, null);

        }
    }
}
