using AmazingFileVersionControl.Core.Contexts.EntityConfigurations;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AmazingFileVersionControl.Core.Contexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        //public DbSet<ProfileEntity> Profiles { get; set; }

        public UserDbContext()
        {
            Database.EnsureCreated();
        }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        }
    }
}
