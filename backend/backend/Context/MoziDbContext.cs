using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class MoziDbContext : DbContext
    {
        public MoziDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Film> Filmek { get; set; }
        public DbSet<Terem> Termek { get; set; }
        public DbSet<Vetites> Vetitesek { get; set; }
        public DbSet<Szek> Szekek { get; set; }
        public DbSet<Foglalas> Foglalasok { get; set; }
        public DbSet<Foglalthely> Foglalthelyek { get; set; }
        public DbSet<Felhasznalo> Felhasznalok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vetites>()
                .HasOne(v => v.Film)
                .WithMany(f => f.Vetitesek)
                .HasForeignKey(v => v.FilmId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vetites>()
                .HasOne(v => v.Terem)
                .WithMany(t => t.Vetitesek)
                .HasForeignKey(v => v.TeremId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Szek>()
                .HasOne(s => s.Terem)
                .WithMany(t => t.Szekek)
                .HasForeignKey(s => s.TeremId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Foglalthely>()
                .HasOne(fh => fh.Szek)
                .WithMany(s => s.Foglalthely)
                .HasForeignKey(fh => fh.SzekId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Foglalthely>()
                .HasOne(fh => fh.Vetites)
                .WithMany(v => v.Foglalthely)
                .HasForeignKey(fh => fh.VetitesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Foglalthely>()
               .HasOne(fh => fh.Foglalas)
               .WithMany(f => f.Foglalthely)
               .HasForeignKey(fh => fh.FoglalasId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Foglalas>()
               .HasOne(f => f.Felhasznalo)
               .WithMany(u => u.Foglalasok)
               .HasForeignKey(f => f.FelhasznaloId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Vetites>()
                .HasIndex(v => v.Idopont)
                .HasDatabaseName("IX_Vetites_Idopont");

            modelBuilder.Entity<Foglalthely>()
                .HasIndex(fh => new { fh.VetitesId, fh.SzekId })
                .IsUnique()
                .HasDatabaseName("IX_Foglalthely_Vetites_Szek_Unique");

            modelBuilder.Entity<Foglalas>()
                .HasIndex(f => f.FelhasznaloId)
                .HasDatabaseName("IX_Foglalas_FelhasznaloId");

            modelBuilder.Entity<Film>()
                .HasIndex(f => f.Cim)
                .HasDatabaseName("IX_Film_Cim");


            modelBuilder.Entity<Vetites>()
                .HasIndex(v => new { v.FilmId, v.Idopont })
                .HasDatabaseName("IX_Vetites_FilmId_Idopont");

            modelBuilder.Entity<Vetites>()
                .HasIndex(v => new { v.TeremId, v.Idopont })
                .HasDatabaseName("IX_Vetites_TeremId_Idopont");
            modelBuilder.Entity<Felhasznalo>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Felhasznalo_Email_Unique");

            modelBuilder.Entity<Foglalthely>()
                .HasOne(fh => fh.Foglalas)
                .WithMany(f => f.Foglalthely)
                .HasForeignKey(fh => fh.FoglalasId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
