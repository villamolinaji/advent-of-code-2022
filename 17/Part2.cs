/*
using System.Diagnostics;

var data =
    (
        from line in File.ReadAllLines("input.txt")
        where !string.IsNullOrWhiteSpace(line)
        select line
    ).ToArray();

var air = data[0].ToArray();


var part2Result = Runner.Run(air, 1_000_000_000_000L);
Console.WriteLine(part2Result);

class Pattern : IEquatable<Pattern>
{
    static IEqualityComparer<HashSet<(int, long)>> setComparer = HashSet<(int, long)>.CreateSetComparer();

        
    public int AirIndex { get; set; }

    public HashSet<(int, long)> Stones { get; set; }

    public Pattern(int airIndex, HashSet<(int, long)> stones)
    {
        AirIndex = airIndex;
        Stones = stones;
    }

    public bool Equals(Pattern? other) =>
        other != null && AirIndex == other.AirIndex && setComparer.Equals(Stones, other.Stones);

    public override bool Equals(object? obj) =>
        Equals(obj as Pattern);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 27;
            hash = (13 * hash) + AirIndex.GetHashCode();
            hash = (13 * hash) + setComparer.GetHashCode(Stones);
            return hash;
        }
    }
}

static class Runner
{
    static List<HashSet<(int x, long y)>> stones = new()
    {
        new() { (0, 0), (1, 0), (2, 0), (3, 0) },
        new() { (1, 2), (0, 1), (1, 1), (2, 1), (1, 0) },
        new() { (2, 2), (2, 1), (0, 0), (1, 0), (2, 0) },
        new() { (0, 3), (0, 2), (0, 1), (0, 0) },
        new() { (0, 1), (1, 1), (0, 0), (1, 0) }
    };

    static Pattern GetPattern(int airIndex, HashSet<(int x, long y)> stones, long maxHeight)
    {
        var maxY = stones.Select(c => c.y).Max();
        var patternStones = stones.Where(c => maxY - c.y < maxHeight).Select(c => (c.x, maxY - c.y)).ToHashSet();
        return new Pattern(airIndex, patternStones);
    }

    static HashSet<(int x, long y)> GetRock(long rockIndex, long bottomY) =>
        Runner.stones[(int)(rockIndex % 5)].Select(c => (c.x + 2, c.y + bottomY)).ToHashSet();

    public static long Run(char[] air, long rockCount)
    {
        var stones = new HashSet<(int x, long y)>() { (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0) };
        var patterns = new Dictionary<Pattern, (long rockIndex, long top)>();
        var rockIndex = 0L;
        var top = 0L;
        var airIndex = 0;
        var patternAdded = 0L;

        while (rockIndex < rockCount)
        {
            var rock = Runner.GetRock(rockIndex, top + 4);

            while (true)
            {
                if (air[airIndex] == '>')
                {
                    rock = Runner.ShiftRight(rock);
                    if (stones.Intersect(rock).Any())
                    {
                        rock = Runner.ShiftLeft(rock);
                    }
                }
                else
                {
                    rock = Runner.ShiftLeft(rock);
                    if (stones.Intersect(rock).Any())
                    {
                        rock = Runner.ShiftRight(rock);
                    }
                }

                airIndex = (airIndex + 1) % air.Length;

                rock = Runner.ShiftDown(rock);
                if (stones.Intersect(rock).Any())
                {
                    rock = Runner.ShiftUp(rock);
                    foreach (var coordinate in rock)
                    {
                        stones.Add(coordinate);
                    }

                    top = stones.Select(c => c.y).Max();

                    if (top >= 15)
                    {
                        var pattern = Runner.GetPattern(airIndex, stones, 15);

                        if (patterns.TryGetValue(pattern, out var result))
                        {
                            var distanceY = top - result.top;
                            var numRocks = rockIndex - result.rockIndex;
                            var multiple = (rockCount - rockIndex) / numRocks;
                            patternAdded += distanceY * multiple;
                            rockIndex += numRocks * multiple;
                        }

                        patterns[pattern] = (rockIndex, top);
                    }

                    ++rockIndex;
                    break;
                }
            }
        }

        return top + patternAdded;
    }

    static HashSet<(int x, long y)> ShiftDown(HashSet<(int x, long y)> rock) =>
        rock.Select(c => (c.x, c.y - 1)).ToHashSet();

    static HashSet<(int x, long y)> ShiftLeft(HashSet<(int x, long y)> rock)
    {
        if (rock.Any(c => c.x == 0))
        {
            return rock;
        }

        return rock.Select(c => (c.x - 1, c.y)).ToHashSet();
    }

    static HashSet<(int x, long y)> ShiftRight(HashSet<(int x, long y)> rock)
    {
        if (rock.Any(c => c.x == 6))
        {
            return rock;
        }

        return rock.Select(c => (c.x + 1, c.y)).ToHashSet();
    }

    static HashSet<(int x, long y)> ShiftUp(HashSet<(int x, long y)> rock) =>
        rock.Select(c => (c.x, c.y + 1)).ToHashSet();
}
*/