using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace WebApiProfissional.Infra.Configurations.Contexts
{
    /// <summary>
    /// Construtor estruturado para receber "Configuration.GetConnectionString" no Startup
    /// </summary>
    /// <param name="options"></param>

    public class IntegrationContext : DbContext
    {
        public IntegrationContext(DbContextOptions<IntegrationContext> options) : base(options){}

#if DEBUG
        /// <summary>
        /// Variavel static criada para utilizar o LoggerFactory
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static readonly LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        });
#endif

        public virtual DbSet<Autarquiafederal> Autarquiafederal { get; set; }
        public virtual DbSet<Enderecos> Enderecos { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<Logeventsintegration> Logeventsintegration { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<RevokedTokens> RevokedTokens { get; set; }
        public virtual DbSet<Telefones> Telefones { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

#if DEBUG
        /// <summary>
        /// Adicionando o Logger no Builder para retonar os comando enviado para o Banco de Dados.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Enderecos>(entity =>
            {
                entity.Property(e => e.Uf).IsFixedLength();
            });

            modelBuilder.Entity<Funcionarios>(entity =>
            {
                entity.HasOne(d => d.IdAutarquiaFederalNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdAutarquiaFederal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Funcionarios_AutarquiaFederal");

                entity.HasOne(d => d.IdEnderecosNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdEnderecos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Funcionarios_Enderecos");
            });

            modelBuilder.Entity<Logeventsintegration>(entity =>
            {
                entity.Property(e => e.Ts).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_RefreshTokens_Usuarios");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<RevokedTokens>(entity =>
            {
                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.RevokedTokens)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_RevokedTokens_Usuarios");
                entity.Property(e => e.RevokedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Telefones>(entity =>
            {
                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.Telefones)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Telefones_Enderecos");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Usuarios_Funcionarios");
            });
        }
    }
}
