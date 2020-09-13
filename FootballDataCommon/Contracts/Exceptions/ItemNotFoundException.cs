namespace FootballDataCommon.Contracts.Exceptions
{
    public class ItemNotFoundException : System.Exception
    {
        public ItemNotFoundException(System.Exception inner) : base("Not found", inner) {

         }

    }
}