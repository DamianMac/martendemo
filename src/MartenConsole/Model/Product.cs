namespace MartenDemo.Model;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTimeOffset ActiveDate { get; set; }
}