using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.Product
{
    public record ProductDTO
    {
    public int ProductID { get; init; }
    [Required(ErrorMessage ="Product name is required")]
    public string? ProductName { get; init; } = string.Empty;
    [Required(ErrorMessage ="Price is required")]
    public decimal Price { get; init; }
    public int? CategoryID { get; init; }
    public string? Summary { get; set; }
    public string? ImageUrl { get; set; }
    
    }
}