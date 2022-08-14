namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.MilitaryUnits.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> models;

        public UnitRepository()
        {
            this.models = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => this.models.AsReadOnly();

        public void AddItem(IMilitaryUnit model) => this.models.Add(model);

        public IMilitaryUnit FindByName(string name) => this.models.FirstOrDefault(u => u.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            var unit = this.models.FirstOrDefault(models => models.GetType().Name == name);

            return this.models.Remove(unit);
        }
    }
}
