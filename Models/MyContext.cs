using Microsoft.EntityFrameworkCore;

namespace Hobbies.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Hobby> Hobbies {get;set;}
        public DbSet<Association> Associations {get;set;}

    }
}