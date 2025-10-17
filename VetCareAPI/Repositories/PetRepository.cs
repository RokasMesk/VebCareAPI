using Microsoft.EntityFrameworkCore;
using VetCareAPI.Data;
using VetCareAPI.Models;

namespace VetCareAPI.Repositories;

public class PetRepository
{
    private readonly ApplicationDbContext _db;
    public PetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<Pet?> GetAsync(Guid id)
    {
        return _db.Pets.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Pet>> GetAllAsync()
    {
        return _db.Pets.AsNoTracking().ToListAsync();
    }

    public Task<List<Pet>> GetByUserAsync(Guid userId)
    {
        return _db.Pets.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Pet> AddAsync(Pet pet)
    {
        _db.Pets.Add(pet); 
        await _db.SaveChangesAsync();
        return pet;
    }

    public async Task<bool> UpdateAsync(Pet entity)
    {
        _db.Pets.Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pet = await _db.Pets.FindAsync(id);
        if (pet is null) return false;
        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync();
        return true;
    }
}