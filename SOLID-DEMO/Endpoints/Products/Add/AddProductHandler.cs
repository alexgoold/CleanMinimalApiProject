﻿using AutoMapper;
using Domain;

namespace Server.Endpoints.Products.Add;

public class AddProductHandler
{
	private readonly IMapper _mapper;

	public AddProductHandler(IMapper mapper)
	{
		_mapper = mapper;
	}
	public async Task<IResult> Handle(AddProductRequest request, CancellationToken cancellationToken)
	{
		var product = _mapper.Map<Product>(request.ProductDto);
		if (string.IsNullOrEmpty(product.Name))
		{
			return Results.BadRequest();
		}

		await request.UnitOfWork.Products.AddAsync(product);

		return Results.Ok();
	}
	
}