namespace Entities.DTOs.Product
{
    public record ProductDTOForUpdate : ProductDTO
    {
        public bool Showcase { get; set; }
    }
}