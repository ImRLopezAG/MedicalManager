using MedicalManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Infrastructure.Persistence.Context;

public class MedicalContext : DbContext {
  public MedicalContext(DbContextOptions<MedicalContext> options) : base(options) { }

  public DbSet<Patient> Patients { get; set; }
  public DbSet<Doctor> Doctors { get; set; }
  public DbSet<LabTest> LabTests { get; set; }
  public DbSet<LabResult> LabResults { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Date> Dates { get; set; }



  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    #region tables
    modelBuilder.Entity<Patient>().ToTable("Patients");
    modelBuilder.Entity<Doctor>().ToTable("Doctors");
    modelBuilder.Entity<LabTest>().ToTable("LabTests");
    modelBuilder.Entity<LabResult>().ToTable("LabResults");
    modelBuilder.Entity<User>().ToTable("Users");
    modelBuilder.Entity<Date>().ToTable("Dates");
    #endregion

    #region keys
    modelBuilder.Entity<Patient>().HasKey(p => p.Id);
    modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
    modelBuilder.Entity<LabTest>().HasKey(lt => lt.Id);
    modelBuilder.Entity<LabResult>().HasKey(lr => lr.Id);
    modelBuilder.Entity<User>().HasKey(u => u.Id);
    modelBuilder.Entity<Date>().HasKey(d => d.Id);
    #endregion

    #region relationships
    modelBuilder.Entity<LabTest>()
      .HasMany(lt => lt.LabResults)
      .WithOne(lr => lr.LabTest)
      .HasForeignKey(lr => lr.LabTestId);

    modelBuilder.Entity<Patient>()
      .HasMany(p => p.Dates)
      .WithOne(d => d.Patient)
      .HasForeignKey(d => d.PatientId);

    modelBuilder.Entity<Doctor>()
      .HasMany(d => d.Dates)
      .WithOne(d => d.Doctor)
      .HasForeignKey(d => d.DoctorId);

    modelBuilder.Entity<Date>()
      .HasOne(d => d.Patient)
      .WithMany(p => p.Dates)
      .HasForeignKey(d => d.PatientId);

    modelBuilder.Entity<Date>()
      .HasOne(d => d.Doctor)
      .WithMany(d => d.Dates)
      .HasForeignKey(d => d.DoctorId);

    modelBuilder.Entity<Date>()
      .HasMany(d => d.LabResults)
      .WithOne(lr => lr.Date)
      .HasForeignKey(lr => lr.DateId);

    modelBuilder.Entity<LabResult>()
      .HasOne(lr => lr.Date)
      .WithMany(d => d.LabResults)
      .HasForeignKey(lr => lr.DateId);

    modelBuilder.Entity<LabResult>()
      .HasOne(lr => lr.LabTest)
      .WithMany(lt => lt.LabResults)
      .HasForeignKey(lr => lr.LabTestId);

    #endregion

    #region "Properties Configuration"

    #region "Patient"

    modelBuilder.Entity<Patient>()
      .Property(p => p.FirstName)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<Patient>()
      .Property(p => p.LastName)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<Patient>()
      .Property(p => p.Address)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<Patient>()
      .Property(p => p.Identification)
      .IsRequired()
      .HasMaxLength(20);

    modelBuilder.Entity<Patient>()
      .Property(p => p.Smoker)
      .IsRequired();

    modelBuilder.Entity<Patient>()
      .Property(p => p.Allergic)
      .IsRequired();
    #endregion

    #region "Doctor"

    modelBuilder.Entity<Doctor>()
      .Property(d => d.FirstName)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<Doctor>()
      .Property(d => d.LastName)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<Doctor>()
      .Property(d => d.Identification)
      .IsRequired()
      .HasMaxLength(20);

    #endregion

    #region "LabTest"

    modelBuilder.Entity<LabTest>()
      .Property(lt => lt.Name)
      .IsRequired()
      .HasMaxLength(50);
    #endregion

    #region "LabResult"


    modelBuilder.Entity<LabResult>()
      .Property(lr => lr.LabTestId)
      .IsRequired();
    #endregion

    #region "User"

    modelBuilder.Entity<User>()
      .Property(u => u.UserName)
      .IsRequired()
      .HasMaxLength(50);

    modelBuilder.Entity<User>()
      .Property(u => u.Password)
      .IsRequired();

    modelBuilder.Entity<User>()
      .Property(u => u.Role)
      .IsRequired()
      .HasMaxLength(15);

    #endregion

    #region "Date"

    modelBuilder.Entity<Date>()
      .Property(d => d.PatientId)
      .IsRequired();

    modelBuilder.Entity<Date>()
      .Property(d => d.DoctorId)
      .IsRequired();

    modelBuilder.Entity<Date>()
      .Property(d => d.Day)
      .IsRequired();

    modelBuilder.Entity<Date>()
      .Property(d => d.Hour)
      .IsRequired();

    #endregion

    #endregion
  }
}
