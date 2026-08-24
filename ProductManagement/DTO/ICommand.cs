using SharedSubsystem.Abstraction;

namespace ProductManagement.DTO;

/// <summary>
/// Local command marker for ProductManagement bounded context.
/// Extends the shared <see cref="SharedSubsystem.Abstraction.ICommand"/> contract.
/// </summary>
public interface ICommand : SharedSubsystem.Abstraction.ICommand
{
}
