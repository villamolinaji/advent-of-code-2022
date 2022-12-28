string[] lines = File.ReadAllLines("input.txt");


var map = new Dictionary<(int x, int y), char>();
var mapList = new List<Map>();

int row = 1;
int col = 1;
int? startX = null;
int? startY = null;
var instructions = new List<Instruction>();
foreach (var line in lines)
{
    
    if (line.Trim().Contains('#') || line.Trim().Contains('.'))
    {
        foreach (var c in line)
        {
            if (c == '.' || c == '#')
            {
                map.Add((col, row), c);
                mapList.Add(new Map() { X = col, Y = row, Item = c });
                if (startX == null && startY == null)
                {
                    startX = col;
                    startY = row;
                }
            }            
            col++;
        }
        col = 1;
        row++;
    }
    else if (!string.IsNullOrEmpty(line.Trim()))
    {
        var auxLine = line;
        char turn = ' ';
        while (auxLine.Length > 0)
        {
            if (auxLine.Contains('R') || auxLine.Contains('L'))
            {
                if (auxLine.IndexOf('R') < auxLine.IndexOf('L') && auxLine.IndexOf('R') > 0)
                {
                    turn = 'R';
                }
                else
                {
                    turn = 'L';
                }
                var instruction = new Instruction() { Tiles = int.Parse(auxLine.Substring(0, auxLine.IndexOf(turn))), Turn = turn };
                instructions.Add(instruction);
                auxLine = auxLine.Remove(0, auxLine.IndexOf(turn) + 1);                
            }
            else
            {
                var instruction = new Instruction() { Tiles = int.Parse(auxLine) };
                instructions.Add(instruction);
                auxLine = "";
            }            
        }
    }        
}


//DoPart1();
// 31568
DoPart2();

void DoPart1()
{
    var direction = Direction.Right;
    int currentX = (int)startX;
    int currentY = (int)startY;
    int nextX = currentX + 1;
    int nextY = currentY;

    foreach (var instruction in instructions)
    {
        bool isMoving = true;
        int steps = instruction.Tiles;
        while (isMoving)
        {
            if (!map.ContainsKey((nextX, nextY)))
            {
                switch (direction)
                {
                    case Direction.Right:
                        nextX = mapList.Where(m => m.Y == nextY).Select(m => m.X).Min();
                        break;
                    case Direction.Down:
                        nextY = mapList.Where(m => m.X == nextX).Select(m => m.Y).Min();
                        break;
                    case Direction.Left:
                        nextX = mapList.Where(m => m.Y == nextY).Select(m => m.X).Max();
                        break;
                    case Direction.Up:
                        nextY = mapList.Where(m => m.X == nextX).Select(m => m.Y).Max();
                        break;
                }

            }
            else if (map[(nextX, nextY)] == '.')
            {
                currentX = nextX;
                currentY = nextY;
                steps--;
                var nextPos = NextStep(direction, currentX, currentY);
                nextX = nextPos.x;
                nextY = nextPos.y;
            }
            else if (map[(nextX, nextY)] == '#')
            {
                isMoving = false;
                if (instruction.Turn == 'R' || instruction.Turn == 'L')
                {
                    direction = NextDirection(direction, instruction.Turn);
                    var nextPos = NextStep(direction, currentX, currentY);
                    nextX = nextPos.x;
                    nextY = nextPos.y;
                }
                break;
            }


            if (steps == 0)
            {
                isMoving = false;
                if (instruction.Turn == 'R' || instruction.Turn == 'L')
                {
                    direction = NextDirection(direction, instruction.Turn);
                    var nextPos = NextStep(direction, currentX, currentY);
                    nextX = nextPos.x;
                    nextY = nextPos.y;
                }
                break;
            }
        }
    }

    int facing = 0;
    switch (direction)
    {
        case Direction.Right:
            facing = 0;
            break;
        case Direction.Down:
            facing = 1;
            break;
        case Direction.Left:
            facing = 2;
            break;
        case Direction.Up:
            facing = 3;
            break;
    }
    int result = (1000 * currentY) + (4 * currentX) + facing;
    Console.WriteLine(result);
}


