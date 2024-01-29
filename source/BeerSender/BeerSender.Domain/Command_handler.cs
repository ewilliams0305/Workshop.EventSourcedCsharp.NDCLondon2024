namespace BeerSender.Domain;

/// <summary>
/// Base class used for all command handler.  Provide the aggregate type as well as the command type.
/// </summary>
/// <typeparam name="TAggregate"></typeparam>
/// <typeparam name="TCommand"></typeparam>
public abstract class Command_handler<TAggregate, TCommand> where TAggregate : IAggregate
{
    private readonly IEnumerable<object> _event_stream;
    private readonly Action<object> _publish_event;

    /// <summary>
    /// Creates the new command handler and provides access to the infrastructure.
    /// </summary>
    /// <param name="event_stream"></param>
    /// <param name="publish_event"></param>
    protected Command_handler(
        IEnumerable<object> event_stream,
        Action<object> publish_event)
    {
        _event_stream = event_stream;
        _publish_event = publish_event;
    }

    /// <summary>
    /// Creates a new aggregate and processes the command. 
    /// </summary>
    /// <param name="command">Command to handle.</param>
    public void Handle(TCommand command)
    {
        var aggregate = CreateAggregate();

        foreach (var @event in _event_stream)
        {
            aggregate.Apply(@event);
        }

        var new_events = HandleInternal(aggregate, command);

        foreach (var new_event in new_events)
        {
            _publish_event(new_event);
        }
    }

    /// <summary>
    /// Handles the command and appends the command.
    /// </summary>
    /// <param name="aggregate">The aggregate</param>
    /// <param name="command">The command to handle</param>
    /// <returns></returns>
    protected abstract IEnumerable<object> HandleInternal(IAggregate aggregate, TCommand command);

    /// <summary>
    /// Factory method to return an instance of the Aggregate.
    /// </summary>
    /// <returns>An instance of the aggregate for which the command will be applied</returns>
    protected abstract TAggregate CreateAggregate();
}