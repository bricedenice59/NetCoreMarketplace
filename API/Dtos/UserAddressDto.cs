using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UserAddressDto
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required] 
    public string Zipcode { get; set; }
}