namespace Heroes.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class HeroRepository : IRepository<IHero>
    {
        private HashSet<IHero> heroes;

        public HeroRepository()
        {
            this.heroes = new HashSet<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => this.heroes;

        public void Add(IHero model)
        {
            this.heroes.Add(model);
        }

        public IHero FindByName(string name)
        {
            return this.heroes.FirstOrDefault(hero => hero.Name == name);
        }

        public bool Remove(IHero model)
        {
            return this.heroes.Remove(model);
        }
    }
}
