
using _15;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;

var sensors = new List<Sensor>();

foreach (var line in lines)
{
    var auxLines = line.Split(":");

    var aux1 = auxLines[0].Replace("Sensor at ", "");
    var aux1List = aux1.Split(",");
    int sX = int.Parse(aux1List[0].Substring(2));
    int sY = int.Parse(aux1List[1].Substring(3));

    var aux2 = auxLines[1].Replace(" closest beacon is at ", "");
    var aux2List = aux2.Split(",");
    int bX = int.Parse(aux2List[0].Substring(2));
    int bY = int.Parse(aux2List[1].Substring(3));

    sensors.Add(new Sensor() { SX = sX, SY = sY, BX = bX, BY = bY });

}

var map = new HashSet<Pos>();

//Utils.DrawPositions(map);

//int row = 10;
int row = 2000000;

List<RowFilled> rowFilleds = new List<RowFilled>();
foreach (var sensor in sensors)
{
    //var sensor = new Sensor() { BX = 2, BY = 10, SX = 8, SY = 7 };
    map.Add(new Pos() { X = sensor.SX, Y = sensor.SY, Item = 'S' });
    map.Add(new Pos() { X = sensor.BX, Y = sensor.BY, Item = 'B' });
    int distance = Utils.CalculateDistance(sensor);
    int distanceY = Math.Abs(sensor.SY - row);
    int distanceX = distance - distanceY;

    rowFilleds.Add(new RowFilled() { StartX = sensor.SX - distanceX, EndX = sensor.SX + distanceX });
}

//Utils.DrawPositions(map);

//result = Utils.CalculatePositions(map, row);
result = Utils.CalculatePositions(map, rowFilleds, row);

Console.WriteLine(result);
//5511201


//part2

//Drawing
/*
 map = new HashSet<Pos>();
int minX = Math.Min(sensors.Select(p => p.SX).Min(), sensors.Select(p => p.BX).Min());
int minY = Math.Min(sensors.Select(p => p.SY).Min(), sensors.Select(p => p.BY).Min());
int maxX = Math.Max(sensors.Select(p => p.SX).Max(), sensors.Select(p => p.BX).Max());
int maxY = Math.Max(sensors.Select(p => p.SY).Max(), sensors.Select(p => p.BY).Max());
for (int x = minX; x <= maxX; x++)
{
    for (int y = minY; y <= maxY; y++)
    {
        if (sensors.Any(p => p.SX == x && p.SY == y))
            map.Add(new Pos() { X = x, Y = y, Item = 'S' });
        else if (sensors.Any(p => p.BX == x && p.BY == y))
            map.Add(new Pos() { X = x, Y = y, Item = 'B' });
        else               
            map.Add(new Pos() { X = x, Y = y, Item = '.' });       
    }
}
Utils.DrawPositions(map);

foreach (var sensor in sensors)
{    
    int distance = Utils.CalculateDistance(sensor);
    int distanceY = 0;    
    bool increaseY = true;
    
    for (int x = sensor.SX - distance; x <= sensor.SX + distance; x++)
    {
        for (int y = sensor.SY - distanceY; y <= sensor.SY + distanceY; y++)
        {
            var itemAux = map.FirstOrDefault(m => m.X == x && m.Y == y);
            if (itemAux != null)
            {
                if (itemAux.Item == '.')
                {
                    itemAux.Item = '#';
                }
            }
            else
            {                
                map.Add(new Pos() { X = x, Y = y, Item = '#' });
            }
        }
        if (distanceY == distance)
            increaseY = false;

        if (increaseY)
            distanceY++;
        else
            distanceY--;
    }
}    
Utils.DrawPositions(map);
*/


long result2 = 0;
int maxSX = sensors.Select(s => s.SX).Max();
int maxSY = sensors.Select(s => s.SY).Max();

var posResult = Utils.FindResultPosition(sensors);

result2 = Utils.CalculateResult2(posResult.X, posResult.Y);
Console.WriteLine(result2);
//11318723411840

class Utils
{
    public static Pos FindResultPosition(List<Sensor> sensors)
    {
        Pos result = new Pos();
        //int top = 20;
        int top = 4000000;

        // find y
        for (int y = 0; y < top; y++)
        {
            var xRanges = new List<Pos>();
            foreach (var sensor in sensors)
            {
                int distance = Utils.CalculateDistance(sensor);
                int sYMin = sensor.SY - distance;
                int sYMax = sensor.SY + distance;

                if (sYMin <= y && y <= sYMax)
                {
                    int width = distance - Math.Abs(sensor.SY - y);
                    xRanges.Add(new Pos() { X = (sensor.SX - width), Y = (sensor.SX + width) });
                }
            }

            xRanges = xRanges.OrderBy(p => p.X).ToList();
            int firstX = xRanges.First().X;
            foreach(var pos in xRanges)
            {
                var nextX = firstX + 1;
                if (nextX < pos.X)
                {
                    result.X = nextX;
                    result.Y = y;
                    return result;
                }
                else
                {
                    if (firstX < pos.Y)
                        firstX = pos.Y;
                }
            }
        }        

        return result;
    }

    
    public static long CalculateResult2(int x, int y)
    {
        return ((long)x * 4000000) + (long)y;
    }

