using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// The exponentiation function (e^x).
/// </summary>
public class Exp : CompositeRealFunction
{
    /// <summary>
    /// Construct a new exponentiation function.
    /// </summary>
    /// <param name="inner">The inner function.</param>
    public Exp(IRealFunction inner)
        : base(inner)
    {

    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var innerValue = Inner.Evaluate();
        if (!innerValue.HasValue)
            return null;

        return Math.Exp(innerValue.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return new Exp(f);
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return "Exp";
    }
}
