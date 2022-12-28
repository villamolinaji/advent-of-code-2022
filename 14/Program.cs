using _14;
using System.Collections.Generic;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;

var positions = new List<Pos>();
positions.Add(new Pos() { X = 500, Y = 0, Item = '+' });

int minX;
int minY;
int maxX;
int maxY;

foreach (var line in lines)
{
    var coordenates = line.Split("->");
    int? prevX = null;
    int? prevY = null;
    int? currentX = null;
    int? currentY = null;
    foreach (var c in coordenates)
    {
        var xy = c.Split(',');

        if (prevX == null && prevY == null)
        {
            prevX = int.Parse(xy[0]);
            prevY = int.Parse(xy[1]);
        }
        else
        {
            currentX = int.Parse(xy[0]);
            currentY = int.Parse(xy[1]);

            minX = Math.Min((int)currentX, (int)prevX);
            maxX = Math.Max((int)currentX, (int)prevX);
            minY = Math.Min((int)currentY, (int)prevY);
            maxY = Math.Max((int)currentY, (int)prevY);

            if (minX < maxX)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    if (!positions.Any(p => p.X == i && p.Y == minY))
                        positions.Add(new Pos() { X = i, Y = minY, Item = '#' });
                }
            }
            if (minY < maxY)
            {
                for (int i = minY; i <= maxY; i++)
                {
                    if (!positions.Any(p => p.X == minX && p.Y == i))
                        positions.Add(new Pos() { X = minX, Y = i, Item = '#' });
                }
            }

            prevX = currentX;
            prevY = currentY;
        }
    }
}

//fill .
minX = positions.Select(p => p.X).Min();
minY = positions.Select(p => p.Y).Min();
maxX = positions.Select(p => p.X).Max();
maxY = positions.Select(p => p.Y).Max();
for (int i = minX; i <= maxX; i++)
{
    for (int j = minY; j <= maxY; j++)
    {
        if (!positions.Any(p => p.X == i && p.Y == j))
            positions.Add(new Pos() { X = i, Y = j, Item = '.' });
    }
}

//Utils.DrawPositions(positions, minX, maxX, minY, maxY);

bool isFilled = false;
/*while (!isFilled)
{
    result++;

    isFilled = Utils.FallSand(positions, 500, 0, minX, maxY);
    //Console.WriteLine(result);
    //Utils.DrawPositions(positions, minX, maxX, minY, maxY);
}


Console.WriteLine(result);*/


//Part 2
positions = new List<Pos>();
positions.Add(new Pos() { X = 500, Y = 0, Item = '+' });


foreach (var line in lines)
{
    var coordenates = line.Split("->");
    int? prevX = null;
    int? prevY = null;
    int? currentX = null;
    int? currentY = null;
    foreach (var c in coordenates)
    {
        var xy = c.Split(',');

        if (prevX == null)
        {
            prevX = int.Parse(xy[0]);
            prevY = int.Parse(xy[1]);
        }
        else
        {
            currentX = int.Parse(xy[0]);
            currentY = int.Parse(xy[1]);

            minX = Math.Min((int)currentX, (int)prevX);
            maxX = Math.Max((int)currentX, (int)prevX);
            minY = Math.Min((int)currentY, (int)prevY);
            maxY = Math.Max((int)currentY, (int)prevY);

            if (minX < maxX)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    if (!positions.Any(p => p.X == i && p.Y == minY))
                        positions.Add(new Pos() { X = i, Y = minY, Item = '#' });
                }
            }
            if (minY < maxY)
            {
                for (int i = minY; i <= maxY; i++)
                {
                    if (!positions.Any(p => p.X == minX && p.Y == i))
                        positions.Add(new Pos() { X = minX, Y = i, Item = '#' });
                }
            }

            prevX = currentX;
            prevY = currentY;
        }
    }
}


minY = positions.Select(p => p.Y).Min();
maxY = positions.Select(p => p.Y).Max() + 2;
minX = positions.Select(p => p.X).Min() - maxY;
maxX = positions.Select(p => p.X).Max() + maxY;
for (int i = minX; i <= maxX; i++)
{
    for (int j = minY; j <= maxY; j++)
    {
        if (!positions.Any(p => p.X == i && p.Y == j))
        {
            if (j == maxY)
                positions.Add(new Pos() { X = i, Y = j, Item = '#' });
            else
                positions.Add(new Pos() { X = i, Y = j, Item = '.' });
        }
    }
}
//Utils.DrawPositions(positions, minX, maxX, minY, maxY);

isFilled = false;
result = 0;
while (!isFilled)
{
    result++;    

    isFilled = Utils.FallSand2(positions, 500, 0, minX, maxY);
    //Console.WriteLine(result);
    //Utils.DrawPositions(positions, minX, maxX, minY, maxY);
}


Console.WriteLine(result);


class Utils
{
    public static void DrawPositions(List<Pos> positions, int minX, int maxX, int minY, int maxY)
    {
        var orderPositions = positions.OrderBy(p => p.X).OrderBy(p => p.Y).ToList();
        Console.WriteLine("================");
        for (int y = minY; y <= maxY; y++)
        {            
            for (int x = minX; x <= maxX; x++)
            {                
                Console.Write(positions.First(p => p.X == x && p.Y == y).Item);
            }
            Console.WriteLine();
        }
    }

    public static bool FallSand(List<Pos> positions, int sandX, int sandY, int minX, int maxY)
    {
        bool isFilled = false;
        if (positions.Any(p => p.X == sandX && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX, sandY + 1, minX, maxY);
        }
        else if (positions.Any(p => p.X == sandX - 1 && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX - 1 && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX - 1, sandY + 1, minX, maxY);
        }
        else if (!positions.Any(p => p.X == sandX - 1 && p.Y == sandY + 1))
        {
            return true;
        }
        else if (positions.Any(p => p.X == sandX + 1 && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX + 1 && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX + 1, sandY + 1, minX, maxY);
        }
        else if (!positions.Any(p => p.X == sandX + 1 && p.Y == sandY + 1))
        {
            return true;
        }
        else
        {
            positions.First(p => p.X == sandX && p.Y == sandY).Item = 'o';
            if (sandX == minX && sandY == maxY)
            {
                isFilled = true;
            }
            return isFilled;
        }
    }

    public static bool FallSand2(List<Pos> positions, int sandX, int sandY, int minX, int maxY)
    {
        bool isFilled = false;
        if (positions.Any(p => p.X == sandX && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX, sandY + 1, minX, maxY);
        }
        else if (positions.Any(p => p.X == sandX - 1 && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX - 1 && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX - 1, sandY + 1, minX, maxY);
        }
        else if (!positions.Any(p => p.X == sandX - 1 && p.Y == sandY + 1))
        {
            return true;
        }
        else if (positions.Any(p => p.X == sandX + 1 && p.Y == sandY + 1) &&
            positions.First(p => p.X == sandX + 1 && p.Y == sandY + 1).Item == '.')
        {
            return FallSand(positions, sandX + 1, sandY + 1, minX, maxY);
        }
        else if (!positions.Any(p => p.X == sandX + 1 && p.Y == sandY + 1))
        {
            return true;
        }
        else
        {
            
            positions.First(p => p.X == sandX && p.Y == sandY).Item = 'o';
            if (sandX == 500 && sandY == 0)
            {
                isFilled = true;
            }
            return isFilled;
        }
    }
}