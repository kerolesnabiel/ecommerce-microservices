using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UserService.Domain.Entities;
namespace UserService.Application.Services;

public class JwtService
{
    private readonly IConfiguration configuration;
    private readonly RSA privateKey;

    public JwtService(IConfiguration config)
    {
        configuration = config;
        privateKey = LoadPrivateKey(configuration["JwtSettings:PrivateKeyPath"]!);
    }

    private RSA LoadPrivateKey(string path)
    {
        var rsa = RSA.Create();
        rsa.ImportFromPem(File.ReadAllText(path));
        return rsa;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new RsaSecurityKey(privateKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["JwtSettings:TokenExpiryMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetTokenExpiryMinutes() =>
        int.Parse(configuration["JwtSettings:TokenExpiryMinutes"]!);
}
