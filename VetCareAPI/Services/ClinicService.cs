// Services/ClinicService.cs
using VetCareAPI.Models;
using VetCareAPI.Models.DTOs.Clinics;
using VetCareAPI.Models.DTOs.Visits;
using VetCareAPI.Models.Mappings;
using VetCareAPI.Repositories;

namespace VetCareAPI.Services;

public class ClinicService
{
    private readonly ClinicRepository _clinics;
    private readonly VisitRepository _visits;
    public ClinicService(ClinicRepository clinics, VisitRepository visits)
    { _clinics = clinics; _visits = visits; }

    public async Task<List<ClinicDto>> GetAllAsync() =>
        (await _clinics.GetAllAsync()).Select(c => c.ToDto()).ToList();

    public async Task<ClinicDto?> GetAsync(Guid id) =>
        (await _clinics.GetAsync(id))?.ToDto();

    public async Task<ClinicDto> CreateAsync(CreateClinicDto dto)
    {
        var e = dto.ToEntity();
        await _clinics.AddAsync(e);
        return e.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateClinicDto dto)
    {
        var e = await _clinics.GetAsync(id);
        if (e is null) return false;
        e.Apply(dto);
        return await _clinics.UpdateAsync(e);
    }

    public Task<bool> DeleteAsync(Guid id) => _clinics.DeleteAsync(id);

    public async Task<List<VisitDto>> GetClinicVisitsAsync(Guid clinicId, DateTime? fromUtc = null, DateTime? toUtc = null)
    {
        var list = await _visits.GetByClinicAsync(clinicId);
        if (fromUtc is not null) list = list.Where(v => v.StartsAt >= fromUtc).ToList();
        if (toUtc   is not null) list = list.Where(v => v.StartsAt <= toUtc).ToList();
        return list.Select(v => v.ToDto()).ToList();
    }
    public async Task<List<VisitDto>> GetPetsVisitsAsync(Guid petId, DateTime? fromUtc = null, DateTime? toUtc = null)
    {
        var list = await _visits.GetByPetAsync(petId);
        if (fromUtc is not null) list = list.Where(v => v.StartsAt >= fromUtc).ToList();
        if (toUtc   is not null) list = list.Where(v => v.StartsAt <= toUtc).ToList();
        return list.Select(v => v.ToDto()).ToList();
    }
}