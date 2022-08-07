namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double EquipmentWeight = 10000;
        private const decimal EquipmentPrice = 80;

        public Kettlebell() 
            : base(EquipmentWeight, EquipmentPrice)
        {
        }
    }
}
