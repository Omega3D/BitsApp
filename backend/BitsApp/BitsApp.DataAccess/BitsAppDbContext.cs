using BitsApp.DataAccess.Configurations;
using BitsApp.DataAccess.Enitites;
using Microsoft.EntityFrameworkCore;

namespace BitsApp.DataAccess
{
    public class BitsAppDbContext : DbContext
    {
        public BitsAppDbContext(DbContextOptions<BitsAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }

        public DbSet<PersonEntity> Persons {  get; set; } 
    }
}
