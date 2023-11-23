namespace Domain;

public class Order
{
    public Guid Id { get; set; }
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
    public DateTime ShippingDate { get; set; }
}