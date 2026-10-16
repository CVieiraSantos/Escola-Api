using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Mapping
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matricula");

            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x => x.UsuarioId)
            .IsRequired();

            builder.Property(x => x.TurmaId)
            .IsRequired();

            builder.HasOne(x => x.Usuario)
            .WithMany(x=> x.Matriculas)
            .HasForeignKey(x => x.UsuarioId)
            .HasConstraintName("FK_Matricula_Usuario")
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Turma)
            .WithMany(x=> x.Matriculas)
            .HasForeignKey(x => x.TurmaId)
            .HasConstraintName("FK_Matricula_Turma")
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
