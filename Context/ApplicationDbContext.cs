using AppointmentsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Context;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Appointment>? Appointments { get; set; }
    public DbSet<AppUser>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            // keep the standard configuration in DbContext
            base.OnModelCreating(modelBuilder);


	    UserDescriptorTable(modelBuilder);
	    AppointmentDescriptionTable(modelBuilder);

    }
    private static void AppointmentDescriptionTable(ModelBuilder modelBuilder)
    {
	// Map a desire name to the table in the database
	modelBuilder.Entity<Appointment>().ToTable("appointment");

	// Set constraints for all columns and their requirements.
	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.ClientName)
	    .HasColumnName("client_name")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(100)
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.ClientAge)
	    .HasColumnName("client_age")
	    .HasColumnType("TINYINT") // to store int from 0 to 255
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.ClientRUT)
	    .HasColumnName("client_rut")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(10) // a Chilean RUT has 10 characters length without spaces or separators
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.ClientEmail)
	    .HasColumnName("client_email")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(255)
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.ClientPhone)
	    .HasColumnName("client_phone")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(10) // a Chilean Phone has 10 characters length without spaces or separators
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.Goals)
	    .HasColumnName("goals")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(255)
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.IsEmailVerified)
	    .HasColumnName("is_email_verified")
	    .HasColumnType("BIT") // we use the BIT data type in SQL Server to store boolean as 1 or 0
	    .IsRequired();

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.PrevDiagnostic)
	    .HasColumnName("previous_diagnostic")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(255);

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.IsCompleted)
	    .HasColumnName("is_completed")
	    .HasColumnType("BIT")
	    .IsRequired();
	//   We use DATETIME data type in SQL Server to store dates and times as UTC
	//   Later be converted to the required time zone
	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.AppointmentDateTime)
	    .HasColumnName("appointment_date")
	    .HasColumnType("DATETIME");

	modelBuilder.Entity<Appointment>()
	    .Property(prop => prop.PublicId)
	    .HasColumnName("public_id")
	    .HasColumnType("UNIQUEIDENTIFIER");

    }

    private static void UserDescriptorTable(ModelBuilder modelBuilder)
    {
	modelBuilder.Entity<AppUser>().ToTable("user");

	modelBuilder.Entity<AppUser>()
	    .Property(prop => prop.Phone)
	    .HasColumnName("phone")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(10) // a Chilean Phone has 10 characters length without spaces or separators
	    .IsRequired();

	modelBuilder.Entity<AppUser>()
	    .Property(prop => prop.Email)
	    .HasColumnName("email")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(255)
	    .IsRequired();

	modelBuilder.Entity<AppUser>()
	    .Property(prop => prop.Username)
	    .HasColumnName("username")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(100)
	    .IsRequired();

	modelBuilder.Entity<AppUser>()
	    .Property(prop => prop.Name)
	    .HasColumnName("name")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(100)
	    .IsRequired();

	modelBuilder.Entity<AppUser>()
	    .Property(prop => prop.Career)
	    .HasColumnName("career")
	    .HasColumnType("VARCHAR")
	    .HasMaxLength(50)
	    .IsRequired();
    }
}
