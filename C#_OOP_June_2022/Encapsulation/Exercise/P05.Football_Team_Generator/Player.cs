namespace P05.Football_Team_Generator
{
    using System;

    public class Player
    {
        private string name;

        public Player(string name, Stats stats)
        {
            Name = name;
            Stats = stats;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExeptionMessage.InvalidName);

                this.name = value;
            }
        }

        public Stats Stats { get; private set; }
    }
}
