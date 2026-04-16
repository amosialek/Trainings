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
//   A[i] may be negative, and values may exceed int32 range.
//
// ════════════════════════════════════════════════════════════════════════════════

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

// ---------------------------------------------------------------------------
// Solution interface
// ---------------------------------------------------------------------------

public interface ISolution
{
    /// <summary>
    /// Returns the minimum number of elements to remove so that
    /// max(remaining) - min(remaining) &lt;= D.
    /// </summary>
    int Solve(long[] A, long D);
}

// ═══════════════════════════════════════════════════════════════════════════════
// 1. PERFECT SOLUTION
// ═══════════════════════════════════════════════════════════════════════════════
//
// Algorithm:
//   1) Sort the array in O(N log N).
//   2) Use a two-pointer (sliding window) to find the largest window [left..right]
//      such that A[right] - A[left] <= D.
//   3) Answer = N - maxWindowSize.
//
// Correctness: after sorting, every contiguous subarray corresponds to taking
// the smallest-range subset of that size.  The two-pointer advances left only
// when the window violates the constraint, so it visits every maximal valid
// window exactly once.
//
// Time:  O(N log N)   (sort dominates)
// Space: O(N)         (copy for sorting — could be O(1) extra if in-place)
//
// Handles: duplicates, D=0, N=1, negatives, large values (int64 arithmetic).
// ═══════════════════════════════════════════════════════════════════════════════

public class PerfectSolution : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int maxKept = 1;
        int left = 0;

        for (int right = 0; right < n; right++)
        {
            while (sorted[right] - sorted[left] > D)
                left++;

            int windowSize = right - left + 1;
            if (windowSize > maxKept)
                maxKept = windowSize;
        }

        return n - maxKept;
    }
}

// ═══════════════════════════════════════════════════════════════════════════════
// 2. IMPERFECT SOLUTIONS
// ═══════════════════════════════════════════════════════════════════════════════

// ---------------------------------------------------------------------------
// 2a. ImperfectSolution_Overflow
// ---------------------------------------------------------------------------
//
// BUG: Casts the difference (A[right] - A[left]) to int before comparing to D.
//      When A contains values like 0 and 3_000_000_000, the difference is
//      3_000_000_000 which overflows int32 (max ~2.1 billion).  The overflowed
//      value wraps to a negative number, causing the while-condition to be
//      false when it should be true — the window never shrinks, and the
//      solution keeps elements it should remove.
//
// FAILS ON: Test cases with values whose range exceeds int32 limits.
//           Example: A = [0, 3_000_000_000], D = 1_000_000_000
//             Range = 3B > 1B, should remove 1, but (int)(3B) wraps negative
//             so the while‑loop never fires → answer 0 (wrong).
// ---------------------------------------------------------------------------

public class ImperfectSolution_Overflow : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int maxKept = 1;
        int left = 0;

        for (int right = 0; right < n; right++)
        {
            // BUG: casting to int causes overflow for large value ranges
            while (left < right && (int)(sorted[right] - sorted[left]) > (int)D)
                left++;

            int windowSize = right - left + 1;
            if (windowSize > maxKept)
                maxKept = windowSize;
        }

        return n - maxKept;
    }
}

// ---------------------------------------------------------------------------
// 2b. ImperfectSolution_WrongComparison
// ---------------------------------------------------------------------------
//
// BUG: Uses strict less-than (< D) instead of less-than-or-equal (<= D)
//      in the window validity check.  This means that a window where
//      max - min == D exactly is rejected, so the solution removes more
//      elements than necessary.
//      When D = 0 this becomes catastrophic: every pair of equal elements
//      has diff 0 >= 0, so the window shrinks to size 1 even for duplicates.
//
// FAILS ON: Any test where the optimal window has max - min == D exactly.
//           Example: A = [1, 3, 5], D = 4 → correct answer is 0 (keep all),
//           but this solution reports 1.
//           Also: A = [5, 5, 5], D = 0 → correct 0, this returns 2.
// ---------------------------------------------------------------------------

public class ImperfectSolution_WrongComparison : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int maxKept = 1;
        int left = 0;

        for (int right = 0; right < n; right++)
        {
            // BUG: should be > D, not >= D  (i.e., the window is valid when diff <= D)
            while (left < right && sorted[right] - sorted[left] >= D)
                left++;

            int windowSize = right - left + 1;
            if (windowSize > maxKept)
                maxKept = windowSize;
        }

        return n - maxKept;
    }
}

