using Microsoft.EntityFrameworkCore;
using ContrRendaFixa.Models;

namespace ContrRendaFixa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TipoProdutoModel> TiposProdutos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ContratanteModel> Contratantes { get; set; }
        public DbSet<ContratacaoModel> Contratacoes { get; set; }
        public DbSet<PagamentoModel> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoProdutoModel>()
                .Property(tp => tp.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ProdutoModel>()
                .Property(p => p.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ProdutoModel>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            modelBuilder.Entity<ProdutoModel>()
                .HasOne(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.TipoId);

            modelBuilder.Entity<ContratanteModel>()
                .Property(c => c.Nome)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<ContratanteModel>()
                .Property(c => c.Sobrenome)
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<ContratanteModel>()
                .Property(c => c.Segmento)
                .HasConversion(
                    v => v.ToString(),
                    v => (SegmentoEnum)Enum.Parse(typeof(SegmentoEnum), v))
                .HasColumnType("text")
                .HasComment("V = Varejo, A = Atacado, E = Especial")
                .IsRequired();

            modelBuilder.Entity<ContratacaoModel>()
                .Property(c => c.PrecoUnitario)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            modelBuilder.Entity<PagamentoModel>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        }
    }
}
