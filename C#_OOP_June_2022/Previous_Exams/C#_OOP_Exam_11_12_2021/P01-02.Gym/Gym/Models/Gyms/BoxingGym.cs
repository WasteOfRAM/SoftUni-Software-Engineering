namespace Gym.Models.Gyms
{
    public class BoxingGym : Gym
    {
        private const int AthletesCapacity = 15;

        public BoxingGym(string name) 
            : base(name, AthletesCapacity)
        {
        }
    }
}
