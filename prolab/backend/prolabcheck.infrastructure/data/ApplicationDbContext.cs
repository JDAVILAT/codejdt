using Microsoft.EntityFrameworkCore;
using prolabcheck.domain.DTOs;
using prolabcheck.domain.entities;

namespace prolabcheck.infrastructure.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<tb_productos> tb_productos { get; set; }

        public DbSet<SearchMedicineDTO> SearchMedicineDTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tb_productos>().HasKey(p => p.NU_ID_PRODUCTO);
            modelBuilder.Entity<SearchMedicineDTO>().HasNoKey();
        }
    }
}
