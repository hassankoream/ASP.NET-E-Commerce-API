using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace ServicesAbstractions
{
    public interface IAuthenticationService
    {
        //Login
        //Take Email, Password, then return Token, Email, and DisplayName
        Task<UserDto> LoginAsync(LoginDto loginDto);   
        //Register
        //Take Email, Password, UserName, DisplayName, and PhoneNumber.
        //then return Token, Email, and DisplayName 

        Task<UserDto> RegisterAsync( RegisterDto registerDto);


        //Check Email
        //Take Email and return boolean
        Task<bool> CheckEmailAsync(string Email);

        //Get Current Address
        //Take Email and Current AddressDTO
        Task<AddressDto> GetCurrentUserAddressAsync(string Email);
        //Update Current User Address 
        //Take Updated AddressDTO and Email Then Return AddressDTO after Update To Client  
        Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto);

        //Get Current User 
        //Take Email Then Return UserDTO : Token , Email and Display Name To Client  

        Task<UserDto> GetCurrentUserAsync(string Email);
    }
}
