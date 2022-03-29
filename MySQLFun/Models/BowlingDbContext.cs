using System;
using Microsoft.EntityFrameworkCore;

namespace MySQLFun.Models
{
    public class BowlingDbContext: DbContext
    {
        public BowlingDbContext(DbContextOptions<BowlingDbContext>options): base(options)
        {

        }
        
        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
