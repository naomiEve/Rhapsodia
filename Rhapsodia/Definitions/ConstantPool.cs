namespace Rhapsodia.Definitions;

/// <summary>
/// A pool for constant values (so we don't get 100k of the same constant on the heap.)
/// </summary>
/// <typeparam name="T"></typeparam>
internal static class ConstantPool<T>
    where T: IEquatable<T>
{
    /// <summary>
    /// The constant pool.
    /// </summary>
    private static HashSet<IConstant<T>> _pool;

    /// <summary>
    /// Constructor for the pool.
    /// </summary>
    static ConstantPool()
    {
        _pool ??= new();
    }

    /// <summary>
    /// Gets the constant for a given value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The constant.</returns>
    public static IConstant<T>? Get(T value)
    {
        return _pool.FirstOrDefault(c => c.Value.Equals(value));
    }

    /// <summary>
    /// Adds a constant to the constant pool.
    /// </summary>
    /// <param name="value">The constant.</param>
    public static void Add(IConstant<T> value)
    {
        _pool.Add(value);
    }
}
