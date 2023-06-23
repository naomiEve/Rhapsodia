using Rhapsodia.Definitions;
using Rhapsodia.Real.Extensions;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Functions;

namespace Rhapsodia.Real;

/// <summary>
/// A real-valued constant.
/// </summary>
public class RealConstant : RealFunction,
    IConstant<double>
{
    /// <inheritdoc/>
    public double Value { get; init; }

    /// <summary>
    /// Creates a constant.
    /// </summary>
    /// <param name="value">The value of the constant.</param>
    public RealConstant(double value)
    {
        Value = value;
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        return Value;
    }

    /// <inheritdoc/>
    public override bool IsPseudoConstant(RealVariable var)
    {
        // A constant is always constant.
        return true;
    }

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        return 0d.ToRealConstant();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }
}
