﻿using AdventOfCode.CSharp.Common;
using System;

namespace AdventOfCode.CSharp.Y2015.Solvers;

public class Day01 : ISolver
{
    public Solution Solve(ReadOnlySpan<char> input)
    {
        int currentFloor = 0;
        int firstBasementFloor = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                currentFloor++;
            }
            else
            {
                currentFloor--;
                if (currentFloor == -1 && firstBasementFloor == 0)
                {
                    firstBasementFloor = i + 1;
                }
            }
        }

        return new Solution(part1: currentFloor, part2: firstBasementFloor);
    }
}
