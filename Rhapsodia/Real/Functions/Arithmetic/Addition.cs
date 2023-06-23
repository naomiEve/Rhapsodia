using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Arithmetic;

/// <summary>
/// Addition.
/// </summary>
public class Addition : ArithmeticFunction
{
    /// <inheritdoc/>
    public override string Operator => "+";

    /// <inheritdoc/>
    protected override double Apply(double x, double y) => x + y;

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        RealFunction dummy = 0d.ToRealConstant();

        // Add all differentiated arguments together.
        var add = (Addition)Arguments.Aggregate(dummy,
            (s, func) => s + (RealFunction)func.DifferentiateWithRespectTo(var));

        add.MergeConstants();
        
        // If the addition is only a constant, we can just return a constant here.
        if (add.IsOnlyConstants())
            return add.Evaluate()!.Value.ToRealConstant();

        // If we only have one argument, we can return it as-is.
        if (add.Arguments.Count == 1)
            return add.Arguments[0];

        return add;
    }
}
