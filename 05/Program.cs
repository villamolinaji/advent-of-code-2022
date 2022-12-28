using _05;

string[] lines = File.ReadAllLines("input.txt");

List<string>[] columns = new List<string>[9];
List<string>[] columns2 = new List<string>[9];
for (int i = 0; i < 9; i++)
{
    columns[i] = new List<string>();
    columns2[i] = new List<string>();
}

int rowShip = 0;
List<Move> movements = new List<Move>();
foreach(string line in lines)
{    
    if (line.EndsWith("]"))
    {        
        int colIndex = 0;
        for (int i = 1; i < line.Length && colIndex < 9; i = i + 4)
        {
            if (line[i] != ' ')
            { 
                columns[colIndex].Add(line[i].ToString());
                columns2[colIndex].Add(line[i].ToString());
            }
            colIndex++;
        }

        rowShip = rowShip+ 1;
    }
    else if (line.StartsWith("move"))
    {
        var aux = line.Replace("move ", "");
        var howMany = aux.Substring(0, aux.IndexOf("from") - 1);
        var from = aux.Substring(aux.IndexOf("from") + 5, (aux.IndexOf("to") - (aux.IndexOf("from") + 5)) - 1);
        var to = aux.Substring(aux.IndexOf("to") + 3, aux.Length - (aux.IndexOf("to") + 3));
        movements.Add(new Move { HowMany = int.Parse(howMany), From = int.Parse(from), To = int.Parse(to) });
    }
}

foreach(var move in movements)
{
    var shipsFrom = columns[move.From - 1].Take(move.HowMany).ToList();
    foreach (var s in shipsFrom)
    {
        columns[move.To - 1].Insert(0, s);
    }
    columns[move.From - 1].RemoveRange(0, move.HowMany);
}

for (int i = 0; i < 9; i++)
{
    Console.Write(columns[i].First());
}

Console.WriteLine("");
Console.WriteLine("Second");
Console.WriteLine("");
foreach (var move in movements)
{
    var shipsFrom = columns2[move.From - 1].Take(move.HowMany).ToList();
    shipsFrom.Reverse();
    foreach (var s in shipsFrom)
    {
        columns2[move.To - 1].Insert(0, s);
    }
    columns2[move.From - 1].RemoveRange(0, move.HowMany);
}

for (int i = 0; i < 9; i++)
{
    Console.Write(columns2[i].First());
}