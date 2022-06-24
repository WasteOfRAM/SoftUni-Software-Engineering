namespace Zoo
{
    public class Animal
    {
        private string species;
        private string diet;
        private double weight;
        private double length;

        public Animal(string species, string diet, double weight, double length)
        {
            this.species = species;
            this.diet = diet;
            this.weight = weight;
            this.length = length;
        }

        public string Species { get => this.species; set => this.species = value; }
        public string Diet { get => this.diet; set => this.diet = value; }
        public double Weight { get => this.weight; set => this.weight = value; }
        public double Length { get => this.length; set => this.length = value; }

        public override string ToString()
        {
            return $"The {this.species} is a {this.diet} and weighs {this.weight} kg.";
        }
    }
}
