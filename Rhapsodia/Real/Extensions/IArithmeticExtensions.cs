using Rhapsodia.Real.Functions;
using Rhapsodia.Real.Functions.Arithmetic;

namespace Rhapsodia.Real.Extensions;

/// <summary>
/// Extensions for the arithmetic operations.
/// </summary>
public static class IArithmeticExtensions
{
    /// <summary>
    /// Tries to reduce an arithmetic expression if possible.
    /// </summary>
    /// <param name="func">The function.</param>
    /// <returns>The reduced function.</returns>
    public static RealFunction ReduceIfPossible(this ArithmeticFunction func)
    {
        // None of this applies to negation, as it's an unary operation.
        if (func is Negation)
            return func;

        // First, merge all constants.
        func.MergeConstants();

        // Now, check if we only have one element, if so return it.
        if (func.Arguments.Count == 1)
            return func.Arguments[0];

        // Otherwise, try to reduce all the elements inside.
        for (var i = 0; i < func.Arguments.Count; i++)
        {
            if (func.Arguments[i] is ArithmeticFunction arith)
                func.Arguments[i] = arith.ReduceIfPossible();
        }

        return func;
    }
}
