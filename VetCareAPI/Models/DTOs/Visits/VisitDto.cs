namespace VetCareAPI.Models.DTOs.Visits;

using System;
using VetCareAPI.Models;

public record class VisitDto(
    Guid Id,
    DateTime StartsAt,
    DateTime EndsAt,
    string? Notes,
    VisitStatus Status,
    VisitReason Reason,
    string? ChiefComplaint,
    string? DiagnosisCode,
    string? DiagnosisText,
    Severity? Severity,
    Guid ClinicId,
    Guid PetId
);