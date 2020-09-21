using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Service
{
  public static class TokkenService
  {
    public static string GenerateTokken(Usuario usuario)
    {

      var tokkenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(Settings.Secret);
      var tokkenDEscription = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]{
           new Claim(ClaimTypes.Name , usuario.id.ToString()),
           new Claim(ClaimTypes.NameIdentifier , usuario.id_cliente.ToString()),
          //  new Claim(ClaimTypes.Name , usuario.id.ToString())
         }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var Tokken = tokkenHandler.CreateToken(tokkenDEscription);
      return tokkenHandler.WriteToken(Tokken);
    }
  }
}
