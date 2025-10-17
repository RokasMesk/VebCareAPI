namespace VetCareAPI.Models.DTOs.Visits;
using System;
using System.ComponentModel.DataAnnotations;
using VetCareAPI.Models;

public record class UpdateVisitDto
{
    [Required]
    public DateTime StartsAt { get; init; }

    [Required]
    public DateTime EndsAt { get; init; }

    [StringLength(1000)]
    public string? Notes { get; init; }

    [Required]
    public VisitStatus Status { get; init; }

    [Required]
    public VisitReason Reason { get; init; }

    public string? ChiefComplaint { get; init; }
    public string? DiagnosisCode { get; init; }
    public string? DiagnosisText { get; init; }
    public Severity? Severity { get; init; }
}