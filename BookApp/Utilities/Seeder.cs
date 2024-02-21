using BookApp.DataLayer;
using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Utilities
{
    public class Seeder
    {
        public static void SeedRoles (AppDatabaseContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        RoleName = "Normaluser"
                    },
                    new Role
                    {
                        RoleName = "Author"
                    }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }          
        }       
    }
}
