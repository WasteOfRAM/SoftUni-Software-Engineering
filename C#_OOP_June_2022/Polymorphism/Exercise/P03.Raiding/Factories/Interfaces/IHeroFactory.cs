namespace P03.Raiding.Factories.Interfaces
{
    using Models;
    public interface IHeroFactory
    {
        BaseHero CreateHero(string name, string type);
    }
}
