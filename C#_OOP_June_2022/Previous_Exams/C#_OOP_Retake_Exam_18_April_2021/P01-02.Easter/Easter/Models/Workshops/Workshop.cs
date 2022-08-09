namespace Easter.Models.Workshops
{
    using Contracts;
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Eggs.Contracts;
    using System.Linq;

    public class Workshop : IWorkshop
    {

        // ??????
        public void Color(IEgg egg, IBunny bunny)
        {
            while(bunny.Energy > 0 && bunny.Dyes.Any(d => !d.IsFinished()) && !egg.IsDone())
            {
                foreach (var dye in bunny.Dyes.Where(d => !d.IsFinished()))
                {
                    while (!dye.IsFinished() && bunny.Energy > 0 && !egg.IsDone())
                    {
                        dye.Use();
                        bunny.Work();
                        egg.GetColored();
                    }
                }
            }
        }
    }
}
