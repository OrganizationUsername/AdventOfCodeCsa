﻿using System.IO;
using System.Linq;
using AdventOfCode.CSharp.Common;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

#if DEBUG
var config = new DebugInProcessConfig();
#else
var config = DefaultConfig.Instance;
#endif
var results = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);

var reports = results.Select(ConvertSummaryToReport).ToList();

using var writer = new StreamWriter(File.Create("Benchmarks.md"));

foreach (var group in reports.GroupBy(r => r.Year))
{
    writer.WriteLine($"## {group.Key}");
    writer.WriteLine();
    writer.WriteLine("| Day | P0 | P50 | P100 | Allocations |");
    writer.WriteLine("|--- |---:|---:|---:|---:|");
    foreach (var r in group)
    {
        writer.WriteLine($"| **{r.Day}** | **{r.P0:N2} μs** | **{r.P50:N2} μs** | **{r.P100:N2} μs** | **{FormatAllocations(r.Allocations)}** |");
    }

    writer.WriteLine();
}

BenchmarkReport ConvertSummaryToReport(Summary summary)
{
    var report = summary.Reports[0];
    var solverType = report.BenchmarkCase.Descriptor.Type.GenericTypeArguments[0];
    (int year, int day) = SolverUtils.GetYearAndDay(solverType);
    var percentiles = report.ResultStatistics!.Percentiles;
    var allocations = report.Metrics.Single(m => m.Key.Equals("Allocated Memory")).Value.Value;
    return new BenchmarkReport(year, day, percentiles.P0 / 1000, percentiles.P50 / 1000, percentiles.P100 / 1000, allocations);
}

string FormatAllocations(double allocations) => allocations switch
{
    0 => "--",
    < 1_000 => $"{(int)allocations} B",
    < 1_000_000 => $"{allocations / 1_000:F1} KB",
    _ => $"{allocations / 1_000_000:N1} MB",
};

record BenchmarkReport(int Year, int Day, double P0, double P50, double P100, double Allocations);
