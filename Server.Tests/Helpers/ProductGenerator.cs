using Domain;
using Shared.ProductsDtos;

namespace Tests.Helpers;

public static class ProductGenerator
{
    public static Product GenerateProduct()
    {
        return new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Abaddon The Despoiler",
            Description = "Abaddon the Despoiler is the ultimate Chaos character."

        };
    }

    public static List<Product> GenerateListOf3Products()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Milk",
                Description = "This is milk"

            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Chaos Terminators",
                Description = "Heavily-armoured Elites for Chaos Space Marines and World Eaters armies"
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Rubric Marines",
                Description = "This multi-part plastic kit contains all the parts necessary to assemble 10 Rubric Marines,"
            }

        };
    }

    public static List<ProductDto> GenerateListOf3ProductDtos()
    {
		return new List<ProductDto>()
		{
			new ProductDto()
			{
				Id = Guid.NewGuid(),
				Name = "Milk",
				Description = "This is milk"

			},
			new ProductDto()
			{
				Id = Guid.NewGuid(),
				Name = "Chaos Terminators",
				Description = "Heavily-armoured Elites for Chaos Space Marines and World Eaters armies"
			},
			new ProductDto()
			{
				Id = Guid.NewGuid(),
				Name = "Rubric Marines",
				Description = "This multi-part plastic kit contains all the parts necessary to assemble 10 Rubric Marines,"
			}

		};
	}

}