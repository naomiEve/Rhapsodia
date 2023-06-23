using Rhapsodia.Real.Functions;

namespace Rhapsodia.Real.Differentiation;

/// <summary>
/// Thrown when we can't differentiate this function.
/// </summary>
public class UnableToDifferentiateException : Exception
{
    public UnableToDifferentiateException(IRealFunction func, string cause)
        : base($"Unable to differentiate function {func}. Reason: {cause}")
    {

    }
}
