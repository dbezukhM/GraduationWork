﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Contracts;
using BLL.Results;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _apiUrl;

        private readonly UserManager<Person> _userManager;

        public TokenGenerator(string apiUrl, UserManager<Person> userManager)
        {
            _apiUrl = apiUrl;
            _userManager = userManager;
        }

        public async Task<Result<string>> GetToken(Person person)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _apiUrl,
                audience: _apiUrl,
                claims: await GetClaims(person),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Result.Success(tokenString);
        }

        private async Task<IEnumerable<Claim>> GetClaims(Person person)
        {
            var roles = await _userManager.GetRolesAsync(person);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, person.Email)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}