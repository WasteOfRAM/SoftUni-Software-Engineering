using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Car
    {
        private string model;
        private Engine engine;
        private int? weight;
        private string color;

        public string Model { get { return this.model; } set { this.model = value; } }
        public Engine Engine { get { return this.engine; } set { this.engine = value; } }
        public int? Weight { get { return this.weight; } set { this.weight = value; } }
        public string Color { get { return this.color; } set { this.color = value; } }

        public Car(string model, Engine engine)
        {
            this.model = model;
            this.Engine = engine;
            this.Weight = null;
            this.color = null;
        }

        public Car(string model, Engine engine, int weight)
            : this(model, engine)
        {
            this.weight = weight;
            this.color = null;
        }

        public Car(string model, Engine engine, string color)
            : this(model, engine)
        {
            this.weight = null;
            this.color = color;
        }

        public Car(string model, Engine engine, int weight, string color)
        {
            this.model = model;
            this.engine = engine;
            this.weight = weight;
            this.color = color;
        }


        public override string ToString()
        {
            string carWeight = "n/a";
            string carColor = "n/a";
            string engineDisp = "n/a";
            string engineEff = "n/a";

            if(this.weight != null)
                carWeight = this.weight.ToString();

            if (this.color != null)
                carColor = this.Color;

            if(this.engine.Displacement != null)
                engineDisp = this.engine.Displacement.ToString();

            if(this.engine.Efficiency != null)
                engineEff = this.engine.Efficiency.ToString();

            string result =
                $"{this.Model}:\n" +
                $"  {this.Engine.Model}:\n" +
                $"    Power: {this.Engine.Power}\n" +
                $"    Displacement: {engineDisp}\n" +
                $"    Efficiency: {engineEff}\n" +
                $"  Weight: {carWeight}\n" +
                $"  Color: {carColor}";

            return result;
        }
    }
}
