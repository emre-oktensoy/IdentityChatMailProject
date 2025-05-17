using IdentityChatMailProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityChatMailProject.Context
{
    public class IdentityMailContext: IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=LAPTOP-4RNLMJVN; initial Catalog = IdentityMailProjectDB;integrated security = true;trust server certificate=true");

        }

        public DbSet<Message> Messages { get; set; }
    }
}
