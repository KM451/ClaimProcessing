using ClaimProcessing.Domain.ValueObjects;

namespace ClaimProcessing.Domain.Exceptions
{
    public class FullNameException : Exception
    {
        //public FullNameException(string fullname, Exception ex) : base($"Given full name {fullname} is not valid. Kuku ryku", ex)
        public FullNameException(string fullname) : base($"Given full name '{fullname}' is not valid. Valid format is 'FirstName SecondName'.")
        {

        }
    }
}
