using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambda
{
    class Lambda
    {
        static void Main()
        {
            List<Person> persons = new List<Person>
            {
                new Person("Дмитрий", 43),
                new Person("Сергей", 18),
                new Person("Анна", 34),
                new Person("Сергей", 21),
                new Person("Павел", 18),
                new Person("Анна", 10),
                new Person("Степан", 14),
                new Person("Михаил", 60)
            };

            List<string> uniqueNames = persons.Select(person => person.Name).Distinct().ToList();
            Console.WriteLine("Имена: " + string.Join(", ", uniqueNames));

            uniqueNames.ForEach(name =>
           {
               if (uniqueNames.First() == name)
               {
                   Console.Write("Имена: ");
               }

               if (name != uniqueNames.Last())
               {
                   Console.Write(name + ", ");
               }
               else
               {
                   Console.WriteLine(name + ".");
               }
           });

            List<Person> peoplesUnder18 = persons.Where(person => person.Age < 18).ToList();
            int avarageAge = (int)peoplesUnder18.Select(people => people.Age).Average();
            Console.WriteLine("Средний возраст людей до 18 лет = " + avarageAge);

            Dictionary<string, int> personsByAvarageAge = persons.GroupBy(person => person.Name).ToDictionary(group => group.Key, group => (int)group.Select(person => person.Age).Average());

            List<string> peopleFrom20To45 = persons.Where(person => person.Age >= 20 && person.Age <= 45).OrderByDescending(person => person.Age).Select(person => person.Name).ToList();
            Console.WriteLine(string.Join(", ", peopleFrom20To45));
        }
    }
}
