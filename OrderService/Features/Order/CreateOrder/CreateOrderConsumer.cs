using BuildingBlocks.Events;
using MassTransit;
using OrderService.Models;

namespace OrderService.Features.Order.CreateOrder;

public class CreateOrderConsumer(IDocumentSession session) : IConsumer<CartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        var order = context.Message.Adapt<Models.Order>();
        order.Address = context.Message.Adapt<OrderAddress>();
        order.Items = context.Message.Items.Adapt<List<OrderItem>>();

        session.Store(order);
        await session.SaveChangesAsync();
    }
}
