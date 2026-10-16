using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Mapping
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turma");
            
            builder.HasKey(t => t.Id);
            
            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired();

            builder.HasOne(x => x.Curso)
                .WithMany(x => x.Turmas)
                .HasForeignKey("CursoId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
