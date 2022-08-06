namespace NavalVessels.Core
{
    using Contracts;
    using Models.Contracts;
    using NavalVessels.Models;
    using NavalVessels.Utilities.Messages;
    using Repositories;
    using Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class Controller : IController
    {
        private IRepository<IVessel> vessels;
        private ICollection<ICaptain> captains;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new List<ICaptain>();
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var captain = this.captains.FirstOrDefault(cap => cap.FullName == selectedCaptainName);
            var vessel = this.vessels.FindByName(selectedVesselName);

            if (captain == null)
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);

            if (vessel == null)
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);

            if(vessel.Captain != null)
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);

            captain.AddVessel(vessel);
            vessel.Captain = captain;

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackingVessel = this.vessels.FindByName(attackingVesselName);
            var defendingVessel = this.vessels.FindByName(defendingVesselName);

            if (attackingVessel == null)
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);

            if (defendingVessel == null)
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);

            if(attackingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);

            if (defendingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);

            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVessel.Name, attackingVessel.Name, defendingVessel.ArmorThickness);
        }

        public string CaptainReport(string captainFullName)
        {
            var captain = this.captains.FirstOrDefault(cap => cap.FullName == captainFullName);

            return captain.Report();
        }

        public string HireCaptain(string fullName)
        {
            string result = string.Empty;

            if(this.captains.Any(cap => cap.FullName == fullName))
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);

            this.captains.Add(new Captain(fullName));

            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel = this.vessels.FindByName(name);

            if (vessel != null)
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);

            if(vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else
            {
                return OutputMessages.InvalidVesselType;
            }

            this.vessels.Add(vessel);

            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string ServiceVessel(string vesselName)
        {
            var vessel = this.vessels.FindByName(vesselName);

            if (vessel == null)
                return string.Format(OutputMessages.VesselNotFound, vesselName);

            vessel.RepairVessel();

            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var vessel = this.vessels.FindByName(vesselName);

            if(vessel is Battleship battleship)
            {
                battleship.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, battleship.Name);
            }
            else if (vessel is Submarine submarine)
            {
                submarine.ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, submarine.Name);
            }

            return string.Format(OutputMessages.VesselNotFound, vesselName);
        }

        public string VesselReport(string vesselName)
        {
            var vessel = this.vessels.FindByName(vesselName);

            return vessel.ToString();
        }
    }
}
