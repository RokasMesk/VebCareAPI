// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetCareAPI.Models;

namespace VetCareAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<AppUser> Users   => Set<AppUser>();
    public DbSet<Pet> Pets        => Set<Pet>();
    public DbSet<Visit> Visits    => Set<Visit>();

    protected override void OnModelCreating(ModelBuilder model)
{
    base.OnModelCreating(model);

    // Relationships
    model.Entity<Pet>()
        .HasOne(p => p.User)
        .WithMany(u => u.Pets)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    model.Entity<Visit>()
        .HasOne(v => v.Clinic)
        .WithMany(c => c.Visits)
        .HasForeignKey(v => v.ClinicId)
        .OnDelete(DeleteBehavior.Restrict);

    model.Entity<Visit>()
        .HasOne(v => v.Pet)
        .WithMany(p => p.Visits)
        .HasForeignKey(v => v.PetId)
        .OnDelete(DeleteBehavior.Cascade);

    var utc = new ValueConverter<DateTime, DateTime>(
        toProvider   => toProvider.Kind == DateTimeKind.Utc ? toProvider : toProvider.ToUniversalTime(),
        fromProvider => DateTime.SpecifyKind(fromProvider, DateTimeKind.Utc)
    );
    model.Entity<Visit>().Property(v => v.StartsAt).HasColumnType("datetime(6)").HasConversion(utc);
    model.Entity<Visit>().Property(v => v.EndsAt)  .HasColumnType("datetime(6)").HasConversion(utc);

    var clinicKaunas  = Guid.Parse("11111111-1111-1111-1111-111111111111");
    var clinicVilnius = Guid.Parse("11111111-1111-1111-1111-222222222222");

    var userRokas     = Guid.Parse("22222222-2222-2222-2222-222222222222");
    var userAuste     = Guid.Parse("22222222-2222-2222-2222-333333333333");

    var petMaksis     = Guid.Parse("33333333-3333-3333-3333-111111111111");
    var petMurka      = Guid.Parse("33333333-3333-3333-3333-222222222222");
    var petPukis      = Guid.Parse("33333333-3333-3333-3333-333333333333");

    var v1Completed   = Guid.Parse("44444444-4444-4444-4444-111111111111");
    var v2Scheduled   = Guid.Parse("44444444-4444-4444-4444-222222222222");
    var v3Scheduled   = Guid.Parse("44444444-4444-4444-4444-333333333333");
    var v4Cancelled   = Guid.Parse("44444444-4444-4444-4444-444444444444");

    model.Entity<Clinic>().HasData(
        new Clinic { Id = clinicKaunas,  Name = "ZooVet Kaunas",  City = "Kaunas",  Address = "Laisvės al. 1" },
        new Clinic { Id = clinicVilnius, Name = "VetHelp Vilnius", City = "Vilnius", Address = "Gedimino pr. 2" }
    );

    model.Entity<AppUser>().HasData(
        new AppUser { Id = userRokas, FullName = "Rokas Meškauskas", Email = "rokas@example.com" },
        new AppUser { Id = userAuste, FullName = "Austė Petrauskaitė", Email = "auste@example.com" }
    );

    model.Entity<Pet>().HasData(
        new Pet { Id = petMaksis, Name = "Maksis", Species = "Dog", UserId = userRokas },
        new Pet { Id = petMurka,  Name = "Murka", Species = "Cat", UserId = userRokas },
        new Pet { Id = petPukis,  Name = "Pūkis", Species = "Rabbit", UserId = userAuste }
    );
    model.Entity<Visit>().HasData(
        new Visit {
            Id = v1Completed,
            StartsAt = new DateTime(2025, 09, 15, 09, 00, 00, DateTimeKind.Utc),
            EndsAt   = new DateTime(2025, 09, 15, 09, 30, 00, DateTimeKind.Utc),
            Notes = "Vaccination completed.",
            Status = VisitStatus.Completed,
            Reason = VisitReason.Vaccination,
            ChiefComplaint = null,
            DiagnosisCode  = null,
            DiagnosisText  = null,
            Severity = Severity.Mild,
            ClinicId = clinicKaunas,
            PetId = petMaksis
        },
        new Visit {
            Id = v2Scheduled,
            StartsAt = new DateTime(2025, 10, 12, 08, 30, 00, DateTimeKind.Utc),
            EndsAt   = new DateTime(2025, 10, 12, 09, 00, 00, DateTimeKind.Utc),
            Notes = "Annual check.",
            Status = VisitStatus.Scheduled,
            Reason = VisitReason.Checkup,
            ChiefComplaint = "Limping on right hind leg",
            DiagnosisCode  = null,
            DiagnosisText  = null,
            Severity = Severity.Moderate,
            ClinicId = clinicKaunas,
            PetId = petMaksis
        },

        new Visit {
            Id = v3Scheduled,
            StartsAt = new DateTime(2025, 10, 20, 14, 00, 00, DateTimeKind.Utc),
            EndsAt   = new DateTime(2025, 10, 20, 14, 30, 00, DateTimeKind.Utc),
            Notes = "Dental check.",
            Status = VisitStatus.Scheduled,
            Reason = VisitReason.Dental,
            ChiefComplaint = "Bad breath",
            DiagnosisCode  = null,
            DiagnosisText  = null,
            Severity = Severity.Mild,
            ClinicId = clinicVilnius,
            PetId = petMurka
        },
        new Visit {
            Id = v4Cancelled,
            StartsAt = new DateTime(2025, 10, 05, 16, 00, 00, DateTimeKind.Utc),
            EndsAt   = new DateTime(2025, 10, 05, 16, 30, 00, DateTimeKind.Utc),
            Notes = "Owner cancelled day before.",
            Status = VisitStatus.Cancelled,
            Reason = VisitReason.Illness,
            ChiefComplaint = "Loss of appetite",
            DiagnosisCode  = null,
            DiagnosisText  = null,
            Severity = Severity.Moderate,
            ClinicId = clinicVilnius,
            PetId = petPukis
        }
    );
}

}
