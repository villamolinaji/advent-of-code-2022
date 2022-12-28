using Nito.Collections;

string[] lines = File.ReadAllLines("input.txt");

var blizzards = new HashSet<(int x, int y, Direction direction)>();
var borders = new HashSet<(int x, int y)>();
int startX = 0;
int startY = 0;
int endX = 0;
int endY = 0;
int row = 0;
int col = 0;
var directions = new List<char>() { '<', '>', 'v', '^' };
foreach (var line in lines)
{
    foreach (var c in line)
    {        
        if (c == '#')
        {
            borders.Add((col, row));            
        }
        else if (c == '.')
        {
            if (row == 0)
            {
                startX = col;
                startY = row;
            }            
        }
        else if (directions.Contains(c))
        {
            var direction = Direction.North;
            switch(c)
            {
                case '^':
                    direction = Direction.North;
                    break;
                case '<':
                    direction = Direction.West;
                    break;
                case '>':
                    direction = Direction.East;
                    break;
                case 'v':
                    direction = Direction.South;
                    break;
            }
            blizzards.Add((col, row, direction));
        }
        col++;
    }
    col = 0;
    row++;
}

int borderMaxY = borders.Select(b => b.y).Max();
int borderMinX = borders.Select(b => b.x).Min();
int borderMaxX = borders.Select(b => b.x).Max();
for (int x = borderMinX + 1; x < borderMaxX; x++)
{
    if (!borders.Contains((x, borderMaxY)))
    {
        endX = x;
        endY = borderMaxY;
        break;
    }
}

int distanceFactor = (borderMaxY * borderMaxX) / GreatCommonDivisor(borderMaxY, borderMaxX);


//DoPart1();

DoPart2();


int GreatCommonDivisor(int num1, int num2)
{
    int remainder;

    while (num2 != 0)
    {
        remainder = num1 % num2;
        num1 = num2;
        num2 = remainder;
    }

    return num1;
}

void DoPart2()
{
    int minSteps = int.MaxValue;
    var queue = new Deque<QueueItem>();
    queue.AddToBack(new QueueItem() { CurrentPosition = (startX, startY), Steps = 0, Blizzards = blizzards, TripNumber = 0 });
    (int x, int y)[] destinations = new (int x, int y)[2];
    destinations[0] = (endX, endY);
    destinations[1] = (startX, startY);

    var stages = new HashSet<(int x, int y, int distanceFactorValue, int tripNumber)>();

    while (queue.Count > 0)
    {
        var queueItem = queue.RemoveFromFront();

        if (queueItem.CurrentPosition.x == destinations[queueItem.TripNumber % 2].x && queueItem.CurrentPosition.y == destinations[queueItem.TripNumber % 2].y)
        {
            if (queueItem.TripNumber == 2)
            {
                if (queueItem.Steps < minSteps)
                {
                    minSteps = queueItem.Steps;
                }
                continue;
            }
            queueItem.TripNumber++;
        }
        if (queueItem.Steps > minSteps)
        {
            continue;
        }
        var distanceFactorValue = queueItem.Steps % distanceFactor;
        if (stages.Contains((queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, distanceFactorValue, queueItem.TripNumber)))
        {
            continue;
        }
        stages.Add((queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, distanceFactorValue, queueItem.TripNumber));

        var newBlizzards = MoveBlizzards(queueItem.Blizzards);
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.AddToBack(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards, TripNumber = queueItem.TripNumber });
        }
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y - 1, newBlizzards))
        {
            queue.AddToBack(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y - 1), Steps = queueItem.Steps + 1, Blizzards = newBlizzards, TripNumber = queueItem.TripNumber });
        }
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y + 1, newBlizzards))
        {
            queue.AddToBack(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y + 1), Steps = queueItem.Steps + 1, Blizzards = newBlizzards, TripNumber = queueItem.TripNumber });
        }
        if (IsEmpty(queueItem.CurrentPosition.x - 1, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.AddToBack(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x - 1, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards, TripNumber = queueItem.TripNumber });
        }
        if (IsEmpty(queueItem.CurrentPosition.x + 1, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.AddToBack(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x + 1, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards, TripNumber = queueItem.TripNumber });
        }        
    }

    Console.WriteLine(minSteps);
    //816    
}

