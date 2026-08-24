using SharedSubsystem.Abstraction;

namespace ProductManagement.DTO;

/// <summary>
/// Local query marker for ProductManagement bounded context.
/// Extends the shared <see cref="SharedSubsystem.Abstraction.IQuery{TResult}"/> contract.
/// </summary>
public interface IQuery<TResult> : SharedSubsystem.Abstraction.IQuery<TResult>
{
}
