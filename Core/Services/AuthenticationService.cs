using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractions;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            //if (User is null)
            //{
            //    return false;
                
            //}
            //else
            //{
            //    return true;
            //}
            return  User is not null;
            
        }
        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
           var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };


        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include( U => U.Address)
                                                .FirstOrDefaultAsync(U=>U.Email == Email) ?? throw new UserNotFoundException(Email);

            if (User.Address is not null)
                return _mapper.Map<Address, AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(User.UserName);
        
        }


        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(U => U.Address)
                                                 .FirstOrDefaultAsync(U => U.Email == Email) ?? throw new UserNotFoundException(Email);

            if (User.Address is not null) //Update Address
            {
                //return _mapper.Map<Address, AddressDto>(User.Address);
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;
            }

            else //Create new Address
            {

                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            ///Update User
            ///Return Address
            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //check if email Exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new UserNotFoundException(loginDto.Email);
            //Check Password
            var IsPasswordVaild = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordVaild)
            {

                //Return UserDto

                return new UserDto
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    //ToDo
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Map from RegisterDto to Application User

            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username,

            };
            //Create user fro type Application user
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
            {

                //if user created return UserDTo
                return new UserDto
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    //ToDo
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {

                //throw Exception

                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

    

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
          {
              new Claim(ClaimTypes.Email, user.Email!), 
              new (ClaimTypes.Name, user.UserName!), 
              new (ClaimTypes.NameIdentifier, user.Id!), 

          };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in  Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            var SecretKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));  
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtOptions")["Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds            
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
