using _10;
using System.Runtime.CompilerServices;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;
int signalStrength = 1;
int addValue = 0;
int cycle = 0;
List<Par> cycles= new List<Par>();
cycles.Add(new Par() { Cycle = 20, Value = 0 });
cycles.Add(new Par() { Cycle = 60, Value = 0 });
cycles.Add(new Par() { Cycle = 100, Value = 0 });
cycles.Add(new Par() { Cycle = 140, Value = 0 });
cycles.Add(new Par() { Cycle = 180, Value = 0 });
cycles.Add(new Par() { Cycle = 220, Value = 0 });


foreach (var line in lines)
{    
    if (line.StartsWith("noop"))
    {
        cycle++;
        Methods.CheckStrength(cycles, cycle, signalStrength);
    }
    else if (line.StartsWith("addx"))
    {
        addValue = int.Parse(line.Substring(5));
        cycle++;
        Methods.CheckStrength(cycles, cycle, signalStrength);
        cycle++;        
        Methods.CheckStrength(cycles, cycle, signalStrength);

        signalStrength = signalStrength + addValue;
    }
}

foreach (var par in cycles)
{
    result = result + (par.Cycle * par.Value);
}

Console.WriteLine(result.ToString());


// PART 2
string[,] crt = new string[6, 40];
int row = 0;
int col = 0;
cycle= 0;
cycles = new List<Par>();
cycles.Add(new Par() { Cycle = 40, Value = 0 });
cycles.Add(new Par() { Cycle = 80, Value = 0 });
cycles.Add(new Par() { Cycle = 120, Value = 0 });
cycles.Add(new Par() { Cycle = 160, Value = 0 });
cycles.Add(new Par() { Cycle = 200, Value = 0 });
cycles.Add(new Par() { Cycle = 240, Value = 0 });
signalStrength = 1;

foreach (var line in lines)
{
    if (line.StartsWith("noop"))
    {
        cycle++;        
        Methods.DrawPixel(cycles, crt, cycle, signalStrength, row, col, out row, out col);
    }
    else if (line.StartsWith("addx"))
    {
        addValue = int.Parse(line.Substring(5));
        cycle++;        
        Methods.DrawPixel(cycles, crt, cycle, signalStrength, row, col, out row, out col);
        cycle++;        
        Methods.DrawPixel(cycles, crt, cycle, signalStrength, row, col, out row, out col);

        signalStrength = signalStrength + addValue;
    }
}

for (int i = 0; i < 6; i++)
{
    for (int j = 0; j < 40; j++)
    {
        Console.Write(crt[i, j]);
    }
    Console.WriteLine();
}

Console.WriteLine(result.ToString());


class Methods
{
    public static void CheckStrength(List<Par> cycles, int cycle, int signalStrength)
    {
        if (cycles.Any(p => p.Cycle == cycle))
        {
            cycles.First(p => p.Cycle == cycle).Value = signalStrength;            
        }
    }

    public static void DrawPixel(List<Par> cycles, string[,] crt, int cycle, int signalStrength, int row1, int col1, out int row, out int col)
    {
        row = row1;
        col = col1;

        
        string pixel = "#";
        if (Math.Abs(signalStrength - col) > 1)
        {
            pixel = ".";
        }
        crt[row, col] = pixel;

        col++;

        if (cycles.Any(p => p.Cycle == cycle))
        {
            row++;
            col = 0;
        }
    }
    
}