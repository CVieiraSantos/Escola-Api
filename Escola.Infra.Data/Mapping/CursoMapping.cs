using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Mapping
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x=> x.Nome)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(x => x.Descricao)
            .HasMaxLength(150);

            builder.HasIndex(x => x.Nome, "IX_Curso_Nome")
            .IsUnique();

        }
    }
}
