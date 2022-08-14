namespace PlanetWars.Repositories
{
    using Contracts;
    using PlanetWars.Models.Weapons.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> models;

        public WeaponRepository()
        {
            this.models = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.models.AsReadOnly();

        public void AddItem(IWeapon model) => this.models.Add(model);

        public IWeapon FindByName(string name) => this.models.FirstOrDefault(w => w.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            var weapon = this.models.FirstOrDefault(w => w.GetType().Name == name);

            return this.models.Remove(weapon);
        }
    }
}
