namespace ClaimProcessing.Application.Common.Exceptions
{
    public class InvalidFilterException : Exception
    {
        public InvalidFilterException(string filter) : base($"Wartość podana jako filtr: '{filter}' jest nieprawidłowa.")
        {
                
        }
    }
}
