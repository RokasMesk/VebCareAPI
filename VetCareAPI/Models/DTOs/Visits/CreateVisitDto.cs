namespace VetCareAPI.Models.DTOs.Visits;
using System;
using System.ComponentModel.DataAnnotations;


public record class CreateVisitDto
{
    [Required]
    public DateTime StartsAt { get; init; }

    [Required]
    public DateTime EndsAt { get; init; }

    [StringLength(1000)]
    public string? Notes { get; init; }

    [Required]
    public Guid ClinicId { get; init; }

    [Required]
    public Guid PetId { get; init; }

    public int? Reason { get; init; }
    public string? ChiefComplaint { get; init; }
    public string? DiagnosisCode { get; init; }
    public string? DiagnosisText { get; init; }
    public int? Severity { get; init; }
}