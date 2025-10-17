using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VetCareAPI.Models;

[Table("Users")]
[Index(nameof(Email), IsUnique = true)]
public class AppUser
{
    public Guid Id { get; set; }

    [MaxLength(120)]
    public string FullName { get; set; } = null!;

    [MaxLength(200)]
    public string Email { get; set; } = null!;

    // Future-ready: you can add navigation to roles later
    // public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}