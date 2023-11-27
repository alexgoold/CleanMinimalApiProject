using Shared.CustomerDtos;
using Shared.ProductsDtos;

namespace Shared.OrderDtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public List<ProductDto> Products { get; set; }
    public CustomerDto Customer { get; set; }
    public DateTime ShippingDate { get; set; }
}