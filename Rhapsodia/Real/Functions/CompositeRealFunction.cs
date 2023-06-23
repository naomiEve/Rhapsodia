using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// A composite function.
/// </summary>
public abstract class CompositeRealFunction : RealFunction,
    ICompositeFunction
{
    /// <inheritdoc/>
    public IRealFunction Inner { get; init; }

    /// <summary>
    /// Constructs a new composite function with the given inner function.
    /// </summary>
    /// <param name="inner">The inner function.</param>
    public CompositeRealFunction(IRealFunction inner)
    {
        Inner = inner;
    }

    /// <summary>
    /// Differentiates this function with a given inner function.
    /// </summary>
    /// <param name="f">The inner function.</param>
    protected abstract IRealDifferentiable DifferentiateWithInnerAs(RealFunction f);

    /// <summary>
    /// Returns the outer function as text.
    /// </summary>
    /// <returns></returns>
    protected abstract string OuterFunctionToText();

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        // If the inner value is a pseudo constant, we can safely return 0.
        if (IsPseudoConstant(var))
            return 0d.ToRealConstant();

        var inner = Inner as RealFunction;

        // If the inner function is the variable we're differentiating with respect to,
        // we can just already return the differentiated form.
        if (Inner == var)
            return DifferentiateWithInnerAs(inner!);

        // Otherwise, we need to use the chain rule.
        return (RealFunction)DifferentiateWithInnerAs(inner!) * (RealFunction)inner!.DifferentiateWithRespectTo(var);
    }

    /// <inheritdoc/>
    public override bool IsPseudoConstant(RealVariable var)
    {
        return Inner.IsPseudoConstant(var);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{OuterFunctionToText()}[{Inner}]";
    }
}
