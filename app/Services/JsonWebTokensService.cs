using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace app.Services;

public class JsonWebTokensService
{
    private readonly IConfiguration _configuration;
    public JsonWebTokensService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(ClaimsIdentity ClaimsParaToken)
    {
        var informacoesParaToken = BuscarInformacoesDoToken();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ClaimsParaToken,
            SigningCredentials = informacoesParaToken.SigningCredentials,
            Audience = informacoesParaToken.Audience,
            Issuer = informacoesParaToken.Issuer,
            Expires = informacoesParaToken.Expires
        };

        return GerarTokenString(tokenDescriptor);
    }

    private (SigningCredentials SigningCredentials, string Audience, string Issuer, DateTime Expires) BuscarInformacoesDoToken()
    {
        var signingCredentials = GerarCredenciais(_configuration["JwtBearerTokenSettings:SecretKey"] ?? "SecretKey not found");
        double lifeTimeInMinutes = double.Parse(_configuration["JwtBearerTokenSettings:ExpiryTimeInMinutes"] ?? "ExpiryTimeInMinutes key not found");
        string audience = _configuration["JwtBearerTokenSettings:Audience"] ?? "Audience not found";
        string issuer = _configuration["JwtBearerTokenSettings:Issuer"] ?? "Issuer not found";
        DateTime expires = DateTime.UtcNow.AddMinutes(lifeTimeInMinutes);

        return (signingCredentials, audience, issuer, expires);
    }

    private SigningCredentials GerarCredenciais(string secretKey)
    {
        byte[] key = Encoding.UTF8.GetBytes(secretKey);
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        return signingCredentials;
    }

    private string GerarTokenString(SecurityTokenDescriptor descriptor)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        var pretoken = TokenHandler.CreateToken(descriptor);
        return TokenHandler.WriteToken(pretoken);
    }

    private string? CleanUpToken(string? token)
    {
        token = Regex.Replace(token, "bearer ", "", RegexOptions.IgnoreCase);
        return token;
    }
}