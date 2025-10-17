using Microsoft.EntityFrameworkCore;
using VetCareAPI.Data;
using VetCareAPI.Models;

namespace VetCareAPI.Repositories;

public class ClinicRepository
{
    private readonly ApplicationDbContext _db;
    public ClinicRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<List<Clinic>> GetAllAsync()
    {
        return _db.Clinics.AsNoTracking().ToListAsync();
    }

    public Task<Clinic?> GetAsync(Guid id)
    {
        return _db.Clinics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public Task<bool> ExistsAsync(Guid id)
    {
        return _db.Clinics.AnyAsync(c => c.Id == id);
    }

    public async Task<Clinic> AddAsync(Clinic clinic)
    {
        _db.Clinics.Add(clinic);
        await _db.SaveChangesAsync();
        return clinic;
    }

    public async Task<bool> UpdateAsync(Clinic entity)
    {
        _db.Clinics.Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tracked = await _db.Clinics.FirstOrDefaultAsync(c => c.Id == id);
        if (tracked is null) return false;
        _db.Clinics.Remove(tracked);
        await _db.SaveChangesAsync();
        return true;
    }
}