using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace отель.DB;

public partial class _3kursContext : DbContext
{
    public _3kursContext()
    {
    }

    public _3kursContext(DbContextOptions<_3kursContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoginTbl> LoginTbls { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-2R2MGI84\\SQLEXPRESS;user=admin;password=admin;database=3kurs;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginTbl>(entity =>
        {
            entity.ToTable("LoginTbl");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Role).WithMany(p => p.LoginTbls)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_LoginTbl_Role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
