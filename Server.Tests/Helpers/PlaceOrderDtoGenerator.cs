using Domain;
using Shared;
using Shared.OrderDtos;

namespace Tests.Helpers;

public static class PlaceOrderDtoGenerator
{
    public static PlaceOrderDto GenerateCartWith3Items()
    {
        return new PlaceOrderDto()
        {
            CustomerId = Guid.NewGuid(),
            ProductIds = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            }

        };
    }
}