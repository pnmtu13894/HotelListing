using System;
using System.Globalization;

namespace HotelListing.DTO.ExceptionHandler
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, params object [] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}