namespace ClaimProcessing.Domain.Exceptions
{
    public class DimensionsException : Exception
    {
        public DimensionsException(string dimensions, Exception ex) : base($"Given dimensions set {dimensions} are not valid", ex)
        {
            
        }
    }
}
