namespace ClaimProcessing.Application.Common.Exceptions
{
    public class InvalidFilterKeyException : Exception
    {
        public InvalidFilterKeyException(string key) : base($"Wartość podana jako klucz filtrowania: '{key}' jest nieprawidłowa.")
        {
                
        }
    }
}
