namespace ClaimProcessing.Domain.Exceptions
{
    public class NullException :Exception
    {
        public NullException(int id) : base($"Object with given Id number: {id} is not found")
        {

        }
    }
}
