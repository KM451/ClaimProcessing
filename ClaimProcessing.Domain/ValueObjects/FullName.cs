using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.Exceptions;

namespace ClaimProcessing.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public FullName()
        {
        }

        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        /// <summary>
        /// Create the FullName object basis given string value
        /// </summary>
        /// <param name="fullname">String containing first and last name</param>
        /// <returns>FullName object</returns>
        /// <exception cref="FullNameException">Represents errors that occur during evaluation of given 'fullname' string.</exception>
        public static FullName For(string fullname)
        {
            var fullnameObj = new FullName();
            try
            {
                var nameSet = fullname.Split(' ');
                fullnameObj.FirstName = nameSet[0].Trim();
                fullnameObj.LastName = nameSet[1].Trim();
            }
            catch
            { 
                throw new FullNameException(fullname);
            }
            return fullnameObj;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
