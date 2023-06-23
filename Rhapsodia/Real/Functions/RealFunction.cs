using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;
using Rhapsodia.Real.Functions.Arithmetic;

namespace Rhapsodia.Real.Functions;

/// <summary>
/// A real function.
/// </summary>
public abstract class RealFunction : IRealFunction
{
    /// <inheritdoc/>
    public abstract IRealDifferentiable DifferentiateWithRespectTo(RealVariable var);
    
    /// <inheritdoc/>
    public abstract double? Evaluate();
    
    /// <inheritdoc/>
    public abstract bool IsPseudoConstant(RealVariable var);

    /// <summary>
    /// Adds two functions together.
    /// </summary>
    /// <param name="left">The left function.</param>
    /// <param name="right">The right function.</param>
    /// <returns>The new function.</returns>
    public static RealFunction operator+(RealFunction left, RealFunction right)
    {
        return new Arithmetic.Addition()
            .PushArgument(left)
            .PushArgument(right);
    }

    /// <summary>
    /// Subtracts one function from another.
    /// </summary>
    /// <param name="left">The left function.</param>
    /// <param name="right">The right function.</param>
    /// <returns>The new function.</returns>
    public static Addition operator-(RealFunction left, RealFunction right)
    {
        return (Addition)new Addition()
            .PushArgument(left)
            .PushArgument(-right);
    }

    /// <summary>
    /// Raises one function to another's power.
    /// </summary>
    /// <param name="left">The left function.</param>
    /// <param name="right">The right function.</param>
    /// <returns>The new function.</returns>
    public static Power operator ^(RealFunction left, RealFunction right)
    {
        return new Power(left, right);
    }

    /// <summary>
    /// Negates this function.
    /// </summary>
    /// <param name="self">The function.</param>
    /// <returns>The negated function.</returns>
    public static RealFunction operator -(RealFunction self)
    {
        return (self.Negate() as RealFunction)!;
    }

    /// <summary>
    /// Multiplies two functions together.
    /// </summary>
    /// <param name="left">The left function.</param>
    /// <param name="right">The right function.</param>
    /// <returns>The new function.</returns>
    public static Multiplication operator *(RealFunction left, RealFunction right)
    {
        return (Multiplication)new Multiplication()
            .PushArgument(left)
            .PushArgument(right);
    }
}
