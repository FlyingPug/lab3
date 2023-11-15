namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;

    public static class Solver
    {
        public static IList<double> Solve(double a, double b, double c)
        {
            double d = (b * b) - (4 * a * c);

            if (Math.Abs(d) < 1e-10)
            {
                return new[] { -b / (2 * a) };
            }
            else if (d > 0)
            {
                d = Math.Sqrt(d);
                return new[] { (-b - d) / (2 * a), (-b + d) / (2 * a) };
            }

            return Array.Empty<double>();
        }
    }
}