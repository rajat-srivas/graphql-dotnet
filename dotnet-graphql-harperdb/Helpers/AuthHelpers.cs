using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using dotnet_graphql_harperdb.Data;
using Microsoft.Extensions.Configuration;

namespace dotnet_graphql_harperdb.Helpers
{
	public class AuthHelpers
	{
		readonly IConfiguration _config;

		public AuthHelpers(IConfiguration config)
		{
			_config = config;
		}
		public string GenerateJWT(User userInfo)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			List<Claim> userClaims = new()
			{
				new Claim(ClaimTypes.Name, userInfo.Email),
				new Claim("userId", userInfo.UserId.ToString()),
				new Claim(ClaimTypes.Role, userInfo.UserRole),
			};

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: userClaims,
				expires: DateTime.Now.AddHours(24),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
