using Microsoft.EntityFrameworkCore;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Infra.Data;

public class ViloesDbContext(DbContextOptions<ViloesDbContext> options) : DbContext(options)
{
    public DbSet<Vilao> Viloes => Set<Vilao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ViloesDbContext).Assembly);
}
