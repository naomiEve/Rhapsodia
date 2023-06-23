using Rhapsodia.Definitions;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;
using Rhapsodia.Real.Functions;

namespace Rhapsodia.Real;

/// <summary>
/// A real-valued variable.
/// </summary>
public class RealVariable : RealFunction, 
    IVariable<double>
{
    /// <inheritdoc/>
    public string Name { get; init; }

    /// <summary>
    /// The value of the variable.
    /// </summary>
    public double? Value { get; init; }

    /// <summary>
    /// Creates a new real-valued variable.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">Maybe a value.</param>
    public RealVariable(
        string name, 
        double? value = null)
    {
        Name = name;
        Value = value;
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        return Value;
    }

    /// <inheritdoc/>
    public override bool IsPseudoConstant(RealVariable var)
    {
        return this != var;
    }

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        return !IsPseudoConstant(var) ? 
            1d.ToRealConstant() : 
            0d.ToRealConstant();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Name;
    }
}
