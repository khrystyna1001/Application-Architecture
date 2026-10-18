namespace EShopService.Domain.Entities;

public class Product : BaseModel
{
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0; // Default value: 0
    public string Sku { get; set; } = string.Empty;
    
    // Foreign key or navigation property for the Category
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}