namespace P05.Football_Team_Generator
{
    public abstract class ExeptionMessage
    {
        public const string InvalidStats = "{0} should be between {1} and {2}.";
        public const string InvalidName = "A name should not be empty.";
        public const string PlayerNotInTeam = "Player {0} is not in {1} team.";
        public const string TeamDoesNotExist = "Team {0} does not exist.";
    }
}
