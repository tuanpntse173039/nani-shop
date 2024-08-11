using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser?> FindUserByEmailClaimsPrincipalWithAddressAsync(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user
        )
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager
                .Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser?> FindUserByEmailClaimsPrincipalAsync(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user
        )
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager.FindByEmailAsync(email ?? "");
        }
    }
}
