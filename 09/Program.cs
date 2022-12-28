using _09;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;

var posVisited = new List<Position>();
var t = new Position() { X = 0, Y = 0 };
var h = new Position() { X = 0, Y = 0 };
var prevH = new Position() { X = 0, Y = 0 };

foreach (var line in lines)
{
    var direction = line[0];
    int steps = int.Parse(line.Substring(2));

    for (int i = 0; i < steps; i++)
    {
        prevH.X = h.X;
        prevH.Y = h.Y;
        switch (direction)
        {
            case 'R':
                h.X = h.X + 1;
                break;
            case 'U':
                h.Y = h.Y + 1;
                break;
            case 'L':
                h.X = h.X - 1;
                break;
            case 'D':
                h.Y = h.Y - 1;
                break;
        }
        if (Math.Abs(h.X - t.X) > 1 || Math.Abs(h.Y - t.Y) > 1)
        {
            t.X = prevH.X;
            t.Y = prevH.Y;
        }

        if (!posVisited.Any(v => v.X == t.X && v.Y == t.Y))
        {
            result++;
            posVisited.Add(new Position() { X = t.X, Y = t.Y });
        }
    }    
}

Console.WriteLine(result.ToString());


result = 0;
posVisited = new List<Position>();
h = new Position() { X = 0, Y = 0 };
prevH = new Position() { X = 0, Y = 0 };
Position[] t9 = new Position[9];
for (int i = 0; i < 9; i++)
{
    t9[i] = new Position() { X = 0, Y = 0 };
}
/*Position[] prevT9 = new Position[8];
for (int i = 0; i < 8; i++)
{
    prevT9[i] = new Position() { X = 0, Y = 0 };
}*/


foreach (var line in lines)
{
    var direction = line[0];
    int steps = int.Parse(line.Substring(2));

    for (int i = 0; i < steps; i++)
    {
        prevH.X = h.X;
        prevH.Y = h.Y;
        switch (direction)
        {
            case 'R':
                h.X = h.X + 1;
                break;
            case 'U':
                h.Y = h.Y + 1;
                break;
            case 'L':
                h.X = h.X - 1;
                break;
            case 'D':
                h.Y = h.Y - 1;
                break;
        }

        if (Math.Abs(h.X - t9[0].X) > 1 || Math.Abs(h.Y - t9[0].Y) > 1)
        {
            t9[0].X = prevH.X;
            t9[0].Y = prevH.Y;
        }

        for (int j = 1; j < 9; j++)
        {
            if (Math.Abs(t9[j].X - t9[j-1].X) > 1 || Math.Abs(t9[j].Y - t9[j-1].Y) > 1)
            {
                switch (direction)
                {
                    case 'R':     

                        if (t9[j].Y == t9[j - 1].Y)
                        {
                            if (t9[j].X != t9[j - 1].X)
                            {
                                t9[j].X = t9[j - 1].X - 1;
                            }
                        }
                        else
                        {
                            if (t9[j - 1].Y > t9[j].Y) t9[j].Y++;
                            else t9[j].Y--;
                        }

                        break;
                    case 'U':
                        if (t9[j].X == t9[j - 1].X)
                        {
                            if (t9[j].Y != t9[j - 1].Y)
                            {
                                t9[j].Y = t9[j - 1].Y - 1;
                            }
                        }
                        else
                        {
                            if (t9[j - 1].X > t9[j].X) t9[j].X++;
                            else t9[j].X--;
                        }
                        break;
                    case 'L':
                        if (t9[j].Y == t9[j - 1].Y)
                        {
                            if (t9[j].X != t9[j - 1].X)
                            {
                                t9[j].X = t9[j - 1].X + 1;
                            }
                        }
                        else
                        {
                            if (t9[j - 1].Y > t9[j].Y) t9[j].Y++;
                            else t9[j].Y--;
                        }                           
                        
                        break;
                    case 'D':                        
                        if (t9[j].X == t9[j - 1].X)
                        {
                            if (t9[j].Y != t9[j - 1].Y)
                            {
                                t9[j].Y = t9[j - 1].Y + 1;
                            }
                        }
                        else
                        {
                            if (t9[j - 1].X > t9[j].X) t9[j].X++;
                            else t9[j].X--;
                        }

                        break;
                }
            }
            else
            {
                break;
            }
        }

        if (!posVisited.Any(v => v.X == t9[8].X && v.Y == t9[8].Y))
        {
            result++;
            posVisited.Add(new Position() { X = t9[8].X, Y = t9[8].Y });
        }
    }
}

Console.WriteLine(result.ToString());