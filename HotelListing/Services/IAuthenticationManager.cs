using HotelListing.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);

        Task<string> CreateToken();
    }
}
