namespace CartService.Models;

public class CartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
