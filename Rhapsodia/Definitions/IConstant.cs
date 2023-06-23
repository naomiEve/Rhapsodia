namespace Rhapsodia.Definitions;

/// <summary>
/// A constant.
/// </summary>
/// <typeparam name="TMathType">The inner type.</typeparam>
public interface IConstant<TMathType>
{
    /// <summary>
    /// The value.
    /// </summary>
    TMathType Value { get; }
}
