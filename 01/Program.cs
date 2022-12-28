
string[] lines = File.ReadAllLines("input.txt");

int elfCalories = 0;
List<int> elfs = new List<int>();
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        elfs.Add(elfCalories);
        elfCalories = 0;
    }
    else
    {
        int outCalories = 0;
        int.TryParse(line, out outCalories);
        elfCalories = elfCalories + outCalories;
    }
}

int maxCalories = elfs.Max();

var elfsTop = elfs.OrderByDescending(e => e).Take(3);
var elfsTopCalories = elfsTop.Sum();
Console.WriteLine($"Max calories: ${maxCalories}");
Console.WriteLine($"To 3 calories: ${elfsTopCalories}");

