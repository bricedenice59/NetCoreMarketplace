namespace API.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int ProductStock { get; set; }
    
    public string ProductType { get; set; }
    
    public double PriceWithExcludedVAT { get; set; }
}