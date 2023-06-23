using Rhapsodia.Real.Differentiation;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Arithmetic;

/// <summary>
/// The multiplication operator.
/// </summary>
public class Multiplication : ArithmeticFunction
{
    /// <inheritdoc/>
    public override string Operator => "*";

    /// <inheritdoc/>
    internal override double AccumulatorBeginValue => 1d;

    /// <inheritdoc/>
    protected override double Apply(double x, double y) => x * y;

    /// <inheritdoc/>
    public override IRealDifferentiable DifferentiateWithRespectTo(RealVariable var)
    {
        RealFunction addStack = new Addition();

        for (var i = 0; i < Arguments.Count; i++)
        {
            var arg = Arguments[i];
            var mul = (arg.DifferentiateWithRespectTo(var) as RealFunction)!;

            for (var j = 0; j < Arguments.Count; j++)
            {
                if (i == j)
                    continue;

                mul *= Arguments[j];
            }

            if (mul is Multiplication mul2)
            {
                // Merge constants and check if we have a zero.
                mul2.MergeConstants();

                if (mul2.IsOnlyConstants())
                {
                    var value = mul2.Evaluate()!.Value;
                    if (value == 0)
                        continue;

                    addStack += value
                        .ToRealConstant();
                }
                else
                {
                    // If we have a zero constant, there's no need to add it to the addition stack.
                    if (mul2.Arguments.Any(arg => arg is RealConstant rc && rc.Value == 0))
                        continue;

                    // If the multiplication only has one element, we can add that element.
                    if (mul2.Arguments.Count == 1)
                        addStack += mul2.Arguments[0];
                    else
                        addStack += mul;
                }
            }
            else
            {
                // I've no idea how we'd end up here, but it once happened.
                addStack += mul;
            }
        }

        var stack = (Addition)addStack;
        stack.MergeConstants();

        if (stack.Arguments.Count == 1)
            return stack.Arguments[0];

        return addStack;
    }
}
