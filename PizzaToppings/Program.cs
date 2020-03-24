using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            //need to compare the arrays inside of the pizza to one another
            var toppingLists = pizzas.Select(p => string.Join(",", p.Toppings.OrderBy(t => t)));

            //getting distinct topping combinations
           // var distinctToppingCombinations = toppingLists.Distinct();

            var countOfCombinations = new Dictionary<string, int>();

            foreach (var combination in toppingLists)
            {
                if (!countOfCombinations.ContainsKey(combination))
                {
                    countOfCombinations.Add(combination, 1);
                }
                else
                {
                    countOfCombinations[combination] += 1;
                }
            }

            var mostOrdered = countOfCombinations.OrderByDescending(item => item.Value).Take(20);

            foreach (var (combination, count) in mostOrdered)
            {
                Console.WriteLine($"The topping combination of {combination} was ordered {count} times.");
            }
        }
    }
}
