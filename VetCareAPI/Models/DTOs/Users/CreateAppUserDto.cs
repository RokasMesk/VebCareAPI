namespace VetCareAPI.Models.DTOs.Users;
using System.ComponentModel.DataAnnotations;


public record class CreateAppUserDto
{
    [Required, StringLength(120)]
    public string FullName { get; init; } = null!;

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; init; } = null!;
}