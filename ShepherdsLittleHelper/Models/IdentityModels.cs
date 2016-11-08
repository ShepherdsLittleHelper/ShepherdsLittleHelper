using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ShepherdsLittleHelper.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Group> Groups { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(up => up.Users)
                .WithMany(course => course.Groups)
                .Map(mc =>
                {
                    mc.ToTable("T_Group_User");
                    mc.MapLeftKey("GroupID");
                    mc.MapRightKey("UserID");
                }
            );
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.Group> Groups { get; set; }

        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.TaskType> TaskTypes { get; set; }

        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.PetType> PetTypes { get; set; }

        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.Pet> Pets { get; set; }

        public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.PetTask> PetTasks { get; set; }

        //public System.Data.Entity.DbSet<ShepherdsLittleHelper.Models.User> Users { get; set; }
    }
}