void DoPart1()
{
    int minSteps = int.MaxValue;        
    var queue = new Queue<QueueItem>();
    queue.Enqueue(new QueueItem() { CurrentPosition = (startX, startY), Steps = 0, Blizzards = blizzards });

    var stages = new HashSet<(int x, int y, int distanceFactorValue)>();

    while (queue.Count > 0)
    {
        var queueItem = queue.Dequeue();

        if (queueItem.CurrentPosition.x == endX && queueItem.CurrentPosition.y == endY)
        {
            if (queueItem.Steps < minSteps)
            {
                minSteps = queueItem.Steps;                
            }
            continue;
        }
        if (queueItem.Steps > minSteps)
        {
            continue;
        }
        var distanceFactorValue = queueItem.Steps % distanceFactor;
        if (stages.Contains((queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, distanceFactorValue)))
        {
            continue;
        }
        stages.Add((queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, distanceFactorValue));

        var newBlizzards = MoveBlizzards(queueItem.Blizzards);
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y - 1, newBlizzards))
        {
            queue.Enqueue(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y - 1), Steps = queueItem.Steps + 1, Blizzards = newBlizzards });
        }
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y + 1, newBlizzards))
        {
            queue.Enqueue(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y + 1), Steps = queueItem.Steps + 1, Blizzards = newBlizzards });
        }
        if (IsEmpty(queueItem.CurrentPosition.x - 1, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.Enqueue(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x - 1, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards });
        }
        if (IsEmpty(queueItem.CurrentPosition.x + 1, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.Enqueue(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x + 1, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards });
        }
        if (IsEmpty(queueItem.CurrentPosition.x, queueItem.CurrentPosition.y, newBlizzards))
        {
            queue.Enqueue(new QueueItem() { CurrentPosition = (queueItem.CurrentPosition.x, queueItem.CurrentPosition.y), Steps = queueItem.Steps + 1, Blizzards = newBlizzards });
        }
    }

    Console.WriteLine(minSteps);
    //292    
}

bool IsEmpty(int x, int y, HashSet<(int x, int y, Direction direction)> blizzards)
{
    bool isEmpty = false;

    if (x >= 0 && y >= 0 && x<= borderMaxX && y <= borderMaxY  && !borders.Contains((x, y)))
    { 
        if (!blizzards.Any(b => b.x == x && b.y == y))
        {
            isEmpty = true;
        }
    }

    return isEmpty;
}

HashSet <(int x, int y, Direction direction)> MoveBlizzards(HashSet<(int x, int y, Direction direction)> blizzards)
{
    var newBlizzards = new HashSet<(int x, int y, Direction direction)>();

    foreach(var blizzard in blizzards)
    {
        newBlizzards.Add(MoveBlizzard(blizzard));
    }

    return newBlizzards;
}

(int x, int y, Direction) MoveBlizzard((int x, int y, Direction direction) blizzard)
{
    int newX = blizzard.x;
    int newY = blizzard.y;

    switch (blizzard.direction)
    {
        case Direction.North:
            if (!borders.Contains((blizzard.x, blizzard.y - 1)))
            {
                newX = blizzard.x;
                newY = blizzard.y - 1;
            }
            else
            {
                newX = blizzard.x;
                newY = borderMaxY - 1;
            }
            break;
        case Direction.South:
            if (!borders.Contains((blizzard.x, blizzard.y + 1)))
            {
                newX = blizzard.x;
                newY = blizzard.y + 1;
            }
            else
            {
                newX = blizzard.x;
                newY = 1;
            }
            break;
        case Direction.West:
            if (!borders.Contains((blizzard.x - 1, blizzard.y)))
            {
                newX = blizzard.x - 1;
                newY = blizzard.y;
            }
            else
            {
                newX = borderMaxX - 1;
                newY = blizzard.y;
            }
            break;
        case Direction.East:
            if (!borders.Contains((blizzard.x + 1, blizzard.y)))
            {
                newX = blizzard.x + 1;
                newY = blizzard.y;
            }
            else
            {
                newX = 1;
                newY = blizzard.y;
            }
            break;
    }

    return (newX, newY, blizzard.direction);
}

enum Direction
{
    North,
    South,
    West,
    East
}

class QueueItem
{
    public (int x, int y) CurrentPosition { get; set; }
    //public Direction Direction { get; set; }
    public int Steps { get; set; }
    public HashSet<(int x, int y, Direction direction)> Blizzards { get; set; }
    public int TripNumber { get; set; }
}