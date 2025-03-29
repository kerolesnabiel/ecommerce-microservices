namespace CartService.DTOs;

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime AddedAt { get; set; }
    public decimal TotalPrice { get; set; }
}
