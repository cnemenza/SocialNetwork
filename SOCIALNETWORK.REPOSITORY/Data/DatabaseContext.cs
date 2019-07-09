using SOCIALNETWORK.CORE;
using SOCIALNETWORK.ENTITIES.Models;
using System.Data.Entity;

namespace SOCIALNETWORK.REPOSITORY.Data
{
    public class DatabaseContext : DbContext 
    {
        //public DatabaseContext() :base("SocialNetworkConnectionString") { }
        public DatabaseContext() :base(ConstantsHelpers.ConnectionString){ }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<StudyCenter> StudyCenters { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Forum> Forums { get; set; }
    }
}
