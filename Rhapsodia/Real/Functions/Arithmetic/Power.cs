using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Arithmetic;

/// <summary>
/// The power function.
/// </summary>
public class Power : RealFunction
{
    /// <summary>
    /// The base of the power.
    /// </summary>
    public IRealFunction Base { get; init; }

    /// <summary>
    /// The exponent of the power.
    /// </summary>
    public IRealFunction Exponent { get; init; }

    /// <summary>
    /// Create a new power function.
    /// </summary>
    /// <param name="base">The base.</param>
    /// <param name="exponent">The exponent.</param>
    public Power(IRealFunction @base, IRealFunction exponent)
    {
        Base = @base;
        Exponent = exponent;
    }

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        var isBaseConstant = Base.IsPseudoConstant(var);
        var isExponentConstant = Exponent.IsPseudoConstant(var);

        // Simplest case, we have a normal constant to a constant's power.
        // That's just a constant in of itself. So the derivative is 0.
        if (isBaseConstant && isExponentConstant)
        {
            if (Base.Evaluate() == 0 && Exponent.Evaluate() == 0)
                throw new UnableToDifferentiateException(this, "Found expression evaluating to 0^0.");

            return 0d.ToRealConstant();
        }

        // This is a function of type function to a constant's power.
        // We use the power rule, doing (x^n)' = (n)*x^(n-1)
        else if (!isBaseConstant && isExponentConstant)
        {
            var exponentVal = Exponent.Evaluate()!.Value;
            
            // A value to 0 will always be one (unless it's 0^0).
            if (exponentVal == 0)
            {
                if (Base.Evaluate() == 0)
                    throw new UnableToDifferentiateException(this, "Found expression evaluating to 0^0.");

                return 1d.ToRealConstant();
            }

            var newExponent = ((RealFunction)Exponent - 1d.ToRealConstant())
                .ReduceIfPossible();

            var multiply = ((RealFunction)Exponent * ((RealFunction)Base ^ newExponent) * (RealFunction)Base.DifferentiateWithRespectTo(var))
                .ReduceIfPossible();

            return multiply;
        }

        // This is a function of type constant to a function's power.
        // This uses the formula (n^x)' = n^x(lnn)
        else if (isBaseConstant && !isExponentConstant)
        {
            throw new NotImplementedException();
        }

        // Otherwise, we have a function of type function to a function.
        // We need to use the logarithmic derivative.
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        var baseVal = Base.Evaluate();
        var exponentVal = Exponent.Evaluate();

        if (baseVal == null || exponentVal == null)
            return null;

        return Math.Pow(baseVal.Value, exponentVal.Value);
    }

    /// <inheritdoc/>
    public override bool IsPseudoConstant(RealVariable var)
    {
        return Base.IsPseudoConstant(var) && Exponent.IsPseudoConstant(var);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Pow[{Base}, {Exponent}]";
    }
}
