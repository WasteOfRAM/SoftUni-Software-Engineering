namespace CarRacing.Repositories
{
    using Models.Cars.Contracts;
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> models;

        public CarRepository()
        {
            this.models = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => this.models;

        public void Add(ICar model) => this.models.Add(model);

        public ICar FindBy(string property) => this.models.FirstOrDefault(car => car.VIN == property);

        public bool Remove(ICar model) => this.models.Remove(model);
    }
}
