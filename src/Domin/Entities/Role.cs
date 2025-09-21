using Microsoft.AspNetCore.Identity;


namespace Domin.Entities;

public class Role : IdentityRole
{
    public string? Description { get; set; }
}

