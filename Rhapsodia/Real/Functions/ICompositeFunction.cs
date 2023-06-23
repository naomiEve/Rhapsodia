namespace Rhapsodia.Real.Functions;

/// <summary>
/// A function that consists of an inner function and an outer function.
/// </summary>
public interface ICompositeFunction : IRealFunction
{
    /// <summary>
    /// The inner function.
    /// </summary>
    IRealFunction Inner { get; }
}
