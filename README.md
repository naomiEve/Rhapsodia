# Rhapsodia

Rhapsodia is a simple and (pretty much useless) symbollic algebra system library written in C#, partially for my Mathematics for Computer Scientists course.

So far only supporting real valued functions, it allows the end user to model a mathematical function using only classes.

A sample Rhapsodia function can look something like this:

```cs
var x = new Variable("x");
var y = new Variable("y");

var function = (new Ln(new Cos(x)) ^ 2d.ToRealConstant()) - (y * new Sin(5d.ToRealConstant() + x));
```

The code above models this function: `Ln[Pow[Cos[x], 2]] - (y * Sin[5 + x])`.

It then allows the user to either evaluate the function, or take its derivative with respect to a given variable by calling the `.DifferentiateWithRespectTo(Variable)` method on the function.