using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();
           
            builder.Property(x => x.PasswordHash).IsRequired();
            
            builder.Property(x => x.PasswordSalt).IsRequired();

            builder.Property(x => x.Perfil)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
