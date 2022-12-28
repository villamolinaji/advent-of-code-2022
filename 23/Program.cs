string[] lines = File.ReadAllLines("input.txt");

int row = 0;
int col = 0;
//var map = new Dictionary<(int x, int y), char>();
var elfs = new List<Elf>();
var mapElfs = new HashSet<(int x, int y)>();
foreach (var line in lines)
{
    foreach (var c in line)
    {
        //map.Add((col, row), c);
        if (c == '#')
        {
            elfs.Add(new Elf() { CurrentPosition = (col, row), NewPosition = (col, row) });
            mapElfs.Add((col, row));
        }
        col++;
    }
    col = 0;
    row++;
}

//DoPart1();
DoPart2();


void DoPart2()
{    
    int iterations = 1;
    var currentDirection = Direction.North;
    bool anyMove = true;
    while (anyMove)
    {
        anyMove = false;
        List<(int x, int y)> newPositions = new List<(int x, int y)>();
        foreach (var elf in elfs)
        {
            elf.NewPosition = MoveElf(currentDirection, elf, mapElfs, currentDirection);
        }

        var newElfs = new List<Elf>();
        foreach (var elf in elfs)
        {
            var newElf = new Elf() { CurrentPosition = elf.CurrentPosition, NewPosition = elf.NewPosition };
            if ((elf.CurrentPosition.x != elf.NewPosition.x || elf.CurrentPosition.y != elf.NewPosition.y) &&
                elfs.Where(e => e.NewPosition.x == elf.NewPosition.x && e.NewPosition.y == elf.NewPosition.y).Count() == 1)
            {
                mapElfs.Remove((elf.CurrentPosition.x, elf.CurrentPosition.y));
                mapElfs.Add((elf.NewPosition.x, elf.NewPosition.y));
                newElf.CurrentPosition = elf.NewPosition;
                anyMove = true;
            }
            newElfs.Add(newElf);
        }

        elfs.Clear();
        foreach (var elf in newElfs)
        {
            elfs.Add(elf);
        }

        currentDirection = NewDirection(currentDirection);

        if (!anyMove)
        {
            break;
        }

        iterations++;
    }
    

    Console.WriteLine(iterations);
    //984
}


void DoPart1()
{
    //DrawMap(map, 0);
    var currentDirection = Direction.North;
    for (int i = 0; i < 10; i++)
    {
        List<(int x, int y)> newPositions = new List<(int x, int y)>();
        foreach (var elf in elfs)
        {
            elf.NewPosition = MoveElf(currentDirection, elf, mapElfs, currentDirection);
        }

        var newElfs = new List<Elf>();
        foreach (var elf in elfs)
        {
            var newElf = new Elf() { CurrentPosition = elf.CurrentPosition, NewPosition = elf.NewPosition };
            if (elfs.Where(e => e.NewPosition.x == elf.NewPosition.x && e.NewPosition.y == elf.NewPosition.y).Count() == 1)
            {        
                mapElfs.Remove((elf.CurrentPosition.x, elf.CurrentPosition.y));
                mapElfs.Add((elf.NewPosition.x, elf.NewPosition.y));
                newElf.CurrentPosition = elf.NewPosition;
            }
            newElfs.Add(newElf);
        }

        elfs.Clear();
        foreach (var elf in newElfs)
        {
            elfs.Add(elf);
        }

        currentDirection = NewDirection(currentDirection);
        //DrawMap(map, i + 1);
    }

    int rectangleMinX = elfs.Select(e => e.CurrentPosition.x).Min();
    int rectangleMaxX = elfs.Select(e => e.CurrentPosition.x).Max();
    int rectangleMinY = elfs.Select(e => e.CurrentPosition.y).Min();
    int rectangleMaxY = elfs.Select(e => e.CurrentPosition.y).Max();    

    var countEmpty = (rectangleMaxX - rectangleMinX + 1) * (rectangleMaxY - rectangleMinY + 1);
    countEmpty = countEmpty - elfs.Count();

    Console.WriteLine(countEmpty);
    //4116
}


void DrawMap(Dictionary<(int x, int y), char> map, int round)
{
    Console.WriteLine();
    Console.WriteLine($"Map round {round}");
    for (int y = 0; y <= map.Select(m => m.Key.y).Max(); y++)
    {        
        for (int x = 0; x <= map.Select(m => m.Key.x).Max(); x++)
        {
            Console.Write(map[(x, y)]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

(int x, int y) MoveElf(Direction direction, Elf elf, HashSet<(int x, int y)> mapElfs, Direction currentDirection)
{
    bool canMove = false;
    if (mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) ||
        mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) ||
        mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) ||
        mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) ||
        mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) ||
        mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) ||
        mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) ||
        mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))                
    {
        canMove = true;
    }

    if (canMove)
    {
        switch (currentDirection)
        {
            case Direction.North:
                if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.South:                
                if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.West:                
                if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.East:                
                if (!mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (!mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) &&
                    !mapElfs.Contains((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)))
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            default:
                return (elf.CurrentPosition.x, elf.CurrentPosition.y);
        }
    }
    else
    {
        return (elf.CurrentPosition.x, elf.CurrentPosition.y);
    }
}

(int x, int y) MoveElfOld(Direction direction, Elf elf, Dictionary<(int x, int y), char> map, Direction currentDirection)
{
    bool canMove = false;
    if ((map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y - 1)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y + 1)] == '#') ||
        (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '#'))
    {
        canMove = true;
    }

    if (canMove)
    {
        switch (currentDirection)
        {
            case Direction.North:
                if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.South:
                if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.West:
                if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            case Direction.East:
                if (map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x + 1, elf.CurrentPosition.y);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y - 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y - 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x, elf.CurrentPosition.y + 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x + 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y + 1);
                else if (map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y - 1)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y)] == '.' &&
                    map.ContainsKey((elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)) && map[(elf.CurrentPosition.x - 1, elf.CurrentPosition.y + 1)] == '.')
                    return (elf.CurrentPosition.x - 1, elf.CurrentPosition.y);
                else
                    return (elf.CurrentPosition.x, elf.CurrentPosition.y);
            default:
                return (elf.CurrentPosition.x, elf.CurrentPosition.y);
        }
    }
    else
    { 
        return (elf.CurrentPosition.x, elf.CurrentPosition.y); 
    }
}

Direction NewDirection(Direction currentDirection)
{
    switch (currentDirection)
    {
        case Direction.North:
            return Direction.South;
        case Direction.South:
            return Direction.West;
        case Direction.West:
            return Direction.East;
        case Direction.East:
            return Direction.North;
        default: 
            return currentDirection;
    }
}


enum Direction
{
    North,
    South,
    West,
    East
}

class Elf
{
    public (int x, int y) CurrentPosition { get; set; }
    public (int x, int y) NewPosition { get; set; }
}