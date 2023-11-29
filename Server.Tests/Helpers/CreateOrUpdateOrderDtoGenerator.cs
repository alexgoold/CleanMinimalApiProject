using Shared.OrderDtos;

namespace Tests.Helpers;

public static class CreateOrUpdateOrderDtoGenerator
{
    public static CreateOrUpdateOrderDto GenerateCartWith3Items()
    {
        return new CreateOrUpdateOrderDto()
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