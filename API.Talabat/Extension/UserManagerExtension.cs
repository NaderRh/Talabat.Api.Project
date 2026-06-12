using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Security_Module;

namespace API.Talabat.Extension
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
           var email=User.FindFirstValue(ClaimTypes.Email);
           var user=await userManager.Users.Include(P=>P.Address).SingleOrDefaultAsync(P=>P.Email==email);
           return user;
        }
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
