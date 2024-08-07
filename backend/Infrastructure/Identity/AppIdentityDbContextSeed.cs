using Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Tuan Pham",
                    Email = "tuanpnt17@gmail.com",
                    UserName = "tuanpnt17",
                    Address = new Address
                    {
                        FirstName = "Tuan",
                        LastName = "Pham",
                        Street = "671 Le Van Viet",
                        City = "Thu Duc",
                        State = "Ho Chi Minh",
                        ZipCode = "70000"
                    }
                };

                await userManager.CreateAsync(user, "TuanPNT@15743");
            }
        }
    }
}
