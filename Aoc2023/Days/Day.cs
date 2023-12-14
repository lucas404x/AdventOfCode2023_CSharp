namespace Aoc2023.Days;

internal abstract class Day
{
    public virtual string[] Input { get; set; } = null!;

    public virtual string FirstHalf() => string.Empty;

    public virtual string SecondHalf() => string.Empty;
}
