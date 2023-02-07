using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CustomerBasketDto
{
    [Required]
    public Guid Id { get; set; }
    public List<BasketItemDto> Items { get; set; }
}