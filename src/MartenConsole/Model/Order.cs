namespace MartenDemo.Model;

public class Order
{
    public Order()
    {
        Products = new List<Product>();
    }

    public int Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<Product> Products { get; set; }
}


public enum OrderStatus {Pending, Confirmed, Shipped}