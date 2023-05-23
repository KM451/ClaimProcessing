using ClaimProcessing.Domain.ValueObjects;

namespace ClaimProcessing.Domain.Exceptions
{
    public class FullNameException : Exception
    {
        public FullNameException(string fullname, Exception ex) : base($"Given full name {fullname} is not valid.", ex)
        {
            
        }
    }
}
