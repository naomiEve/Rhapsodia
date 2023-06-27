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
    /// Returns the taylor expansion for the given function, relative to a point a, and a variable x.
    /// </summary>
    /// <param name="a">The point.</param>
    /// <param name="var">The variable the function will be differentiated in respect to.</param>
    /// <param name="terms">How many terms of the taylor expansion should be computed.</param>
    /// <returns>The taylor expansion for this function.</returns>
    public RealFunction TaylorExpansion(int a, RealVariable var, int terms)
    {
        // Temporarily set the value of the variable to a.
        var prevVal = var.Value;
        var.Value = a;

        var f = new Addition();
        f.PushArgument(Evaluate()!.Value.ToRealConstant());

        if (terms == 0)
        {
            // Reset the value.
            var.Value = prevVal;
            return f;
        }

        var differential = DifferentiateWithRespectTo(var) as RealFunction;
        var factorial = 1d;

        for (var i = 1; i < terms + 1; i++)
        {
            var multiplication = new Multiplication();
            multiplication.PushArgument(differential!.Evaluate()!.Value.ToRealConstant());
            multiplication.PushArgument(factorial.ToRealConstant() ^ -1d.ToRealConstant());
            
            multiplication.PushArgument((var - ((double)a).ToRealConstant()).ReduceIfPossible() ^ ((double)i).ToRealConstant());

            // Discard this if it has any zeroes.
            if (!multiplication.HasZeroes())
                f.PushArgument(multiplication.ReduceIfPossible());

            factorial *= (i + 1);

            differential = differential.DifferentiateWithRespectTo(var) as RealFunction;
        }

        // Reset the value.
        var.Value = prevVal;

        return f.ReduceIfPossible();
    }

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
