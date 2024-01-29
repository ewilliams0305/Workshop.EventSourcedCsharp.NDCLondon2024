using BeerSender.Domain.Boxes.Handlers;

namespace BeerSender.Domain.Boxes;

public class Box : IAggregate
{
    public Capacity? Capacity { get; private set; }

    public void Apply(object @event)
    {
        throw new Exception($"Event type {@event.GetType()} not implemented for {this.GetType()}.");
    }

    public void Apply(Box_created @event)
    {
        Capacity = @event.Capacity;
    }
}