using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Infra.Data.Configurations;

public class VilaoConfiguration : IEntityTypeConfiguration<Vilao>
{
    public void Configure(EntityTypeBuilder<Vilao> builder)
    {
        builder.ToTable("viloes");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasColumnName("id");

        builder.Property(v => v.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Franquia)
            .HasColumnName("franquia")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Ator)
            .HasColumnName("ator")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(v => v.Ano).HasColumnName("ano");

        // O SQLite grava decimal como TEXT; convertido para manter a coluna REAL do banco original.
        builder.Property(v => v.Maldade)
            .HasColumnName("maldade")
            .HasConversion<double>();
    }
}
