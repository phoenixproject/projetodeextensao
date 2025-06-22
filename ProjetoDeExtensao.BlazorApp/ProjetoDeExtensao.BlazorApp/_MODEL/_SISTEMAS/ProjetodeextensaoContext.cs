using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

public partial class ProjetodeextensaoContext : DbContext
{
    public ProjetodeextensaoContext()
    {
    }

    public ProjetodeextensaoContext(DbContextOptions<ProjetodeextensaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TipoServico> TipoServicos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BUH27MO;Database=projetodeextensao;User Id=sysdb;Password=123;TrustServerCertificate=True;Encrypt=False");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.CdStatus).HasName("PK_Status_cd_status");

            entity.ToTable("Status");

            entity.Property(e => e.CdStatus).HasColumnName("cd_status");
            entity.Property(e => e.DsStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ds_status");
        });

        modelBuilder.Entity<TipoServico>(entity =>
        {
            entity.HasKey(e => e.TpServico).HasName("PK_Tipo_Servico_tp_servico");

            entity.ToTable("Tipo_Servico");

            entity.Property(e => e.TpServico).HasColumnName("tp_servico");
            entity.Property(e => e.DsTpServico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ds_tp_servico");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TpUsuario).HasName("PK_Tipo_Usuario_tp_usuario");

            entity.ToTable("Tipo_Usuario");

            entity.Property(e => e.TpUsuario).HasColumnName("tp_usuario");
            entity.Property(e => e.DsTpUsuario)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ds_tp_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CdUsuario).HasName("PK_EP_Usuario_cd_usuario");

            entity.ToTable("Usuario");

            entity.Property(e => e.CdUsuario).HasColumnName("cd_usuario");
            entity.Property(e => e.CdStatus).HasColumnName("cd_status");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telefone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("telefone");
            entity.Property(e => e.TpServico).HasColumnName("tp_servico");
            entity.Property(e => e.TpUsuario).HasColumnName("tp_usuario");

            entity.HasOne(d => d.CdStatusNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.CdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TpServicoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TpServico)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TpUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TpUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
