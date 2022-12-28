using _19;

string[] lines = File.ReadAllLines("input.txt");

var bluePrints = new List<Blueprint>();
foreach (var line in lines)
{
    var bluePrint = new Blueprint();
    var aux = line.Split('.');
    bluePrint.Id = int.Parse(aux[0].Substring(10, aux[0].IndexOf(':') - 10));
    aux[0] = aux[0].Replace("Each ore robot", "");
    bluePrint.OreCost = int.Parse(aux[0].Substring(aux[0].IndexOf("costs") + 5, aux[0].IndexOf("ore") - (aux[0].IndexOf("costs") + 5)));

    bluePrint.ClayOreCost = int.Parse(aux[1].Substring(aux[1].IndexOf("costs") + 5, aux[1].IndexOf("ore") - (aux[1].IndexOf("costs") + 5)));

    bluePrint.ObsidianOreCost = int.Parse(aux[2].Substring(aux[2].IndexOf("costs") + 5, aux[2].IndexOf("ore") - (aux[2].IndexOf("costs") + 5)));
    bluePrint.ObsidianClayCost = int.Parse(aux[2].Substring(aux[2].IndexOf("and") + 3, aux[2].IndexOf("clay") - (aux[2].IndexOf("and") + 3)));

    bluePrint.GeodeOreCost = int.Parse(aux[3].Substring(aux[3].IndexOf("costs") + 5, aux[3].IndexOf("ore") - (aux[3].IndexOf("costs") + 5)));
    bluePrint.GeodeObsidianCost = int.Parse(aux[3].Substring(aux[3].IndexOf("and") + 3, aux[3].IndexOf("obsidian") - (aux[3].IndexOf("and") + 3)));

    bluePrints.Add(bluePrint);
}

//part 2
var newBlueprints = bluePrints.Take(3).ToList();

foreach (var bluePrint in newBlueprints)
{
    bluePrint.TotalGeodes = Blueprint.CollectGeodes2(bluePrint);
}

int result = 0;
foreach (var bluePrint in newBlueprints)
{
    //result = result + (bluePrint.TotalGeodes * bluePrint.Id);
    result = result * bluePrint.TotalGeodes;
}

Console.WriteLine(result);
// p1: 1192
// p2: 14725