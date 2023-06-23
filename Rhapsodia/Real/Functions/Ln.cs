using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// The natural logarithm.
/// </summary>
public class Ln : CompositeRealFunction
{
    /// <summary>
    /// Construct a new natural logarithm.
    /// </summary>
    /// <param name="inner">The inner function.</param>
    public Ln(IRealFunction inner)
        : base(inner)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var innerValue = Inner.Evaluate();
        if (!innerValue.HasValue)
            return null;

        return Math.Log(innerValue.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return f ^ -1d.ToRealConstant();
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Ln";
    }
}
