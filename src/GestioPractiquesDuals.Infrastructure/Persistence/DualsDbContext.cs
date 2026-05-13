using GestioPractiquesDuals.Domain.Entities;
using GestioPractiquesDuals.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GestioPractiquesDuals.Infrastructure.Persistence;

public sealed class DualsDbContext(DbContextOptions<DualsDbContext> options) : DbContext(options)
{
    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
    public DbSet<TrainingCycle> TrainingCycles => Set<TrainingCycle>();
    public DbSet<ClassGroup> ClassGroups => Set<ClassGroup>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentAcademicEnrollment> Enrollments => Set<StudentAcademicEnrollment>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyMentor> CompanyMentors => Set<CompanyMentor>();
    public DbSet<InternshipAgreement> InternshipAgreements => Set<InternshipAgreement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(20);
            entity.Property(x => x.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TrainingCycle>(entity =>
        {
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(20);
            entity.Property(x => x.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<ClassGroup>(entity =>
        {
            entity.Property(x => x.Code).HasMaxLength(20);
            entity.Property(x => x.Name).HasMaxLength(150);
            entity.HasOne(x => x.AcademicYear).WithMany().HasForeignKey(x => x.AcademicYearId);
            entity.HasOne(x => x.TrainingCycle).WithMany(x => x.ClassGroups).HasForeignKey(x => x.TrainingCycleId);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasIndex(x => x.SchoolEmail).IsUnique();
            entity.Property(x => x.SchoolEmail).HasMaxLength(200);
            entity.Property(x => x.FirstName).HasMaxLength(100);
            entity.Property(x => x.LastName).HasMaxLength(150);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(x => x.SchoolEmail).IsUnique();
            entity.HasIndex(x => x.NationalId).IsUnique();
            entity.Property(x => x.SchoolEmail).HasMaxLength(200);
            entity.Property(x => x.FirstName).HasMaxLength(100);
            entity.Property(x => x.LastName).HasMaxLength(150);
            entity.Property(x => x.NationalId).HasMaxLength(30);
        });

        modelBuilder.Entity<StudentAcademicEnrollment>(entity =>
        {
            entity.HasIndex(x => new { x.StudentId, x.AcademicYearId }).IsUnique();
            entity.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId);
            entity.HasOne(x => x.AcademicYear).WithMany().HasForeignKey(x => x.AcademicYearId);
            entity.HasOne(x => x.TrainingCycle).WithMany().HasForeignKey(x => x.TrainingCycleId);
            entity.HasOne(x => x.ClassGroup).WithMany(x => x.Enrollments).HasForeignKey(x => x.ClassGroupId);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(200);
            entity.Property(x => x.ContactEmail).HasMaxLength(200);
        });

        modelBuilder.Entity<CompanyMentor>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(150);
            entity.Property(x => x.Email).HasMaxLength(200);
            entity.HasOne(x => x.Company).WithMany(x => x.Mentors).HasForeignKey(x => x.CompanyId);
        });

        modelBuilder.Entity<InternshipAgreement>(entity =>
        {
            entity.Property(x => x.InternshipType).HasMaxLength(50);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            entity.HasOne(x => x.Student).WithMany(x => x.Agreements).HasForeignKey(x => x.StudentId);
            entity.HasOne(x => x.AcademicYear).WithMany().HasForeignKey(x => x.AcademicYearId);
            entity.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId);
            entity.HasOne(x => x.CompanyMentor).WithMany().HasForeignKey(x => x.CompanyMentorId);
            entity.HasIndex(x => new { x.StudentId, x.AcademicYearId, x.Status });
        });

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        var academicYearId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var cycleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var classId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var teacherId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var studentId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var enrollmentId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var companyId = Guid.Parse("77777777-7777-7777-7777-777777777777");
        var mentorId = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var agreementId = Guid.Parse("99999999-9999-9999-9999-999999999999");

        modelBuilder.Entity<AcademicYear>().HasData(new AcademicYear
        {
            Id = academicYearId,
            Code = "2025-2026",
            Name = "Curs 2025-2026",
            StartDate = new DateOnly(2025, 9, 1),
            EndDate = new DateOnly(2026, 6, 30),
            IsActive = true
        });

        modelBuilder.Entity<TrainingCycle>().HasData(new TrainingCycle
        {
            Id = cycleId,
            Code = "ASIX",
            Name = "Administracio de Sistemes Informatics en Xarxa"
        });

        modelBuilder.Entity<ClassGroup>().HasData(new ClassGroup
        {
            Id = classId,
            AcademicYearId = academicYearId,
            TrainingCycleId = cycleId,
            Code = "S1SX",
            Name = "S1SX"
        });

        modelBuilder.Entity<Teacher>().HasData(new Teacher
        {
            Id = teacherId,
            SchoolEmail = "tutor.asix@sarria.salesians.cat",
            FirstName = "Tutor",
            LastName = "ASIX",
            TrainingCycleId = cycleId,
            IsManager = true
        });

        modelBuilder.Entity<Student>().HasData(new Student
        {
            Id = studentId,
            SchoolEmail = "alumne.demo@sarria.salesians.cat",
            FirstName = "Alumne",
            LastName = "Demo",
            NationalId = "00000000X",
            PhoneNumber = "600000000"
        });

        modelBuilder.Entity<StudentAcademicEnrollment>().HasData(new StudentAcademicEnrollment
        {
            Id = enrollmentId,
            StudentId = studentId,
            AcademicYearId = academicYearId,
            TrainingCycleId = cycleId,
            ClassGroupId = classId,
            CanEditProfile = false
        });

        modelBuilder.Entity<Company>().HasData(new Company
        {
            Id = companyId,
            Name = "Empresa Demo",
            ContactEmail = "contacte@empresademo.test",
            PhoneNumber = "930000000"
        });

        modelBuilder.Entity<CompanyMentor>().HasData(new CompanyMentor
        {
            Id = mentorId,
            CompanyId = companyId,
            FullName = "Tutor d'Empresa Demo",
            Email = "mentor@empresademo.test"
        });

        modelBuilder.Entity<InternshipAgreement>().HasData(new InternshipAgreement
        {
            Id = agreementId,
            StudentId = studentId,
            AcademicYearId = academicYearId,
            CompanyId = companyId,
            CompanyMentorId = mentorId,
            InternshipType = "Dual gen.",
            Status = AgreementStatus.Open,
            StartDate = new DateOnly(2026, 2, 1)
        });
    }
}
