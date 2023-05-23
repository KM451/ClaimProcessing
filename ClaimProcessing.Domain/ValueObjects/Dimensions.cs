using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.Exceptions;

namespace ClaimProcessing.Domain.ValueObjects
{
    public class Dimensions : ValueObject
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }

        public Dimensions()
        {
        }

        public Dimensions(double height, double width, double depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }
        public override string ToString()
        {
            return $"{Height}x{Width}x{Depth}";
        }

        /// <summary>
        /// Create the Dimensions object basis given string value
        /// </summary>
        /// <param name="hwd">A string containing values of three dimensions.</param>
        /// <returns>Dimensions object</returns>
        /// <exception cref="DimensionsException">Represents errors that occur during evaluation of given 'hwd' string. </exception>
        public static Dimensions For(string hwd)
        {
            var dimensions = new Dimensions();
            try
            {
                var dimensionsSet = hwd.Split('x');
                dimensions.Height = double.Parse(dimensionsSet[0]);
                dimensions.Width = double.Parse(dimensionsSet[1]);
                dimensions.Depth = double.Parse(dimensionsSet[2]);
            }
            catch (Exception ex)
            {
                throw new DimensionsException(hwd, ex);
            }
            return dimensions;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Height;
            yield return Width;
            yield return Depth;
        }
    }
}

