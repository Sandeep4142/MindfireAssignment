using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUDUsingDotNetCore.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=SANDEEPH-WIN10;Database=School;User Id=sa;Password=mindfire; Trust Server Certificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassNo).HasName("PK__Class__75771CE589C2B799");

            entity.ToTable("Class");

            entity.Property(e => e.ClassNo).ValueGeneratedNever();
            entity.Property(e => e.ClassTeacher)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Students).WithMany(p => p.ClassNos)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__ClassStud__stude__403A8C7D"),
                    l => l.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ClassStud__class__3F466844"),
                    j =>
                    {
                        j.HasKey("ClassNo", "StudentId").HasName("PK__ClassStu__A1A60186802D5F40");
                        j.ToTable("ClassStudent");
                    });

            entity.HasMany(d => d.Subjects).WithMany(p => p.ClassNos)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassSubject",
                    r => r.HasOne<Subject>().WithMany()
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK__ClassSubj__subje__619B8048"),
                    l => l.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ClassSubj__class__60A75C0F"),
                    j =>
                    {
                        j.HasKey("ClassNo", "SubjectId").HasName("PK__ClassSub__6FB88693CF5CBFE4");
                        j.ToTable("ClassSubject");
                    });
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorDate);

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(255)
                .IsFixedLength();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__4D11D63C915A4561");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ClassNoNavigation).WithMany(p => p.StudentsNavigation)
                .HasForeignKey(d => d.ClassNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__classNo__3A81B327");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__ACF9A760114AAEFD");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).ValueGeneratedNever();
            entity.Property(e => e.Book)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectTeacher).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
