namespace NavalVessels.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class VesselRepository : IRepository<IVessel>
    {
        private readonly List<IVessel> models;

        public VesselRepository()
        {
            this.models = new List<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => this.models;

        public void Add(IVessel model)
        {
            this.models.Add(model);
        }

        public IVessel FindByName(string name)
        {
            return this.models.FirstOrDefault(vn => vn.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return this.models.Remove(model);
        }
    }
}
