using Rhapsodia.Real.Differentiation;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// A function of a real variable.
/// </summary>
public interface IRealFunction : IRealDifferentiable
{
    /// <summary>
    /// Evaluates the function.
    /// </summary>
    /// <returns>The value, or nothing.</returns>
    double? Evaluate();
}
