using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The cotangent function.
/// </summary>
public class Cot : CompositeRealFunction
{
    /// <summary>
    /// Creates a new cotangent function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Cot(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Cos(inner.Value) / Math.Sin(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return -(new Csc(f) ^ 2d.ToRealConstant());
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Cot";
    }
}
