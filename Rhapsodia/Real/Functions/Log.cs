using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// The logarithm function.
/// </summary>
public class Log : CompositeRealFunction
{
    /// <summary>
    /// The base of the logarithm.
    /// </summary>
    public IRealFunction Base { get; init; }

    /// <summary>
    /// Constructs a new logarithm.
    /// </summary>
    /// <param name="base">The base of the logarithm.</param>
    /// <param name="inner">The inner value</param>
    public Log(IRealFunction @base, IRealFunction inner)
        : base(inner)
    {
        Base = @base;
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var inner = Inner.Evaluate();
        if (!inner.HasValue)
            return null;

        return Math.Log(inner.Value, Base.Evaluate()!.Value);
    }

    /// <inheritdoc/>
    protected override IRealDifferentiable DifferentiateWithInnerAs(RealFunction f)
    {
        return ((RealFunction)Inner * new Ln(Base)) ^ -1d.ToRealConstant();
    }

    /// <inheritdoc/>
    protected override string OuterFunctionToText()
    {
        return $"Log{Base.Evaluate()}";
    }
}
