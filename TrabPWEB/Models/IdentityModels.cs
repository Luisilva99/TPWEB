using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TrabPWEB.DAL;

namespace TrabPWEB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Region> Regions { get; set; }
        public DbSet<Local> Locals { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<TimeData> TimeDatas { get; set; }
        public DbSet<StationPost> StationPosts { get; set; }
        public DbSet<RechargeMod> RechargeMods { get; set; }
        public DbSet<RechargeType> RechargeTypes { get; set; }

        public DbSet<TimeAtribuition> TimeAtribuitions { get; set; }
    }
}