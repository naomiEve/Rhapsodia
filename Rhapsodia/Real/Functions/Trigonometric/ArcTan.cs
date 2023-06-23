using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The inverse tangent function.
/// </summary>
public class ArcTan : CompositeRealFunction
{
    /// <summary>
    /// Constructs a new arctangent.
    /// </summary>
    /// <param name="inner">The inner function.</param>
    public ArcTan(IRealFunction inner) 
        : base(inner)
    {
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Atan(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return (1d.ToRealConstant() + (f ^ 2d.ToRealConstant())) ^ -1d.ToRealConstant();
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "ArcTan";
    }
}
