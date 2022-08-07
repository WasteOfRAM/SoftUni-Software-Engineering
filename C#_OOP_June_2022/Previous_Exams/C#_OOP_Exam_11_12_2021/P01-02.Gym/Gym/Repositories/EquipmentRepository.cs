namespace Gym.Repositories
{
    using Contracts;
    using Models.Equipment.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly HashSet<IEquipment> models;

        public EquipmentRepository()
        {
            this.models = new HashSet<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => this.models;

        public void Add(IEquipment model)
        {
            this.models.Add(model);
        }

        public IEquipment FindByType(string type) => this.models.FirstOrDefault(et => et.GetType().Name == type);

        public bool Remove(IEquipment model) => this.models.Remove(model);
    }
}
