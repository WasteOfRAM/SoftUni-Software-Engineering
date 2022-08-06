namespace NavalVessels.Models
{
    using Contracts;
    public class Submarine : Vessel, ISubmarine
    {
        private const double InitialArmourThickness = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed) 
            : base(name, mainWeaponCaliber, speed, InitialArmourThickness)
        {
            this.SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmourThickness;
        }

        public void ToggleSubmergeMode()
        {
            this.SubmergeMode = !this.SubmergeMode;

            if (this.SubmergeMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }

            if (!this.SubmergeMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" *Submerge mode: {(this.SubmergeMode ? "ON" : "OFF")}";
        }
    }
}
