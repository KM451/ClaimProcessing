using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address() { }

        public Address(string street, string city, string country, string zipcode)
        {
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipcode;
        }

        public override string ToString()
        {
            return $"{Street}/n{ZipCode} {City}/n{Country}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }
    }
}
