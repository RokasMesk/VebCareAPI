// Repositories/AppUserRepository.cs
using Microsoft.EntityFrameworkCore;
using VetCareAPI.Data;
using VetCareAPI.Models;

namespace VetCareAPI.Repositories;

public class AppUserRepository
{
    private readonly ApplicationDbContext _db;
    public AppUserRepository(ApplicationDbContext db) => _db = db;

    public Task<List<AppUser>> GetAllAsync() =>
        _db.Users.AsNoTracking().ToListAsync();

    public Task<AppUser?> GetAsync(Guid id) =>
        _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    public Task<AppUser?> GetByEmailAsync(string email) =>
        _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    public Task<bool> EmailExistsAsync(string email) =>
        _db.Users.AnyAsync(u => u.Email == email);

    public async Task<AppUser> AddAsync(AppUser entity)
    {
        _db.Users.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(AppUser entity)
    {
        var tracked = await _db.Users.FirstOrDefaultAsync(u => u.Id == entity.Id);
        if (tracked is null) return false;

        tracked.FullName = entity.FullName;
        tracked.Email    = entity.Email;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tracked = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (tracked is null) return false;

        _db.Users.Remove(tracked);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(Guid id) =>
        _db.Users.AnyAsync(u => u.Id == id);

    public Task<List<Pet>> GetPetsAsync(Guid userId) =>
        _db.Pets.AsNoTracking()
            .Where(p => p.UserId == userId)
            .ToListAsync();
}