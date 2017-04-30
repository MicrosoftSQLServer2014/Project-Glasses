using System.Data.Entity;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
    class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ClientProfile> ClientProfiles;


        public ApplicationContext(string connectionString) : base(connectionString)
        {

        }
    }
}
