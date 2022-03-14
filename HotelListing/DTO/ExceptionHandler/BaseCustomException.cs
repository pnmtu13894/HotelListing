using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTO.ExceptionHandler
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException() : base() { }

        public BaseCustomException(string message) : base(message) { }

        public BaseCustomException(string message, params object [] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
