namespace ClaimProcessing.Application.Common.Exceptions
{
    public class NullException :Exception
    {
        public NullException(int id) : base($"Object with given Id no. {id} is not found.")
        {

        }
    }
}
