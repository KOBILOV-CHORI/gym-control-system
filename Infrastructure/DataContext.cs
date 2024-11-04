using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
}