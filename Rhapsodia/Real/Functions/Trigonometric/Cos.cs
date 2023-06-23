using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The cosine function.
/// </summary>
public class Cos : CompositeRealFunction
{
    /// <summary>
    /// Creates a new cosine function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Cos(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Cos(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return -new Sin(f);
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Cos";
    }
}
