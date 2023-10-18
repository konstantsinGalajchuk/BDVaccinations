using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityConsole;

public partial class VaccinationsDbContext : DbContext
{
    public VaccinationsDbContext()
    {
    }

    public VaccinationsDbContext(DbContextOptions<VaccinationsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Dose> Doses { get; set; }

    public virtual DbSet<MedicalInstitution> MedicalInstitutions { get; set; }

    public virtual DbSet<MessagesAfterVaccination> MessagesAfterVaccinations { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    public virtual DbSet<VaccinationsInfo> VaccinationsInfos { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineDose> VaccineDoses { get; set; }

    public virtual DbSet<VaccinesWithDisease> VaccinesWithDiseases { get; set; }

    public virtual DbSet<VaccinesWithDose> VaccinesWithDoses { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-LBPEBEI;Database=VaccinationsDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PK__Diseases__69B533892139A12E");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dose>(entity =>
        {
            entity.HasKey(e => e.DoseId).HasName("PK__Doses__F03E00A24CD3DC6B");
        });

        modelBuilder.Entity<MedicalInstitution>(entity =>
        {
            entity.HasKey(e => e.MedicalInstitutionId).HasName("PK__MedicalI__D0403289720EE407");

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MessagesAfterVaccination>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9C23BF8F4A");

            entity.ToTable("MessagesAfterVaccination");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Doctor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Recommendations)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC366439CEF92");

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIO");
            entity.Property(e => e.Pasport)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sex)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("PK__Vaccinat__466430474958EF85");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.MedicalInstitution).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.MedicalInstitutionId)
                .HasConstraintName("FK__Vaccinati__Madic__49C3F6B7");

            entity.HasOne(d => d.Patient).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Vaccinati__Patie__48CFD27E");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__Vaccinati__Vacci__47DBAE45");
        });

        modelBuilder.Entity<VaccinationsInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VaccinationsInfo");

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIO");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.VaccineId).HasName("PK__Vaccines__45DC6889F6049265");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Disease).WithMany(p => p.Vaccines)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("FK__Vaccines__Diseas__3D5E1FD2");
        });

        modelBuilder.Entity<VaccineDose>(entity =>
        {
            entity.HasKey(e => e.VaccineDoseId).HasName("PK__VaccineD__BEA63A178DC3C611");

            entity.HasOne(d => d.Dose).WithMany(p => p.VaccineDoses)
                .HasForeignKey(d => d.DoseId)
                .HasConstraintName("FK__VaccineDo__DoseI__403A8C7D");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccineDoses)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__VaccineDo__Vacci__412EB0B6");
        });

        modelBuilder.Entity<VaccinesWithDisease>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VaccinesWithDiseases");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DiseaseName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VaccinesWithDose>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VaccinesWithDoses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
