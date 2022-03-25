using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ServiceApp.Models;

namespace ServiceApp.Models
{
    public class DatabaseContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "server=alexkuznetsov.database.windows.net;user=sashabelaz7;password=zse3rfvWWW;database=ITRBook");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<Article>()
                .Property<string>("TagsCollection")
                .HasField("TagsCollection");

            base.OnModelCreating(modelBuilder);
        }

    }
}
