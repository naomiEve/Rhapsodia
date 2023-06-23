namespace Rhapsodia.Definitions;

/// <summary>
/// An interface for a variable.
/// </summary>
public interface IVariable<TMathType>
{
    /// <summary>
    /// The name of the variable.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The value of the variable.
    /// </summary>
    Nullable<double> Value { get; }
}
