using Rhapsodia.Real.Differentiation;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The secant function.
/// </summary>
public class Sec : CompositeRealFunction
{
    /// <summary>
    /// Creates a new secant function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Sec(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return 1 / Math.Cos(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return new Tan(f) * new Sec(f);
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Sec";
    }
}