// ---------------------------------------------------------------------------
// 2c. ImperfectSolution_Quadratic
// ---------------------------------------------------------------------------
//
// BUG: Uses an O(N^2) nested loop instead of the O(N) two-pointer technique.
//      For each starting index, it linearly scans forward to find the end of
//      the valid window.  Produces correct results but is far too slow for
//      large inputs (N ~ 10^6 → ~10^12 operations).
//
// FAILS ON: Large-N test cases — exceeds the 1-second time limit.
//           Correct answers on small inputs.
// ---------------------------------------------------------------------------

public class ImperfectSolution_Quadratic : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int maxKept = 1;

        // BUG: O(N^2) — for each left, scan right from left forward
        for (int left = 0; left < n; left++)
        {
            for (int right = left; right < n; right++)
            {
                if (sorted[right] - sorted[left] <= D)
                {
                    int windowSize = right - left + 1;
                    if (windowSize > maxKept)
                        maxKept = windowSize;
                }
                else
                {
                    break; // sorted, so no point going further
                }
            }
        }

        return n - maxKept;
    }
}

// ---------------------------------------------------------------------------
// 2d. ImperfectSolution_UnsortedWindow
// ---------------------------------------------------------------------------
//
// BUG: Applies a sliding window on the ORIGINAL (unsorted) array, tracking
//      the running min and max of the current contiguous subarray.  This
//      finds the longest CONTIGUOUS subarray with max-min <= D, but the
//      problem allows removing ANY elements (not just a contiguous prefix
//      or suffix).  The optimal kept subset may be scattered across the
//      original array.
//
// FAILS ON: Cases where the optimal subset is non-contiguous in the
//           original ordering.
//           Example: A = [1, 100, 2, 3], D = 2
//             Correct: remove only 100 → keep {1, 2, 3}, answer = 1
//             This solution: best contiguous window is [2, 3] (size 2),
//             so it returns 2.
// ---------------------------------------------------------------------------

public class ImperfectSolution_UnsortedWindow : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        // BUG: working on unsorted array — finds best contiguous subarray,
        //      not best subset.
        int maxKept = 1;

        for (int left = 0; left < n; left++)
        {
            long curMin = A[left];
            long curMax = A[left];

            for (int right = left; right < n; right++)
            {
                if (A[right] < curMin) curMin = A[right];
                if (A[right] > curMax) curMax = A[right];

                if (curMax - curMin <= D)
                {
                    int windowSize = right - left + 1;
                    if (windowSize > maxKept)
                        maxKept = windowSize;
                }
                else
                {
                    break; // can't shrink window from the left in this approach
                }
            }
        }

        return n - maxKept;
    }
}

// ---------------------------------------------------------------------------
// 2e. ImperfectSolution_BinarySearchOffByOne
// ---------------------------------------------------------------------------
//
// BUG: Instead of two pointers, this uses sorting + Binary Search to find
//      the upper bound for each element. However, it uses `Array.BinarySearch`,
//      which returns the index of ANY match if there are duplicates, rather
//      than the LAST possible match (Upper Bound).
//
// FAILS ON: Test cases with many duplicate values at the tail of a valid window.
// ---------------------------------------------------------------------------

public class ImperfectSolution_BinarySearchOffByOne : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int maxKept = 1;
        for (int i = 0; i < n; i++)
        {
            long target = sorted[i] + D;
            int idx = Array.BinarySearch(sorted, target);
            
            // BUG: BinarySearch on [1, 2, 2, 2, 3] for target 2 might return 1, 2, or 3.
            // It doesn't guarantee the "last" index of the value, leading to 
            // smaller-than-optimal windows when duplicates exist at the boundary.
            if (idx < 0) idx = ~idx - 1;

            int windowSize = idx - i + 1;
            if (windowSize > maxKept) maxKept = windowSize;
        }
        return n - maxKept;
    }
}

// ---------------------------------------------------------------------------
// 2f. ImperfectSolution_GreedySkip
// ---------------------------------------------------------------------------
//
// BUG: Tries a greedy approach: sort the array, and if the current window
//      is invalid (max - min > D), it removes either the smallest or largest
//      element based on which one is further from the "center" of the range.
//
// FAILS ON: Cases where the optimal subset is at one end, but the other end
//           is "closer" to the middle, tricking the greedy choice.
// ---------------------------------------------------------------------------

