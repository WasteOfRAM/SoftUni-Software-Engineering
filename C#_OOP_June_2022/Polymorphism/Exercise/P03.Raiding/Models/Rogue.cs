namespace P03.Raiding.Models
{
    public class Rogue : BaseHero
    {
        private const int HeroPower = 80;

        public Rogue(string name)
            : base(name, HeroPower)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
        }
    }
}
