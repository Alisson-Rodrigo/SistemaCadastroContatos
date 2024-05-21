using SistemaDeCadastro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaDeCadastro.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
        }
    }
}