﻿using System;
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

            List<string> uniqueNames = persons
                .Select(person => person.Name)
                .Distinct()
                .ToList();

            Console.WriteLine("Имена: " + string.Join(", ", uniqueNames));

            List<Person> personsUnder18 = persons
                .Where(p => p.Age < 18)
                .ToList();

            double averageAge = personsUnder18.Average(p => p.Age);
            Console.WriteLine("Средний возраст людей до 18 лет = " + averageAge);

            var averageAgesByName = persons
                .GroupBy(p => p.Name)
                .ToDictionary(group => group.Key, group => group.Average(person => person.Age));

            foreach (var pair in averageAgesByName)
            {
                Console.WriteLine("Имя = " + pair.Key + ", Средний возраст = " + pair.Value);
            }

            List<string> personsNamesFrom20To45 = persons
                .Where(p => p.Age >= 20 && p.Age <= 45)
                .OrderByDescending(p => p.Age)
                .Select(p => p.Name)
                .ToList();

            Console.WriteLine(string.Join(", ", personsNamesFrom20To45));
        }
    }
}
