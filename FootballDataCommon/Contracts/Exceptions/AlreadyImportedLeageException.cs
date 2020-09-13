namespace FootballDataCommon.Contracts.Exceptions
{
    public class AlreadyImportedLeageException : System.Exception
    {
        public AlreadyImportedLeageException(string leagueCode) : base("League already imported: " + leagueCode) { }
    }
}