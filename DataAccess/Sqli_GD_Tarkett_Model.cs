namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Sqli_GD_Tarkett_Model : DbContext
    {
        public Sqli_GD_Tarkett_Model()
            : base("name=Sqli_GD_Tarkett_Model")
        {
        }

        public virtual DbSet<Colaborateur> Colaborateur { get; set; }
        public virtual DbSet<DemandeVisa> DemandeVisa { get; set; }
        public virtual DbSet<Deplacement> Deplacement { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Cin)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Poste)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Equipe)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Sexe);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Colaborateur>()
                .HasMany(e => e.DemandeVisa)
                .WithOptional(e => e.Colaborateur)
                .HasForeignKey(e => e.IdC);

            modelBuilder.Entity<Colaborateur>()
                .HasMany(e => e.Deplacement)
                .WithOptional(e => e.Colaborateur)
                .HasForeignKey(e => e.IdC);

            modelBuilder.Entity<DemandeVisa>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<DemandeVisa>()
                .Property(e => e.Observation)
                .IsUnicode(false);

            modelBuilder.Entity<Deplacement>()
                .Property(e => e.Observation)
                .IsUnicode(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
