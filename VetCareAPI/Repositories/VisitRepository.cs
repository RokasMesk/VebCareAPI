using Microsoft.EntityFrameworkCore;
using VetCareAPI.Data;
using VetCareAPI.Models;

namespace VetCareAPI.Repositories;

public class VisitRepository
{
    private readonly ApplicationDbContext _db;
    public VisitRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<Visit?> GetAsync(Guid id)
    {
        return _db.Visits.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<List<Visit>> GetAllAsync()
    {
        return _db.Visits.AsNoTracking().OrderBy(v => v.StartsAt).ToListAsync();
    }

    public async Task<Visit> AddAsync(Visit entity)
    {
        _db.Visits.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Visit entity)
    {
        _db.Visits.Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tracked = await _db.Visits.FirstOrDefaultAsync(v => v.Id == id);
        if (tracked is null) return false;
        _db.Visits.Remove(tracked);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Visit>> GetByClinicAsync(Guid clinicId)
    {
        return _db.Visits.AsNoTracking().Where(v => v.ClinicId == clinicId).ToListAsync();
    }

    public Task<List<Visit>> GetByPetAsync(Guid petId)
    {
        return _db.Visits.AsNoTracking().Where(v => v.PetId == petId).ToListAsync();
    }
}