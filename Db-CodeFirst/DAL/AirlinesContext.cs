using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Db_CodeFirst.DAL
{
    public partial class AirlinesContext : DbContext
    {
        public AirlinesContext()
        {
        }

        public AirlinesContext(DbContextOptions<AirlinesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PassInTrip> PassInTrips { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompId)
                    .HasName("PK__Company__AD362A166A53B048");

                entity.ToTable("Company");

                entity.Property(e => e.CompId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PassInTrip>(entity =>
            {
                entity.HasKey(e => new { e.TripNo, e.Date, e.PassId })
                    .HasName("PK__PassInTr__8F69BB1BC62AA9BE");

                entity.ToTable("PassInTrip");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Pass)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.PassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PassInTri__PassI__3D5E1FD2");

                entity.HasOne(d => d.TripNoNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.TripNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PassInTri__TripN__3C69FB99");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.PassId)
                    .HasName("PK__Passenge__C6740AA8404F2874");

                entity.ToTable("Passenger");

                entity.Property(e => e.PassId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.TripNo)
                    .HasName("PK__Trip__51DC48C144A84121");

                entity.ToTable("Trip");

                entity.Property(e => e.TripNo).ValueGeneratedNever();

                entity.Property(e => e.Plane)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TownFrom)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TownTo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trip__CompId__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
