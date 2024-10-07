using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager )
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Shimaa Hamdy",
                    Email = "shimaahamdy864@gmail.com",
                    UserName = "shimaahamdy" , 
                    Address = new Address
                    {
                        FirstName = "Shimaa", 
                        LastName = "Hamdy",
                        City = "October" , 
                        State = "Giza" , 
                        PostalCode = "246",
                        Street = "2"
                    }
                }; 

                await userManager.CreateAsync(user , "Password123!");
            }

        }

    }
}
