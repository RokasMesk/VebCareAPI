using Microsoft.AspNetCore.Mvc;
using VetCareAPI.Models.DTOs.Pets;
using VetCareAPI.Services;

namespace VetCareAPI.Controllers;

[ApiController]
[Route("api/pets")]
[Produces("application/json")]
public class PetsController : ControllerBase
{
    private readonly PetService _svc;
    public PetsController(PetService svc) => _svc = svc;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PetDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(Guid id)
        => (await _svc.GetAsync(id)) is { } dto ? Ok(dto) : NotFound();

    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId)
        => Ok(await _svc.GetByUserAsync(userId));
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
        try
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
        return await _svc.UpdateAsync(id, dto) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}
