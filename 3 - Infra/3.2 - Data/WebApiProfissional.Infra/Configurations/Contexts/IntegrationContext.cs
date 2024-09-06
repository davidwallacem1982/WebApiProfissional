using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace WebApiProfissional.Infra.Configurations.Contexts
{
    /// <summary>
    /// Representa o contexto do banco de dados para a aplicação de integração, responsável pela configuração
    /// das entidades e suas relações, além de fornecer a interface para interagir com o banco de dados.
    /// </summary>
    public class IntegrationContext : DbContext
    {
        /// <summary>
        /// Construtor que inicializa o contexto do banco de dados com as opções fornecidas.
        /// </summary>
        /// <param name="options">Opções de configuração do contexto.</param>
        public IntegrationContext(DbContextOptions<IntegrationContext> options) : base(options) { }

#if DEBUG
        /// <summary>
        /// LoggerFactory estático para capturar e exibir os logs de comandos SQL enviados ao banco de dados,
        /// usado para depuração em ambientes de desenvolvimento.
        /// </summary>
        public static readonly LoggerFactory _myLoggerFactory = new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });

        /// <summary>
        /// Configurações adicionais aplicadas ao contexto, como o uso de LoggerFactory para registrar os comandos SQL executados.
        /// </summary>
        /// <param name="optionsBuilder">Construtor de opções de configuração do contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
#endif

        /// <summary>
        /// Configurações específicas das entidades, mapeando suas propriedades e definindo os relacionamentos
        /// e comportamentos específicos de cada entidade.
        /// </summary>
        /// <param name="modelBuilder">Construtor de modelos que define as configurações de mapeamento das entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a collation e charset padrão para o banco de dados
            modelBuilder.UseCollation("utf8mb3_general_ci").HasCharSet("utf8mb3");

            // Configurações específicas para a entidade Enderecos
            modelBuilder.Entity<Enderecos>(entity =>
            {
                entity.Property(e => e.Uf).IsFixedLength();
            });

            // Configurações específicas para a entidade Funcionarios e seus relacionamentos
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

            // Configurações específicas para a entidade Logeventsintegration
            modelBuilder.Entity<Logeventsintegration>(entity =>
            {
                entity.Property(e => e.Ts).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configurações específicas para a entidade RefreshTokens e seus relacionamentos
            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_RefreshTokens_Usuarios");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            });

            // Configurações específicas para a entidade RevokedTokens e seus relacionamentos
            modelBuilder.Entity<RevokedTokens>(entity =>
            {
                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.RevokedTokens)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_RevokedTokens_Usuarios");

                entity.Property(e => e.RevokedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configurações específicas para a entidade Telefones e seus relacionamentos
            modelBuilder.Entity<Telefones>(entity =>
            {
                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.Telefones)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Telefones_Enderecos");
            });

            // Configurações específicas para a entidade Usuarios e seus relacionamentos
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Usuarios_Funcionarios");
            });
        }

        // Definição das DbSets que representam as tabelas no banco de dados

        /// <summary>
        /// Conjunto de entidades da tabela Autarquiafederal.
        /// </summary>
        public virtual DbSet<Autarquiafederal> Autarquiafederal { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela Enderecos.
        /// </summary>
        public virtual DbSet<Enderecos> Enderecos { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela Funcionarios.
        /// </summary>
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela Logeventsintegration.
        /// </summary>
        public virtual DbSet<Logeventsintegration> Logeventsintegration { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela RefreshTokens.
        /// </summary>
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela RevokedTokens.
        /// </summary>
        public virtual DbSet<RevokedTokens> RevokedTokens { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela Telefones.
        /// </summary>
        public virtual DbSet<Telefones> Telefones { get; set; }

        /// <summary>
        /// Conjunto de entidades da tabela Usuarios.
        /// </summary>
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }

}
