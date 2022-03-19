using Ascensor.WebAPI.DTO.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ascensor.WebAPI.Data.Context
{
    public class DBAscensorContext : DbContext
    {
        public DBAscensorContext(DbContextOptions<DBAscensorContext> options) : base(options) { }

        public DbSet<AscensorEntity> Ascensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AscensorEntity>().ToTable("tb_Ascensor").HasKey(x => x.Asce_Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}