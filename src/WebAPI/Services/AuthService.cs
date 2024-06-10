using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BlogApp.Infrastructure.Data;
using BlogApp.Infrastructure.Identity;
using BlogApp.WebAPI.Contracts;
using BlogApp.WebAPI.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.WebAPI.Services;

public class AuthService(
    IOptions<JwtConfig> config,
    ApplicationDbContext context) : IAuthService
{
    public bool Authorize(string username, string password)
    {
        var user = context.Users.FirstOrDefault(user => user.UserName == username && user.PasswordHash == password);
        return (user is not null);
    }

    public string GenerateToken(string username)
    {
        var issuer = config.Value.Issuer;
        var audience = config.Value.Audience;
        var key = Encoding.ASCII.GetBytes(config.Value.Key);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()), new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config.Value.Key);

        try
        {
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var expirationDate = jwtToken.ValidTo;

            return expirationDate >= DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}
