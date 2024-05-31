using Microsoft.EntityFrameworkCore;
using MUSbooking.Model;

namespace MUSbooking.Context;

public class PrimaryDataBaseContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<OrderEquipment> OrderEquipments { get; set; }
    
    public PrimaryDataBaseContext(DbContextOptions<PrimaryDataBaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEquipment>()
            .HasKey(oe => new { oe.OrderId, oe.EquipmentId });

        modelBuilder.Entity<OrderEquipment>()
            .HasOne(oe => oe.Order)
            .WithMany(o => o.OrderEquipments)
            .HasForeignKey(oe => oe.OrderId);

        modelBuilder.Entity<OrderEquipment>()
            .HasOne(oe => oe.Equipment)
            .WithMany()
            .HasForeignKey(oe => oe.EquipmentId);
    }

}