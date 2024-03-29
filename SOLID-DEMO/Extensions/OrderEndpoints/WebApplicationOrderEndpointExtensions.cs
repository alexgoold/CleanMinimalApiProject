﻿using Server.Endpoints.Orders.AddToOrder;
using Server.Endpoints.Orders.CancelOrder;
using Server.Endpoints.Orders.GetAll;
using Server.Endpoints.Orders.GetOrdersByCustomerId;
using Server.Endpoints.Orders.PlaceOrder;
using Server.Endpoints.Orders.RemoveFromOrder;

namespace Server.Extensions.OrderEndpoints;

public static class WebApplicationOrderEndpointExtensions
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        app.MediateGet<GetAllOrdersRequest>("orders/getAllOrders");
        app.MediateGet<GetOrdersByCustomerIdRequest>("orders/getOrderByCustomerId");
        app.MediatePost<PlaceOrderRequest>("orders/placeOrder");
        app.MediateDelete<CancelOrderRequest>("orders/cancelOrder");
        app.MediatePut<AddToOrderRequest>("orders/addToOrder");
        app.MediatePut<RemoveFromOrderRequest>("orders/removeFromOrder");
        return app;
    }
}