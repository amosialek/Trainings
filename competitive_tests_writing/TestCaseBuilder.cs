// ════════════════════════════════════════════════════════════════════════════════
// PROBLEM: Minimum Removals to Make Array "Good"
// ════════════════════════════════════════════════════════════════════════════════
//
// Given an array A of N integers and a non-negative integer D:
//
//   The array is "good" if:  max(A) - min(A) <= D
//
// You may remove any elements from the array (the remaining subset need not be
// contiguous). Compute the MINIMUM number of elements that must be removed so
// that the remaining array is good.
//
// Constraints:
//   1 <= N <= 10^6 (or larger)
//   D >= 0
//   A[i] may be negative, and values may exceed int32 range, but cannot exceed long type.
//
// ════════════════════════════════════════════════════════════════════════════════

// -----------------------------------------------------------------------
// Test-case definitions
// -----------------------------------------------------------------------

using System;
using System.Linq;

public class TestCaseBuilder
{
    public static TestCase[] BuildTestCases()
    {
        var tests = new TestCase[]
        {
            // 1) Basic case
            new TestCase("Basic", new long[] { 2 }, 2),
            new TestCase("your test case name", new long[]{ /*TODO, fill array of numbers here*/}, /*TODO: set D here*/0),
            // TODO: add more test cases here
        
        };
        return tests;
    }

}


