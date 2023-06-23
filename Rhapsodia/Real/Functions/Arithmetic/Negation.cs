using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Arithmetic;

/// <summary>
/// Negation of a value.
/// </summary>
public class Negation : ArithmeticFunction
{
    /// <inheritdoc/>
    public override string Operator => "-";

    /// <inheritdoc/>
    protected override double Apply(double x, double y) => x - y;
    
    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        var diff = (RealFunction)Arguments[0].DifferentiateWithRespectTo(var);
        if (diff is Negation neg)
            return neg.Arguments[0];
        
        return diff.Negate();
    }
}
