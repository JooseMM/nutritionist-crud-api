using AppointmentsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Context;

public class AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : DbContext(options)
{

    public DbSet<Appointment>? Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            // Set the connection string
            optionsBuilder.UseSqlServer("Server=Home\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True")
            // Print all queries done with SQL Server
            .LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information
            ).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            // keep the standard configuration in DbContext
            base.OnModelCreating(modelBuilder);

            // Map a desire name to the table in the database
            modelBuilder.Entity<Appointment>().ToTable("appointment");

            // Set constraints for all columns and their requirements.
            modelBuilder.Entity<Appointment>()
                    .Property(prop => prop.ClientName)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100)
                    .IsRequired();

                modelBuilder.Entity<Appointment>()
                    .Property(prop => prop.ClientAge)
                    .HasColumnType("TINYINT") // to store int from 0 to 255
                    .IsRequired();

                modelBuilder.Entity<Appointment>()
                    .Property(prop => prop.ClientRUT)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(10) // a Chilean RUT has 10 characters length without spaces or separators
                    .IsRequired();

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.ClientEmail)
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(255)
                        .IsRequired();

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.ClientPhone)
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(10) // a Chilean Phone has 10 characters length without spaces or separators
                        .IsRequired();

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.Goals)
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(255)
                        .IsRequired();

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.IsEmailVerified)
                        .HasColumnType("BIT") // we use the BIT data type in SQL Server to store boolean as 1 or 0
                        .IsRequired();

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.PrevDiagnostic)
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(255);

                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.IsCompleted)
                        .HasColumnType("BIT")
                        .IsRequired();

                //   We use DATETIME data type in SQL Server to store dates and times as UTC
                //   Later be converted to the required time zone
                modelBuilder.Entity<Appointment>()
                        .Property(prop => prop.AppointmentDateTime)
                        .HasColumnType("DATETIME");
        }
}