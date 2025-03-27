using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public virtual DbSet<ActivityDescription> ActivityDescriptions { get; set; }

        public virtual DbSet<Activity> Activitys { get; set; }

        public virtual DbSet<Entry> Entries { get; set; }

        public virtual DbSet<Klasse> Klasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityDescription>(entity =>
            {
                entity.ToTable("ActivityDescription");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User).WithMany(p => p.ActivityDescriptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDescription_AspNetUsers");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("User_Id");

                entity.HasOne(d => d.User).WithMany(p => p.ActivityTitles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)  
                    .HasConstraintName("FK_ActivityTitle_AspNetUsers");
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.ToTable("Entry");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.End).HasColumnType("datetime");
                entity.Property(e => e.Start).HasColumnType("datetime");
                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("User_id");

                entity.HasOne(d => d.User).WithMany(p => p.Entries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entry_AspNetUsers");
            });

            modelBuilder.Entity<Klasse>(entity =>
            {
                entity.ToTable("Klasse");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(250);
                entity.Property(e => e.StartJahr).HasColumnName("Start_Jahr");
            });
        }
    }
}
