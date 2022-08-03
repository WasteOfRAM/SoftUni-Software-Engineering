namespace Heroes.Models.Map
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using Heroes;

    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            HashSet<IHero> knights = players.Where(ht => ht.GetType() == typeof(Knight)).ToHashSet();
            HashSet<IHero> barbarians = players.Where(ht => ht.GetType() == typeof(Barbarian)).ToHashSet();

            int knightDeats = 0;
            int barbarianDeats = 0;

            while (knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                foreach (var knight in knights.Where(a => a.IsAlive))
                {
                    foreach (var barbarian in barbarians.Where(a => a.IsAlive))
                    {
                        if (barbarian.IsAlive)
                        {
                            barbarian.TakeDamage(knight.Weapon.DoDamage());

                            if (!barbarian.IsAlive)
                                barbarianDeats++;
                        }
                    }
                }

                foreach (var barbarian in barbarians.Where(a => a.IsAlive))
                {
                    foreach (var knight in knights.Where(a => a.IsAlive))
                    {
                        if (knight.IsAlive)
                        {
                            knight.TakeDamage(barbarian.Weapon.DoDamage());

                            if (!knight.IsAlive)
                                knightDeats++;
                        }
                    }
                }
            }

            string result;

            if (knights.Any(k => k.IsAlive))
                result = $"The knights took {knightDeats} casualties but won the battle.";
            else
                result = $"The barbarians took {barbarianDeats} casualties but won the battle.";

            return result;
        }
    }
}
