﻿using Application.UnitOfWork;
using Shared;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.AddToOrder;

public class AddToOrderRequest : IHttpRequest
{ 
	public Guid OrderId { get; set; }
	public PlaceOrderDto Cart { get; set; }
	public IUnitOfWork UnitOfWork { get; set; }
}