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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassModel>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                      .IsRequired();

                entity.HasOne<ClassModel>()
                      .WithMany()
                      .HasForeignKey(c => c.BaseClassId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeModel>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name)
                      .IsRequired();
                entity.Property(a => a.Type)
                      .IsRequired();

                entity.HasOne<ClassModel>()
                      .WithMany(c => c.Attributes)
                      .HasForeignKey(a => a.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MethodModel>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name)
                      .IsRequired();

                entity.HasOne<ClassModel>()
                      .WithMany(c => c.Methods)
                      .HasForeignKey(m => m.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ParamModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                      .IsRequired();
                entity.Property(p => p.Type)
                      .IsRequired();

                entity.HasOne<MethodModel>()
                      .WithMany(m => m.Params)
                      .HasForeignKey(p => p.MethodId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LocalVarModel>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Name)
                      .IsRequired();
                entity.Property(v => v.Type)
                      .IsRequired();

                entity.HasOne<MethodModel>()
                      .WithMany(m => m.Vars)
                      .HasForeignKey(v => v.MethodId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MethodCallModel>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.MethodName).IsRequired();
                entity.Property(c => c.ReferenceType).IsRequired();

                entity.HasOne<MethodModel>()
                    .WithMany(m => m.MethodsCalled)
                    .HasForeignKey(c => c.ParentMethodId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

    public SimuladorDbContext(DbContextOptions<SimuladorDbContext> options)
        : base(options)
    {
    }
}
