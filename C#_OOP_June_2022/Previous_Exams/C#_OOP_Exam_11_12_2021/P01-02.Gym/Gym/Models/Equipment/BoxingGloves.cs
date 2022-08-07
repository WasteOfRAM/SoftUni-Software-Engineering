namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const double EquipmentWeight = 227;
        private const decimal EquipmentPrice = 120;

        public BoxingGloves() 
            : base(EquipmentWeight, EquipmentPrice)
        {
        }
    }
}
