using Authentication.Net.Application.Interfaces;
using Authentication.Net.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Net.Application.Providers.JWT
{
	public class JwtProvider : IJwtProvider
	{
		private readonly string _issuer = "AuthenticationAPI";
		private readonly string _audience = "User";
		private readonly TokenValidationParameters _validationParameters;
		private readonly SymmetricSecurityKey _symmetricSecurityKey = new(Encoding.ASCII.GetBytes("chave-secreta-com-mais-de-256-bits"));

		public JwtProvider()
		{

			_validationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _symmetricSecurityKey,
				ValidIssuer = _issuer,
				ValidAudience = _audience
			};

		}
		public string GenerateJwt(string userEmail)
		{
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Email, userEmail)
				}),
				Issuer = _issuer,
				Audience = _audience,
				NotBefore = DateTime.Now,
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
			};
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
		}

		public string DecodeJwt(string jwt)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				var claimsPrincipal = tokenHandler.ValidateToken(jwt, _validationParameters, out _);
				var payload = claimsPrincipal.Claims;
				return payload.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;
			}
			catch (Exception ex)
			{
				throw new InternalErrorException("Critical Error, contact support.");
            }
		}

		public bool IsTokenValid(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				tokenHandler.ValidateToken(token, _validationParameters, out _);
                return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
