using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimuladorDeObjetos.Infrastructure;

public class SimuladorDbContext : DbContext
{
    public DbSet<ClassModel> Classes { get; set; }
    public DbSet<AttributeModel> Attributes { get; set; }
    public DbSet<MethodModel> Methods { get; set; }
    public DbSet<ParamModel> Params { get; set; }
    public DbSet<LocalVarModel> LocalVars { get; set; }
    public DbSet<MethodCallModel> MethodCalls { get; set; }
    public DbSet<InterfaceModel> InterfaceModels { get; set; }
    public DbSet<InterfaceMethodModel> InterfaceMethodModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassModel>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.HasOne(c => c.BaseClass)
                .WithMany()
                .HasForeignKey(c => c.BaseClassId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AttributeModel>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Type).IsRequired().HasMaxLength(100);
            entity.HasOne(a => a.Class)
                .WithMany(c => c.Attributes)
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MethodModel>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
            entity.HasOne(m => m.Class)
                .WithMany(c => c.Methods)
                .HasForeignKey(m => m.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ParamModel>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Type).IsRequired().HasMaxLength(100);

            entity.HasOne(p => p.Method)
                .WithMany(m => m.Params)
                .HasForeignKey(p => p.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasOne<InterfaceMethodModel>()
                .WithMany(i => i.Parameters)
                .HasForeignKey("InterfaceMethodModelId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        });

        modelBuilder.Entity<LocalVarModel>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
            entity.Property(v => v.Type).IsRequired().HasMaxLength(100);
            entity.HasOne(v => v.Method)
                .WithMany(m => m.Vars)
                .HasForeignKey(v => v.MethodId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MethodCallModel>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.MethodName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.ReferenceType).IsRequired();
            entity.HasOne(c => c.ParentMethod)
                .WithMany(m => m.MethodsCalled)
                .HasForeignKey(c => c.ParentMethodId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterfaceModel>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Name).IsRequired().HasMaxLength(100);
            entity.HasMany(i => i.Methods)
                  .WithOne()
                  .HasForeignKey("InterfaceId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterfaceMethodModel>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
            entity.Property(m => m.ReturnType).IsRequired().HasMaxLength(100);
        });

        base.OnModelCreating(modelBuilder);
    }

    public SimuladorDbContext(DbContextOptions<SimuladorDbContext> options)
        : base(options) { }
}
