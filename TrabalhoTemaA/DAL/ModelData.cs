namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelData : DbContext
    {
        public ModelData()
            : base("name=ModelData")
        {
        }

        public virtual DbSet<AdminTable> AdminTable { get; set; }
        public virtual DbSet<ClienteTable> ClienteTable { get; set; }
        public virtual DbSet<EstacaoTable> EstacaoTable { get; set; }
        public virtual DbSet<HoraTable> HoraTable { get; set; }
        public virtual DbSet<PostoTable> PostoTable { get; set; }
        public virtual DbSet<RedeTable> RedeTable { get; set; }
        public virtual DbSet<ReservaTable> ReservaTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminTable>()
                .Property(e => e.UserNameAdmin)
                .IsFixedLength();

            modelBuilder.Entity<AdminTable>()
                .Property(e => e.PasswordAdmin)
                .IsFixedLength();

            modelBuilder.Entity<AdminTable>()
                .Property(e => e.PasswordConfirmation)
                .IsFixedLength();

            modelBuilder.Entity<ClienteTable>()
                .Property(e => e.NomeCliente)
                .IsFixedLength();

            modelBuilder.Entity<ClienteTable>()
                .Property(e => e.PasswordCliente)
                .IsFixedLength();

            modelBuilder.Entity<ClienteTable>()
                .HasMany(e => e.ReservaTable)
                .WithRequired(e => e.ClienteTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstacaoTable>()
                .Property(e => e.NomeEstacao)
                .IsFixedLength();

            modelBuilder.Entity<EstacaoTable>()
                .HasMany(e => e.PostoTable)
                .WithRequired(e => e.EstacaoTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PostoTable>()
                .HasMany(e => e.HoraTable)
                .WithRequired(e => e.PostoTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PostoTable>()
                .HasMany(e => e.ReservaTable)
                .WithRequired(e => e.PostoTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RedeTable>()
                .Property(e => e.NomeRede)
                .IsFixedLength();

            modelBuilder.Entity<RedeTable>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<RedeTable>()
                .Property(e => e.PasswordClienteRede)
                .IsFixedLength();
        }
    }
}
