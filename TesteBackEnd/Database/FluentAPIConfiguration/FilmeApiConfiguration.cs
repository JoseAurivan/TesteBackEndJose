using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class FilmeApiConfiguration:IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasOne(c => c.Locacao)
                .WithOne(c => c.Filme)
                .HasForeignKey<Locacao>(c => c.FilmeId);
        }
    }
}