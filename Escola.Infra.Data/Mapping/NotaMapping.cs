using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Mapping
{
    public class NotaMapping : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.ToTable("Nota");

            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x => x.MatriculaId)
            .IsRequired();

            builder.Property(x => x.ValorNota)
            .IsRequired();

            builder.HasOne(x => x.Matricula)
            .WithMany(x=> x.Notas)
            .HasForeignKey(x => x.MatriculaId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
