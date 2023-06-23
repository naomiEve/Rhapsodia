namespace Rhapsodia.Real.Differentiation;

/// <summary>
/// A differentiable real function.
/// </summary>
public interface IRealDifferentiable
{
    /// <summary>
    /// Is the variable pseudo-constant when differentiated with respect to a given variable.
    /// </summary>
    /// <param name="var">The variable.</param>
    /// <returns>If it's constant or not.</returns>
    bool IsPseudoConstant(RealVariable var);

    /// <summary>
    /// Differentiates a function with respect to some variable.
    /// </summary>
    /// <param name="var">The variable.</param>
    /// <returns>The differentiated function.</returns>
    IRealDifferentiable DifferentiateWithRespectTo(RealVariable var);
}
