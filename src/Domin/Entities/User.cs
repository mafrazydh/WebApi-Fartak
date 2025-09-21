using Microsoft.AspNetCore.Identity;
namespace Domin.Entities;

public class User : IdentityUser
{

    public required string FullName { get; set; }
    public string? Profile { get; set; }
    public DateTime? CreatedDate { get; set; }
    public ICollection<Cart> Cart { get; set; }
    public ICollection<Order> Orders { get; set; }


}
