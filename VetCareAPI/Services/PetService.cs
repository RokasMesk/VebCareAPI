// Services/PetService.cs
using VetCareAPI.Models;
using VetCareAPI.Models.DTOs.Pets;
using VetCareAPI.Models.Mappings;
using VetCareAPI.Repositories;

namespace VetCareAPI.Services;

public class PetService
{
    private readonly PetRepository _pets;
    private readonly AppUserRepository _users;
    public PetService(PetRepository pets, AppUserRepository users)
    { _pets = pets; _users = users; }

    public async Task<PetDto?> GetAsync(Guid id) =>
        (await _pets.GetAsync(id))?.ToDto();

    public async Task<List<PetDto>> GetAllAsync() =>
        (await _pets.GetAllAsync()).Select(c => c.ToDto()).ToList();
    public async Task<List<PetDto>> GetByUserAsync(Guid userId) =>
        (await _pets.GetByUserAsync(userId)).Select(p => p.ToDto()).ToList();

    public async Task<PetDto> CreateAsync(CreatePetDto dto)
    {
        if (await _users.GetAsync(dto.UserId) is null)
            throw new InvalidOperationException("User does not exist"); // -> 422

        var e = dto.ToEntity();
        await _pets.AddAsync(e);
        return e.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePetDto dto)
    {
        var e = await _pets.GetAsync(id);
        if (e is null) return false;     // -> 404
        e.Apply(dto);
        return await _pets.UpdateAsync(e);
    }

    public Task<bool> DeleteAsync(Guid id) => _pets.DeleteAsync(id);
}