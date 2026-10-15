using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Infra.Data.Configurations;

public class AparicaoConfiguration : IEntityTypeConfiguration<Aparicao>
{
    public void Configure(EntityTypeBuilder<Aparicao> builder)
    {
        builder.ToTable("aparicoes");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("id");

        builder.Property(a => a.VilaoId).HasColumnName("vilao_id");

        builder.Property(a => a.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(a => a.Ano).HasColumnName("ano");

        builder.Property(a => a.Temporada).HasColumnName("temporada");

        builder.HasOne<Vilao>()
            .WithMany(v => v.Aparicoes)
            .HasForeignKey(a => a.VilaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
