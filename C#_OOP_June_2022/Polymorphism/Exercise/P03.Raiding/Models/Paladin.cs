namespace P03.Raiding.Models
{
    public class Paladin : BaseHero
    {
        private const int HeroPower = 100;

        public Paladin(string name)
            : base(name, HeroPower)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
