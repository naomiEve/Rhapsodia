using Rhapsodia.Definitions;

namespace Rhapsodia.Real.Extensions;

/// <summary>
/// Double extensions.
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    /// Converts a value to a real constant.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The resulting constant.</returns>
    public static RealConstant ToRealConstant(this double value)
    {
        var maybeConstant = ConstantPool<double>.Get(value);
        if (maybeConstant != null)
            return (RealConstant)maybeConstant;

        var constant = new RealConstant(value);
        ConstantPool<double>.Add(constant);

        return constant;
    }
}
