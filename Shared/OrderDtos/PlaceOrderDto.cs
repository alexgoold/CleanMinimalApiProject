namespace Shared.OrderDtos;

public class PlaceOrderDto
{
	public Guid CustomerId { get; set; }
	public List<Guid> ProductIds { get; set; }
}