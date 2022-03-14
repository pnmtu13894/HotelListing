using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTO.ExceptionHandler
{
    public class ApiResonse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        
        public static ApiResonse<T> Fail(string errorMessage)
        {
            return new ApiResonse<T> { Succeeded = false, Message = errorMessage };
        }

        public static ApiResonse<T> Success(T data)
        {
            return new ApiResonse<T> { Succeeded = true, Data = data };
        }
    }
}
