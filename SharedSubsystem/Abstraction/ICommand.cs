namespace SharedSubsystem.Abstraction;


public interface ICommand
{

    Guid CorrelationId { get; }
}
