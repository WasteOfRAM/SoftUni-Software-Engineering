namespace P03.Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int HeroPower = 80;

        public Druid(string name) 
            : base(name, HeroPower)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
