using ActivityCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityCenter
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<MainUser> Users {get; set;}
        public DbSet<Activity> Activities { get; set; }
        public DbSet<JoinActivity> JoinActivities { get; set; }
    }

}
