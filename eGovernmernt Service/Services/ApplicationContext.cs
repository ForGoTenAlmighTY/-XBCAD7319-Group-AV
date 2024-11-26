using eGovernmernt_Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eGovernmernt_Service.Services
{
    public class ApplicationContext: IdentityDbContext<AppUser>
    {
        public ApplicationContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var depthome = new IdentityRole("Home Affairs");
            depthome.NormalizedName = "Home Affairs";

            var depttraffic = new IdentityRole("Traffic");
            depttraffic.NormalizedName = "Traffic";

            var depthealth = new IdentityRole("Health");
            depthealth.NormalizedName = "Health";

            var deptsocial = new IdentityRole("Social Development");
            deptsocial.NormalizedName = "Social Development";

            builder.Entity<IdentityRole>().HasData(depthome, depttraffic, depthealth, deptsocial);
            builder.Entity<HomeAffairsApplication>().HasNoKey();
            builder.Entity<HomeAffairsAppointment>().HasNoKey();
            builder.Entity<HealthAppointment>().HasNoKey();
            builder.Entity<HealthApplication>().HasNoKey();
            builder.Entity<TrafficRegistrationApplication>().HasNoKey();
            builder.Entity<TrafficLicenceApplication>().HasNoKey();
            builder.Entity<SocialAppointment>().HasNoKey();
            builder.Entity<SocialApplication>().HasNoKey(); 
            builder.Entity<TrafficAppointment>().HasNoKey();

        }
        public DbSet<HealthAppointment> HealthAppointment { get; set; }
        public DbSet<HealthApplication> HealthApplication { get; set; }
        public DbSet<TrafficRegistrationApplication> TrafficRegistrationApplication { get; set; }
        public DbSet<TrafficLicenceApplication> TrafficLicenceApplication { get; set; }
        public DbSet<TrafficAppointment> TrafficAppointment { get; set; }
        public DbSet<SocialAppointment> SocialAppointment { get; set; } 
        public DbSet<SocialApplication> SocialApplication { get; set; }
        public DbSet<HomeAffairsApplication> HomeAffairsApplication { get; set; }
        public DbSet<HomeAffairsAppointment> HomeAffairsAppointment { get; set; }

    }
}
