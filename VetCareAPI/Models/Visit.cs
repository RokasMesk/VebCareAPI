namespace VetCareAPI.Models;

public class Visit
{
    public Guid Id { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Notes { get; set; }
    public VisitStatus Status { get; set; }
    public VisitReason Reason { get; set; } = VisitReason.Checkup;

    // NEW: patient-reported issue (free text, e.g., "limping", "vomiting")
    public string? ChiefComplaint { get; set; }

    // NEW (optional): clinician diagnosis (structured/free)
    public string? DiagnosisCode { get; set; }   // e.g., ICD-10 "K52.9"
    public string? DiagnosisText { get; set; }   // e.g., "Gastroenteritis"
    public Severity? Severity { get; set; }      // optional triage severity
    
    
    public Guid ClinicId { get; set; }
    public Clinic Clinic { get; set; }
    
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
}

public enum VisitStatus
{
    Scheduled = 0,
    Completed = 1,
    Cancelled = 2
}
public enum VisitReason
{
    Checkup = 0,
    Vaccination = 1,
    Illness = 2,
    Injury = 3,
    FollowUp = 4,
    Dental = 5,
    Surgery = 6,
    Other = 7
}

public enum Severity { Mild = 0, Moderate = 1, Severe = 2, Critical = 3 }