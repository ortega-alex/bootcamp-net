using System.Security.Claims;
using UniversitiApiBackend.Models.DataModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace UniversitiApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokents userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            if (userAccounts.UserName == "Admin")
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            else if (userAccounts.UserName == "User1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;  
        }

        public static IEnumerable<Claim> GetClaims(this UserTokents userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokents GenTokenKey(UserTokents model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokents();
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                // Obtain SECRET key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuingSigningKey);
                Guid Id;
                // Expires in 1 day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                // Validity of out token
                userToken.Validity = expireTime.TimeOfDay;

                // Generate our jwt
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                userToken.Tocken = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                return userToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating token", ex);
            }
        }
    }
}
