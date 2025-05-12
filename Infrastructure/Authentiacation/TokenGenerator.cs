using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Authentication;
using Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentiacation;

public class TokenGenerator: ITokenGenerator
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "NazarNazarNazarNazarNazarNazarNazarNazar"u8.ToArray();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.FullName),
            new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
            new(JwtRegisteredClaimNames.Birthdate, user.BirthDate.ToString("dd/MM/yyyy")),
            new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new("role", user.Role.Title),
            new(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "https://localhost:5094",
            Audience = "https://localhost:5094",
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}