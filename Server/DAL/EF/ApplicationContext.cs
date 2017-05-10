using System.Data.Entity;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<GlassesEntity> Glasses { get; set; }
        public DbSet<ModeEntity> Modes { get; set; }
        public DbSet<StatisticEntity> Statistic { get; set; }

        public ApplicationContext(string connectionString) : base(connectionString)
        {

        }
    }
}
