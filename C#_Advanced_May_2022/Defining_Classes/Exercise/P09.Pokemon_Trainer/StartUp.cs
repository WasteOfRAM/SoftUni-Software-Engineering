using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses

{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            List<Trainer> trainers = new List<Trainer>();

            string input;
            while ((input = Console.ReadLine()) != "Tournament")
            {
                string trainerName = input.Split()[0];
                string pokemonName = input.Split()[1];
                string pokemonElement = input.Split()[2];
                int pokemonHealth = int.Parse(input.Split()[3]);

                Trainer trainer = trainers.Find(t => t.Name == trainerName);
                Pokemon pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);

                if (trainer == null)
                {
                    trainer = new Trainer(trainerName);
                    trainer.Pokemons.Add(pokemon); 
                    trainers.Add(trainer);
                }
                else
                {
                    trainer.Pokemons.Add(pokemon);
                }

            }

            while ((input = Console.ReadLine()) != "End")
            {
                Battle(input, trainers);
            }

            Console.WriteLine(string.Join(Environment.NewLine, trainers.OrderByDescending(b => b.Badges)));
        }

        private static void Battle(string element, List<Trainer> trainers)
        {
            foreach (var trainer in trainers)
            {
                if (trainer.Pokemons.Any(p => p.Element == element))
                {
                    trainer.Badges++;
                }
                else
                {
                    trainer.Pokemons.ForEach(p => p.Health -= 10);

                    trainer.Pokemons = trainer.Pokemons.Where(p => p.Health > 0).ToList();
                }
            }
        }
    }
}
