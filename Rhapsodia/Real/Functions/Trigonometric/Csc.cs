using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhapsodia.Real.Differentiation;

namespace Rhapsodia.Real.Functions.Trigonometric;

/// <summary>
/// The cosecant function.
/// </summary>
public class Csc : CompositeRealFunction
{
    /// <summary>
    /// Creates a new cosecant function.
    /// </summary>
    /// <param name="outer">The outer function.</param>
    public Csc(IRealFunction outer)
        : base(outer)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return 1 / Math.Sin(inner.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return -(new Cot(f) * new Csc(f));
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Csc";
    }
}
