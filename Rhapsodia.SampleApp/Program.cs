using Rhapsodia.Real;
using Rhapsodia.Real.Extensions;
using Rhapsodia.Real.Functions;
using Rhapsodia.Real.Functions.Trigonometric;

var x = new RealVariable("x", 5);
var y = new RealVariable("y", 7);
RealFunction f = new Sin(x ^ 2d.ToRealConstant()) ^ 6d.ToRealConstant();

while (true)
{
    Console.WriteLine($"{f} => {f.Evaluate()}");
    Console.ReadKey();
    f = (f.DifferentiateWithRespectTo(x) as RealFunction)!;
}
