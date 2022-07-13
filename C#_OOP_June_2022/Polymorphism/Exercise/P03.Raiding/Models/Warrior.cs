namespace P03.Raiding.Models
{
    public class Warrior : BaseHero
    {
        private const int HeroPower = 100;

        public Warrior(string name)
            : base(name, HeroPower)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
        }
    }
}
