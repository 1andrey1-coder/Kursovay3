using System;
using System.Collections.Generic;
using ApiDB.DB;
using Microsoft.EntityFrameworkCore;

namespace ApiDB;

public partial class MemContext : DbContext
{
    public MemContext()
    {
        //Database.EnsureCreated();
    }

    public MemContext(DbContextOptions<MemContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }
    public static void OnConfigurate(MemContext context)
    {
        //    if(context.Database != null)
        //    {
        //        context.Database.EnsureDeleted();
        //    }
        //    context.Database.EnsureCreated();
    }

//("server=192.168.200.35;password=64457;user=user43;database=user43;TrustServerCertificate=true"); колледж
//("server=LAPTOP-2R2MGI84\\SQLEXPRESS;password=admin;user=admin;database=mem;TrustServerCertificate=true"); дома
public virtual DbSet<End> Ends { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<LoginUser> LoginUsers { get; set; }

    public virtual DbSet<Rofl> Rofls { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Start> Starts { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Teg> Tegs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;password=64457;user=user43;database=user43;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<End>(entity =>
        {
            entity.ToTable("End");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
        });

        modelBuilder.Entity<LoginUser>(entity =>
        {
            entity.HasKey(e => e.LoginId);

            entity.ToTable("LoginUser");

            entity.Property(e => e.LoginId).HasColumnName("LoginID");

            entity.HasOne(d => d.Role).WithMany(p => p.LoginUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_LoginUser_Role");
        });

        modelBuilder.Entity<Rofl>(entity =>
        {
            entity.ToTable("Rofl");

            entity.Property(e => e.RoflId).HasColumnName("RoflID");
            entity.Property(e => e.RoflDateTime).HasColumnType("date");

            entity.HasOne(d => d.RoflEnd).WithMany(p => p.Rofls)
                .HasForeignKey(d => d.RoflEndId)
                .HasConstraintName("FK_Rofl_End");

            entity.HasOne(d => d.RoflGenre).WithMany(p => p.Rofls)
                .HasForeignKey(d => d.RoflGenreId)
                .HasConstraintName("FK_Rofl_Genre");

            entity.HasOne(d => d.RoflStart).WithMany(p => p.Rofls)
                .HasForeignKey(d => d.RoflStartId)
                .HasConstraintName("FK_Rofl_Start");

            entity.HasOne(d => d.RoflStatus).WithMany(p => p.Rofls)
                .HasForeignKey(d => d.RoflStatusId)
                .HasConstraintName("FK_Rofl_Status");

            entity.HasOne(d => d.Teg).WithMany(p => p.Rofls)
                .HasForeignKey(d => d.TegId)
                .HasConstraintName("FK_Rofl_Teg");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Start>(entity =>
        {
            entity.ToTable("Start");

            entity.Property(e => e.StartId).HasColumnName("StartID");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
        });

        modelBuilder.Entity<Teg>(entity =>
        {
            entity.ToTable("Teg");

            entity.Property(e => e.TegId).HasColumnName("TegID");
            entity.Property(e => e.TegName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
