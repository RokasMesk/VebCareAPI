// Services/VisitService.cs
using VetCareAPI.Models;
using VetCareAPI.Models.DTOs.Visits;
using VetCareAPI.Models.Mappings;
using VetCareAPI.Repositories;

namespace VetCareAPI.Services;

public class VisitService
{
    private readonly VisitRepository _visits;
    private readonly ClinicRepository _clinics;
    private readonly PetRepository _pets;
    public VisitService(VisitRepository visits, ClinicRepository clinics, PetRepository pets)
    { _visits = visits; _clinics = clinics; _pets = pets; }

    public async Task<VisitDto> CreateAsync(CreateVisitDto dto)
    {
        if (!await _clinics.ExistsAsync(dto.ClinicId))
            throw new InvalidOperationException("Clinic not found");
        if (await _pets.GetAsync(dto.PetId) is  null)
            throw new InvalidOperationException("Pet not found"); 
        if (dto.EndsAt <= dto.StartsAt)
            throw new ArgumentException("EndsAt must be after StartsAt");

        var v = dto.ToEntity();
        await _visits.AddAsync(v);
        return v.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateVisitDto dto)
    {
        if (dto.EndsAt <= dto.StartsAt)
            throw new ArgumentException("EndsAt must be after StartsAt");

        var v = await _visits.GetAsync(id);
        if (v is null) return false;
        v.Apply(dto);
        return await _visits.UpdateAsync(v);
    }
    
    public async Task<List<VisitDto>> GetByClinicAsync(Guid clinicId, DateTime? fromUtc = null, DateTime? toUtc = null)
    {
        var list = await _visits.GetByClinicAsync(clinicId);
        if (fromUtc is not null) list = list.Where(v => v.StartsAt >= fromUtc).ToList();
        if (toUtc   is not null) list = list.Where(v => v.StartsAt <= toUtc).ToList();
        return list.Select(v => v.ToDto()).ToList();
    }
    
    public async Task<List<VisitDto>> GetByPetAsync(Guid clinicId, DateTime? fromUtc = null, DateTime? toUtc = null)
    {
        var list = await _visits.GetByPetAsync(clinicId);
        if (fromUtc is not null) list = list.Where(v => v.StartsAt >= fromUtc).ToList();
        if (toUtc   is not null) list = list.Where(v => v.StartsAt <= toUtc).ToList();
        return list.Select(v => v.ToDto()).ToList();
    }

    public async Task<VisitDto?> GetAsync(Guid id) =>
        (await _visits.GetAsync(id))?.ToDto();
    
    public Task<bool> DeleteAsync(Guid id) => _visits.DeleteAsync(id);
}
