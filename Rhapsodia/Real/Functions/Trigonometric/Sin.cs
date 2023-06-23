using Rhapsodia.Real.Differentiation;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The sine function.
/// </summary>
public class Sin : CompositeRealFunction
{
    /// <summary>
    /// Creates a new sine function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Sin(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Sin(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return new Cos(f);
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Sin";
    }
}
