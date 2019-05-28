using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace InternetShop.WEB.Models
{
    //public class UserTokenProvider : IUserTwoFactorTokenProvider<User>
    //{
    //    public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
    //    {
    //        if (purpose == "login")
    //        {
    //            var now = DateTime.Now;
    //            var jwt = new JwtSecurityToken(
    //                issuer: AuthOptions.Issuer,
    //                audience: AuthOptions.Audience,
    //                notBefore: now,
    //                claims: await manager.GetClaimsAsync(user),// claim.Claims,
    //                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
    //                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),

    //                    SecurityAlgorithms.HmacSha256)
    //            );
    //            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    //            return encodedJwt;
    //        }
    //        //if (user == null)
    //        //{
    //        //    throw new ArgumentNullException(nameof(user));
    //        //}
    //        //var ms = new MemoryStream();
    //        //var userId = await manager.GetUserIdAsync(user);
    //        //using (var writer = ms.CreateWriter())
    //        //{
    //        //    writer.Write(DateTimeOffset.UtcNow);
    //        //    writer.Write(userId);
    //        //    writer.Write(purpose ?? "");
    //        //    string stamp = null;
    //        //    if (manager.SupportsUserSecurityStamp)
    //        //    {
    //        //        stamp = await manager.GetSecurityStampAsync(user);
    //        //    }
    //        //    writer.Write(stamp ?? "");
    //        //}
    //        //var protectedBytes = Protector.Protect(ms.ToArray());
    //        //return Convert.ToBase64String(protectedBytes);
    //    }

    //    public Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
    //    {
    //        manager.VerifyTwoFactorTokenAsync(user,)
    //    }

    //    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
