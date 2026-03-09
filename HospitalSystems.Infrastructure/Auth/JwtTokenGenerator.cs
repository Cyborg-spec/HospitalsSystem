using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalSystems.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HospitalSystems.Infrastructure.Auth;

public class JwtTokenGenerator:IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    // We inject the strongly-typed settings using IOptions<T>!
    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }
    public TokenResponse GenerateToken(User user)
    {
        // 1. Convert our Secret string into a cryptographic byte array key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        
        // 2. We use HMAC SHA256 to sign the token
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // 3. Define the minimal Claims. This tells ASP.NET *who* this is and their *Role*
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role.ToString()) 
        };
        // 4. Create the actual token object payload
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        var accessToken = tokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();
        return new TokenResponse(
            accessToken, 
            refreshToken, 
            tokenDescriptor.Expires.Value
        );
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}