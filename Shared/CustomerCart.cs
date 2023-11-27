namespace Shared;

public class CustomerCart
{
    public Guid CustomerId { get; set; }

    public List<Guid> ProductIds { get; set; }
}