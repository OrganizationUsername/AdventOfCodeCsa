﻿using AdventOfCode.CSharp.Common;
using AdventOfCode.CSharp.Y2023.Solvers;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.CSharp.Benchmarks;

[GenericTypeArguments(typeof(Day01))]
[GenericTypeArguments(typeof(Day02))]
[GenericTypeArguments(typeof(Day03))]
[GenericTypeArguments(typeof(Day04))]
[GenericTypeArguments(typeof(Day05))]
[GenericTypeArguments(typeof(Day06))]
[GenericTypeArguments(typeof(Day07))]
[GenericTypeArguments(typeof(Day08))]
public class Y2023Solver<TSolver> : SolverBenchmarkBase<TSolver> where TSolver : ISolver
{
}
