using Rhapsodia.Real.Functions;
using Rhapsodia.Real.Functions.Arithmetic;

namespace Rhapsodia.Real.Extensions;

/// <summary>
/// Real function extensions.
/// </summary>
public static class IRealFunctionExtensions
{
    /// <summary>
    /// Negates a function.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The negated function.</returns>
    public static IRealFunction Negate(this IRealFunction function)
    {
        // If this is a negation, just return the base value.
        if (function is Negation negation)
        {
            return negation.Arguments[0];
        }

        // If the function is a constant, just negate it.
        else if (function is RealConstant c)
        {
            return (-c.Value).ToRealConstant();
        }
        
        // If the function is an addition, invert each field.
        else if (function is Addition add)
        {
            var subbed = (Addition)add.Arguments.Aggregate((RealFunction)0d.ToRealConstant(), 
                (acc, val) => acc + (RealFunction)val.Negate());
            subbed.MergeConstants();

            return subbed;
        }

        return new Negation()
            .PushArgument((function as RealFunction)!);
    }
}
