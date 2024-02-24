using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;
using System.Collections.Generic;

namespace MyMvcApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LinkType> LinkTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<Badges> Badges { get; set; }
    }
}
