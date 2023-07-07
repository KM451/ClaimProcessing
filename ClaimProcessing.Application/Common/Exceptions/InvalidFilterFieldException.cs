using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Common.Exceptions
{
    public class InvalidFilterFieldException : Exception
    {
        public InvalidFilterFieldException(string field) : base($"Wartość podana jako filtrowane pole: '{field}' jest nieprawidłowa.")
        {
             
        }
    }
}
