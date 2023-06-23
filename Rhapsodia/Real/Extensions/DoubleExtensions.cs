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
        return new RealConstant(value);
    }
}
