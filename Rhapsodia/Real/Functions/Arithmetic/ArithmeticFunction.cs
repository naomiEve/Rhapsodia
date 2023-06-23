using System.Text;
using Rhapsodia.Real.Extensions;

namespace Rhapsodia.Real.Functions.Arithmetic;

/// <summary>
/// A base class for all arithmetic operations.
/// </summary>
public abstract class ArithmeticFunction : RealFunction
{
    /// <summary>
    /// The operator for this function.
    /// </summary>
    public virtual string Operator { get; } = "?";

    /// <summary>
    /// The value the accumulator should begin at.
    /// </summary>
    internal virtual double AccumulatorBeginValue { get; } = 0d;

    /// <summary>
    /// The list of functions.
    /// </summary>
    public List<RealFunction> Arguments { get; private set; }

    /// <summary>
    /// Creates a new arithmetic function.
    /// </summary>
    public ArithmeticFunction()
    {
        Arguments = new();
    }

    /// <summary>
    /// Pushes a function into the argument stack.
    /// </summary>
    public ArithmeticFunction PushArgument(RealFunction func)
    {
        if (func.GetType() == GetType())
            ((ArithmeticFunction)func).Arguments.ForEach(arg => Arguments.Add(arg));
        else
            Arguments.Add(func);

        return this;
    }

    /// <summary>
    /// Apply this operator on two doubles.
    /// </summary>
    /// <param name="x">The left double.</param>
    /// <param name="y">The right double.</param>
    /// <returns>The applied value.</returns>
    protected abstract double Apply(double x, double y);

    /// <summary>
    /// Merges all of the constants.
    /// </summary>
    public void MergeConstants()
    {
        var merge = Arguments.Where(arg => arg is RealConstant)
            .Select(constant => constant as RealConstant)
            .Select(constant => constant!.Value)
            .Aggregate(AccumulatorBeginValue, (acc, val) => Apply(acc, val));

        Arguments.RemoveAll(arg => arg is RealConstant);

        // If the merged constants equal the neutral accumulator value
        // (ergo, adding it to the stack does nothing), we can just skip it.
        if (merge == AccumulatorBeginValue)
            return;

        PushArgument(merge.ToRealConstant());
    }

    /// <summary>
    /// Returns if this function only consists of constants.
    /// </summary>
    /// <returns>Whether it only consists of constants.</returns>
    protected bool IsOnlyConstants()
    {
        return Arguments.All(arg => arg is RealConstant);
    }

    /// <inheritdoc/>
    public override bool IsPseudoConstant(RealVariable var)
    {
        return Arguments.All(arg => arg.IsPseudoConstant(var));
    }

    /// <inheritdoc/>
    public override double? Evaluate()
    {
        if (Arguments.Count < 1)
            return 0;

        var accumulator = AccumulatorBeginValue;
        foreach (var func in Arguments)
        {
            var value = func.Evaluate();
            if (value is null)
                return null;

            accumulator = Apply(accumulator, value.Value);
        }

        return accumulator;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (Arguments.Count == 0)
            return string.Empty;

        if (Arguments.Count == 1)
            return $"{Operator}{Arguments[0]}";

        var sb = new StringBuilder();
        sb.Append('(');

        for (var i = 0; i < Arguments.Count - 1; i++)
        {
            sb.Append(Arguments[i].ToString())
                .Append(' ')
                .Append(Operator)
                .Append(' ');
        }

        sb.Append(Arguments.Last().ToString())
            .Append(')');
        return sb.ToString();
    }
}
