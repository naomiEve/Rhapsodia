using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The tangent function.
/// </summary>
public class Tan : CompositeRealFunction
{
    /// <summary>
    /// Creates a new tangent function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Tan(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Tan(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return new Sec(f) ^ 2d.ToRealConstant();
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Tan";
    }
}
