namespace BeerSender.Domain.Boxes.Handlers;

public interface IAggregate
{
    void Apply(object @event);
}