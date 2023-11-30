namespace Shared.OrderDtos;

public class CreateOrUpdateOrderDto
{
    public Guid CustomerId { get; set; }
    public List<Guid> ProductIds { get; set; }
}