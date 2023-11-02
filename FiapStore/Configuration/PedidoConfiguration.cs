using FiapStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapStore.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnType("INT")
                .UseIdentityColumn();

            builder
                .Property(u => u.NomeProduto)
                .HasColumnType("VARCHAR(100)");

            builder
                .HasOne(u => u.Usuario)
                .WithMany(p => p.Pedidos)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
