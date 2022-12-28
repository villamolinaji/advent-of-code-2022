string[] lines = File.ReadAllLines("input.txt");

int result = 0;
char[,] map = new char[lines.Length, lines[0].Length];

int row = 0;
int col = 0;
//int startX = 0;
//int startY = 0;
var options = new List<PosStep>();

foreach (var line in lines)
{
    foreach(var s in line)
    {
        map[row, col] = s;
        
        if (s == 'S' || s == 'a')
        {
            //startX = col;
            //startY = row;
            options.Add(new PosStep() { Pos = new Pos() { X = col, Y = row }, Steps = 0 });
        }

        col++;
    }
    col = 0;
    row++;    
}

/*var posUsed = new List<Pos>();
var stepsUsed = new List<int>();
var posStep = new List<PosStep>();*/
foreach (var option in options)
{
    var posUsed = new List<Pos>();
    var stepsUsed = new List<int>();
    var posStep = new List<PosStep>();
    var par = Utils.FindWay(map, option.Pos.X, option.Pos.Y, result, lines[0].Length, lines.Length, posUsed, stepsUsed, posStep);
    option.Steps = par.Steps;
}
//var par = Utils.FindWay(map, startX, startY, result, lines[0].Length, lines.Length, posUsed, stepsUsed, posStep);

//result = par.Steps;

result = options.Select(o => o.Steps).Min();

Console.WriteLine(result.ToString());

class Utils
{
    public static Par FindWay(char[,] map, int currentX, int currentY, int steps, int maxX, int maxY, List<Pos> posUsed, List<int> stepsUsed, List<PosStep> posStep)
    {
        List<Par> options = new List<Par>();
        posUsed.Add(new Pos() { X = currentX, Y = currentY });

        if (stepsUsed.Count > 0 && steps > stepsUsed.Min())
        {
            return new Par() { Steps = steps, HasSolution = false };
        }

        if (posStep.Any(p => p.Pos?.X == currentX && p.Pos.Y == currentY))
        {
            var aux = posStep.First(p => p.Pos.X == currentX && p.Pos.Y == currentY);
            if (aux.Steps < steps)
            {
                return new Par() { Steps = steps, HasSolution = false };
            }
            else
            {
                aux.Steps = steps;
            }
        }
        else
        {
            posStep.Add(new PosStep() { Pos = new Pos() { X = currentX, Y = currentY }, Steps = steps });
        }

        if (map[currentY, currentX] == 'E')
        {
            stepsUsed.Add(steps);
            return new Par() { Steps = steps, HasSolution = true };
        }

        if (currentX > 0 && Utils.IsNewMove(currentX - 1, currentY, posUsed))
        {
            if (Utils.CanMove(map[currentY, currentX], map[currentY, currentX - 1]))
            {
                var posUsedAux = Utils.NewPosUsed(posUsed);
                var parAux = FindWay(map, currentX - 1, currentY, steps + 1, maxX, maxY, posUsedAux, stepsUsed, posStep);
                if (parAux.HasSolution)
                    options.Add(parAux);
            }
        }
        if (currentX < maxX - 1 && Utils.IsNewMove(currentX + 1, currentY, posUsed))
        {
            if (Utils.CanMove(map[currentY, currentX], map[currentY, currentX + 1]))
            {
                var posUsedAux = Utils.NewPosUsed(posUsed);
                var parAux = FindWay(map, currentX + 1, currentY, steps + 1, maxX, maxY, posUsedAux, stepsUsed, posStep);
                if (parAux.HasSolution)
                    options.Add(parAux);
            }
        }
        if (currentY > 0 && Utils.IsNewMove(currentX, currentY - 1, posUsed))
        {
            if (Utils.CanMove(map[currentY, currentX], map[currentY - 1, currentX]))
            {
                var posUsedAux = Utils.NewPosUsed(posUsed);
                var parAux = FindWay(map, currentX, currentY - 1, steps + 1, maxX, maxY, posUsedAux, stepsUsed, posStep);
                if (parAux.HasSolution)
                    options.Add(parAux);
            }
        }
        if (currentY < maxY - 1 && Utils.IsNewMove(currentX, currentY + 1, posUsed))
        {
            if (Utils.CanMove(map[currentY, currentX], map[currentY + 1, currentX]))
            {
                var posUsedAux = Utils.NewPosUsed(posUsed);
                var parAux = FindWay(map, currentX, currentY + 1, steps + 1, maxX, maxY, posUsedAux, stepsUsed, posStep);
                if (parAux.HasSolution)
                    options.Add(parAux);
            }
        }

        if (options.Count > 0)
            return options.OrderBy(o => o.Steps).First();
        else
            return new Par() { Steps = steps, HasSolution = false };
    }

    public static bool CanMove(char orig, char dest)
    {
        if (orig == 'S' && dest == 'a')
            return true;
        if (orig == 'z' && dest == 'E')
            return true;
        if (dest == 'S' || dest == 'E')
            return false;
        if (dest - orig <= 1)
            return true;

        return false;
    }

    public static bool IsNewMove(int newX, int newY, List<Pos> posUsed)
    {        
        if (posUsed.Any(p => p.X == newX && p.Y == newY)) 
            return false;

        return true;
    }

    public static List<Pos> NewPosUsed(List<Pos> posUsed)
    {        
        return posUsed.ToList();
    }
}


class Par
{
    public int Steps { get; set; }
    public bool HasSolution { get; set; }
}

class Pos
{
    public int X { get; set; }
    public int Y { get; set; }
}

class PosStep
{
    public Pos Pos { get; set; }
    public int Steps { get; set; }
}