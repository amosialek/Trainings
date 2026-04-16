namespace SolidTraining.Common;

/// <summary>
/// Abstraction over DateTime.Now to enable deterministic testing.
/// Used in OCP module to make seasonal discounts testable.
/// </summary>
public interface IClock
{
    DateTime UtcNow { get; }
}

/// <summary>
/// Production implementation that returns the real current time.
/// </summary>
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
