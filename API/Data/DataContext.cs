using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options){

        }

        public DbSet<AppUser> Users{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    
    modelBuilder.Entity<AppUser>().HasData(
    new AppUser
    {
        Id = 1,
        UserName = "Thrisha"
    },
    new AppUser
    {
        Id = 2,
        UserName = "Deeksha"
    }
    );
}
    }
}