public class ImperfectSolution_GreedySkip : ISolution
{
    public int Solve(long[] A, long D)
    {
        int n = A.Length;
        if (n <= 1) return 0;

        long[] sorted = (long[])A.Clone();
        Array.Sort(sorted);

        int left = 0;
        int right = n - 1;
        int removed = 0;

        while (left < right && sorted[right] - sorted[left] > D)
        {
            // BUG: Greedy choice based on which end is "further" from the mid-value.
            // This fails when a small cluster at one end is optimal, but the 
            // other end has a single value that's slightly further out.
            long mid = (sorted[left] + sorted[right]) / 2;
            if (Math.Abs(sorted[left] - mid) > Math.Abs(sorted[right] - mid))
                left++;
            else
                right--;
            
            removed++;
        }

        return removed;
    }
}

// ═══════════════════════════════════════════════════════════════════════════════
// 3. TEST FRAMEWORK
// ═══════════════════════════════════════════════════════════════════════════════

public class TestCase
{
    public string Name { get; }
    public long[] A { get; }
    public long D { get; }

    public TestCase(string name, long[] a, long d)
    {
        Name = name;
        A = a;
        D = d;
    }
}

public static class Program
{
    // Time limit for each solution run (milliseconds)
    private const int TimeLimitMs = 1000;
    // -----------------------------------------------------------------------
    // Test runner
    // -----------------------------------------------------------------------

    public static void Main()
    {
        var testCases = TestCaseBuilder.BuildTestCases();

        var solutions = new (string Name, ISolution Instance)[]
        {
            ("PerfectSolution",                   new PerfectSolution()),
            ("ImperfectSolution_1",        new ImperfectSolution_Overflow()),
            ("ImperfectSolution_2", new ImperfectSolution_WrongComparison()),
            ("ImperfectSolution_3",       new ImperfectSolution_Quadratic()),
            ("ImperfectSolution_4",  new ImperfectSolution_UnsortedWindow()),
            ("ImperfectSolution_5", new ImperfectSolution_BinarySearchOffByOne()),
            ("ImperfectSolution_6",      new ImperfectSolution_GreedySkip()),
        };

        // Track per-solution stats
        int[] totalPass = new int[solutions.Length];
        int[] totalFail = new int[solutions.Length];

        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("  MINIMUM REMOVALS — TEST RUNNER");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine();

        foreach (var tc in testCases)
        {
            Console.WriteLine($"── Test: {tc.Name}  (N={tc.A.Length}, D={tc.D})");

            // Run perfect solution first to get expected answer
            int expected;
            {
                var sw = Stopwatch.StartNew();
                expected = solutions[0].Instance.Solve((long[])tc.A.Clone(), tc.D);
                sw.Stop();
                Console.WriteLine($"   {"PerfectSolution",-38} => {expected,8}  ({sw.ElapsedMilliseconds,5} ms)  [EXPECTED]");
            }

            // Run each imperfect solution
            for (int s = 1; s < solutions.Length; s++)
            {
                string solName = solutions[s].Name;
                ISolution sol = solutions[s].Instance;

                string status;
                int result = -1;
                long elapsedMs = 0;

                try
                {
                    var inputCopy = (long[])tc.A.Clone();
                    long d = tc.D;

                    var sw = Stopwatch.StartNew();
                    var task = Task.Run(() => sol.Solve(inputCopy, d));
                    bool finished = task.Wait(TimeLimitMs);
                    sw.Stop();
                    elapsedMs = sw.ElapsedMilliseconds;

                    if (!finished)
                    {
                        status = "FAIL (TLE)";
                    }
                    else
                    {
                        result = task.Result;
                        status = (result == expected)
                            ? "PASS"
                            : $"FAIL (got {result}, expected {expected})";
                    }
                }
                catch (AggregateException ae)
                {
                    status = $"FAIL (exception: {ae.InnerException?.GetType().Name}: {ae.InnerException?.Message})";
                }
                catch (Exception ex)
                {
                    status = $"FAIL (exception: {ex.GetType().Name}: {ex.Message})";
                }

                string resultStr = result >= 0 ? result.ToString() : "N/A";
                Console.WriteLine($"   {solName,-38} => {resultStr,8}  ({elapsedMs,5} ms)  [{status}]");

                if (status == "PASS")
                    totalPass[s]++;
                else
                    totalFail[s]++;
            }

            Console.WriteLine();
        }

        // -----------------------------------------------------------------------
        // Summary
        // -----------------------------------------------------------------------
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("  SUMMARY");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        for (int s = 1; s < solutions.Length; s++)
        {
            Console.WriteLine($"   {solutions[s].Name,-38}  PASS: {totalPass[s],3}  FAIL: {totalFail[s],3}");
        }
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
    }
}