    public static int CalculateDistance(Sensor sensor)
    {
        return Math.Abs(sensor.SX - sensor.BX) + Math.Abs(sensor.SY - sensor.BY);
    }

    public static int CalculatePositions(HashSet<Pos> map, List<RowFilled> rowFilleds, int y)
    {
        int result = 0;
        var aux = Utils.CleanRowFilled(rowFilleds);

        foreach (var rowFilled in aux)
        {
            int diff = Math.Abs((rowFilled.StartX - rowFilled.EndX)) + 1;
            int occupied = map.Where(m => m.X >= rowFilled.StartX && m.X <= rowFilled.EndX && m.Y == y).Select(m => new { m.X, m.Y }).Distinct().Count();
            result = result + (diff - occupied);

        }

        return result;
    }

    public static List<RowFilled> CleanRowFilled(List<RowFilled> rowFilleds)
    {
        var result = new List<RowFilled>();
        foreach (var rowFilled in rowFilleds)
        {
            if (result.Count == 0)
            {
                result.Add(new RowFilled() { StartX = rowFilled.StartX, EndX = rowFilled.EndX });
            }
            else
            {
                if (result.Any(r => r.StartX <= rowFilled.StartX && r.EndX >= rowFilled.EndX))
                {
                    continue;
                }
                else if (result.Any(r => rowFilled.StartX < r.StartX && rowFilled.EndX <= r.EndX))
                {
                    result.First(r => rowFilled.StartX < r.StartX && rowFilled.EndX <= r.EndX).StartX = rowFilled.StartX;
                    continue;
                }
                else if (result.Any(r => r.StartX <= rowFilled.StartX && r.EndX < rowFilled.EndX))
                {
                    result.First(r => r.StartX <= rowFilled.StartX && r.EndX < rowFilled.EndX).EndX = rowFilled.EndX;
                    continue;
                }
                else if (result.Any(r => rowFilled.StartX < r.StartX && rowFilled.EndX > r.EndX))
                {
                    var rowAux = result.First(r => rowFilled.StartX < r.StartX && rowFilled.EndX > r.EndX);
                    rowAux.StartX = rowFilled.StartX;
                    rowAux.EndX = rowFilled.EndX;
                    continue;
                }
                else
                {
                    result.Add(new RowFilled() { StartX = rowFilled.StartX, EndX = rowFilled.EndX });
                }
            }
        }

        return result;
    }

    public static int CalculatePositions(HashSet<Pos> map, int y)
    {
        int count = 0;
        int minX = map.Select(p => p.X).Min();
        int maxX = map.Select(p => p.X).Max();
        for (int x = minX; x <= maxX; x++)
        {
            var item = map.FirstOrDefault(m => m.X == x && m.Y == y);
            if (item != null)
            {
                if (item.Item == '#')
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static void DrawPositions(HashSet<Pos> map)
    {
        int minX = map.Select(p => p.X).Min();
        int minY = map.Select(p => p.Y).Min();
        int maxX = map.Select(p => p.X).Max();
        int maxY = map.Select(p => p.Y).Max();
        //var orderMap = map.OrderBy(p => p.X).OrderBy(p => p.Y).ToList();
        Console.WriteLine("================");
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                var item = map.FirstOrDefault(p => p.X == x && p.Y == y);
                if (item != null)
                    Console.Write(item.Item);
            }
            Console.WriteLine();
        }
    }

    public static void AddIntoMap(HashSet<Pos> map, int x, int y)
    {
        int minX = map.Select(p => p.X).Min();
        int minY = map.Select(p => p.Y).Min();
        int maxX = map.Select(p => p.X).Max();
        int maxY = map.Select(p => p.Y).Max();

        if (x < minX || x > maxX)
        {
            for (int y2 = minY; y2 <= maxY; y2++)
            {
                map.Add(new Pos() { X = x, Y = y2, Item = '.' });
            }
        }
        else if (y < minY || y > maxY)
        {
            for (int x2 = minX; x2 <= maxX; x2++)
            {
                map.Add(new Pos() { X = x2, Y = y, Item = '.' });
            }
        }

        var item = map.FirstOrDefault(m => m.X == x && m.Y == y);
        if (item != null)
        {
            if (item.Item == '.')
            {
                item.Item = '#';
            }
        }
    }
}