void DoPart2()
{
    var direction = Direction.Right;
    int currentX = (int)startX;
    int currentY = (int)startY;
    int nextX = currentX + 1;
    int nextY = currentY;

    int cubeSizeX = 0;
    int cubeSizeY = 0;

    int maxX = mapList.Select(m => m.X).Max();
    int maxY = mapList.Select(m => m.Y).Max();

    if (maxX < 50)
    {
        cubeSizeX = 4;
        cubeSizeY = 4;
    }
    else
    {
        cubeSizeX = 50;
        cubeSizeY = 50;
    }

    var emptyCubeMap = new Dictionary<(int x, int y), (int x, int y, Direction direction)>();

    if (cubeSizeX == 4)
    {
        for (int i = 1; i <= 4; i++)
        {
            // 1 left - 3 up
            emptyCubeMap[(8, i)] = (4 + i, 5, Direction.Down);
            emptyCubeMap[(4 + i, 4)] = (9, i, Direction.Right);

            // 1 up - 2 up
            emptyCubeMap[(8 + i, 0)] = (5 - i, 5, Direction.Down);
            emptyCubeMap[(i, 4)] = (13 - i, 1, Direction.Down);

            // 1 right - 6 left
            emptyCubeMap[(13, i)] = (16, 13 - i, Direction.Left);
            emptyCubeMap[(17, 8 + i)] = (12, 5 - i, Direction.Right);

            // 2 left - 6 down
            emptyCubeMap[(0, 4 + i)] = (17 - i, 12, Direction.Up);
            emptyCubeMap[(12 + i, 13)] = (1, 9 - i, Direction.Right);

            // 2 down - 5 down
            emptyCubeMap[(i, 9)] = (13 - i, 12, Direction.Up);
            emptyCubeMap[(8 + i, 13)] = (5 - i, 8, Direction.Up);

            // 3 down - 5 left
            emptyCubeMap[(4 + i, 9)] = (9, 13 - i, Direction.Right);
            emptyCubeMap[(8, 8 + i)] = (9 - i, 8, Direction.Up);

            // 4 right - 6 up
            emptyCubeMap[(13, 4 + i)] = (17 - i, 9, Direction.Down);
            emptyCubeMap[(12 + i, 8)] = (12, 9 - i, Direction.Left);
        }
    }
    else
    {
        for (int i = 1; i <= 50; i++)
        {
            // 1 left - 4 left **
            emptyCubeMap[(50, i)] = (1, 151 - i, Direction.Right);
            emptyCubeMap[(0, 100 + i)] = (51, 51 - i, Direction.Right);

            // 1 up - 6 left **
            emptyCubeMap[(50 + i, 0)] = (1, 150 + i, Direction.Right);
            emptyCubeMap[(0, 150 + i)] = (50 + i, 1, Direction.Down);

            // 2 up - 6 down **
            emptyCubeMap[(100 + i, 0)] = (i, 200, Direction.Up);
            emptyCubeMap[(i, 201)] = (100 + i, 1, Direction.Down);

            // 2 right - 5 right **
            emptyCubeMap[(151, i)] = (100, 151 - i, Direction.Left);
            emptyCubeMap[(101, 100 + i)] = (150, 51 - i, Direction.Left);

            // 3 left - 4 up **
            emptyCubeMap[(50, 50 + i)] = (i, 101, Direction.Down);
            emptyCubeMap[(i, 100)] = (51, 50 + i, Direction.Right);

            // 3 right - 2 down **
            emptyCubeMap[(101, 50 + i)] = (100 + i, 50, Direction.Up);
            emptyCubeMap[(100 + i, 51)] = (100, 50 + i, Direction.Left);

            // 5 down - 6 right **
            emptyCubeMap[(50 + i, 151)] = (50, 150 + i, Direction.Left);
            emptyCubeMap[(51, 150 + i)] = (50 + i, 150, Direction.Up);
        }
    }


    foreach (var instruction in instructions)
    {
        bool isMoving = true;
        int steps = instruction.Tiles;
        while (isMoving)
        {
            var newDirection = direction;
            if (emptyCubeMap.ContainsKey((nextX, nextY)))
            {
                var nextMove = emptyCubeMap[((nextX, nextY))];
                newDirection = nextMove.direction;
                nextX = nextMove.x;
                nextY = nextMove.y;
            }
            
            if (map[(nextX, nextY)] == '.')
            {
                currentX = nextX;
                currentY = nextY;
                steps--;
                direction = newDirection;
                var nextPos = NextStep(direction, currentX, currentY);
                nextX = nextPos.x;
                nextY = nextPos.y;
            }
            else if (map[(nextX, nextY)] == '#')
            {
                isMoving = false;
                if (instruction.Turn == 'R' || instruction.Turn == 'L')
                {
                    direction = NextDirection(direction, instruction.Turn);
                    var nextPos = NextStep(direction, currentX, currentY);
                    nextX = nextPos.x;
                    nextY = nextPos.y;
                }
                break;
            }          

            if (steps == 0)
            {
                isMoving = false;
                if (instruction.Turn == 'R' || instruction.Turn == 'L')
                {
                    direction = NextDirection(direction, instruction.Turn);
                    var nextPos = NextStep(direction, currentX, currentY);
                    nextX = nextPos.x;
                    nextY = nextPos.y;
                }
                break;
            }
        }
    }

    int facing = 0;
    switch (direction)
    {
        case Direction.Right:
            facing = 0;
            break;
        case Direction.Down:
            facing = 1;
            break;
        case Direction.Left:
            facing = 2;
            break;
        case Direction.Up:
            facing = 3;
            break;
    }
    int result = (1000 * currentY) + (4 * currentX) + facing;
    Console.WriteLine(result);    
    //36540
}


(int x, int y) NextStep(Direction currentDirection, int currentX, int currentY)
{
    int nextX = 0;
    int nextY = 0;

    switch (currentDirection)
    {
        case Direction.Right:
            nextX = currentX + 1;
            nextY = currentY;
            break;
        case Direction.Down:
            nextY = currentY + 1;
            nextX = currentX;
            break;
        case Direction.Left:
            nextX = currentX - 1;
            nextY = currentY;
            break;
        case Direction.Up:
            nextY = currentY - 1;
            nextX = currentX;
            break;
    }

    return (nextX, nextY);
}


Direction NextDirection(Direction currentDirection, char Turn)
{
    switch (currentDirection)
    {
        case Direction.Right:
            if (Turn == 'R')
                return Direction.Down;
            else
                return Direction.Up;            
        case Direction.Down:
            if (Turn == 'R')
                return Direction.Left;
            else
                return Direction.Right;            
        case Direction.Left:
            if (Turn == 'R')
                return Direction.Up;
            else
                return Direction.Down;            
        case Direction.Up:
            if (Turn == 'R')
                return Direction.Right;
            else
                return Direction.Left;            
        default:
            return Direction.Right;            
    }
}


enum Direction
{
    Right,
    Down,
    Left,
    Up
}


class Instruction
{
    public int Tiles { get; set; }
    public char Turn { get; set; }
}

class Map
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Item { get; set; }
}

