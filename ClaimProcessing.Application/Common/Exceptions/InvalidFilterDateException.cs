namespace ClaimProcessing.Application.Common.Exceptions
{
    public class InvalidFilterDateException : Exception
    {
        public InvalidFilterDateException(string date) : base($"Wartość podana jako data: '{date}' jest nieprawidłowa.")
        {
                
        }
    }
}
