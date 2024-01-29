namespace BeerSender.Domain.Boxes.Handlers;

public class Get_box_handler : Command_handler<Box, Get_box>
{
    /// <inheritdoc />
    public Get_box_handler(
        IEnumerable<object> event_stream, 
        Action<object> publish_event) 
        : base(event_stream, publish_event)
    {
    }

    #region Overrides of CommandHandler<Box,Get_box>

    /// <inheritdoc />
    protected override IEnumerable<object> HandleInternal(IAggregate aggregate, Get_box command)
    {
        var capacity = Capacity.Create(command.Desired_number_of_spots);
        yield return new Box_created(capacity);
    }

    /// <inheritdoc />
    protected override Box CreateAggregate()
    {
        return new Box();
    }

    #endregion
}
