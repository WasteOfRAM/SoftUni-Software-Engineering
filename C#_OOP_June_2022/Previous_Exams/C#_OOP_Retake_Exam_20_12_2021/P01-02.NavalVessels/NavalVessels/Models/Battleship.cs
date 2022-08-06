namespace NavalVessels.Models
{
    using Contracts;

    public class Battleship : Vessel, IBattleship
    {
        private const double InitialArmourThickness = 300;

        public Battleship(string name, double mainWeaponCaliber, double speed) 
            : base(name, mainWeaponCaliber, speed, InitialArmourThickness)
        {
            this.SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmourThickness;
        }

        public void ToggleSonarMode()
        {
            this.SonarMode = !this.SonarMode;

            if (this.SonarMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }

            if (!this.SonarMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" *Sonar mode: {(this.SonarMode ? "ON" : "OFF")}";
        }
    }
}
