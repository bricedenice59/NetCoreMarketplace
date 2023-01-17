namespace Core.Models;

public class CustomerBasket
{
    public CustomerBasket()
    {
        
    }
    public CustomerBasket(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public List<BasketItem> Items { get; set; } = new();
}

public class BasketItem
{
    public Guid Id{ get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public string productType { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}