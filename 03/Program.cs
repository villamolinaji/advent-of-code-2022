using _03_AdventOfCode;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;
foreach (string line in lines)
{

    string firstPart = line.Substring(0, line.Length / 2);
    string secondPart = line.Substring(line.Length / 2, line.Length / 2);

    char commonChar = ' ';
    foreach (var c in firstPart)
    {
        if (secondPart.Contains(c))
        {
            commonChar = c;
            break;
        }
    }

    result = result + Utils.GetCharValue(commonChar);
}

Console.WriteLine(result);


result = 0;
List<string> firstGroup = new List<string>();
List<string> secondGroup = new List<string>();
int count = 0;
foreach (string line in lines)
{
    count = count + 1;
    if (count < 4)
        firstGroup.Add(line);
    else
        secondGroup.Add(line);

    if (count == 6)
    {
        count = 0;
        char commonCharFirstGroup = ' ';
        char commonCharSecondGroup = ' ';
        foreach (var c in firstGroup.First())
        {
            if (firstGroup[1].Contains(c) && firstGroup[2].Contains(c))
            {
                commonCharFirstGroup = c;
                break;
            }
        }
        foreach (var c in secondGroup.First())
        {
            if (secondGroup[1].Contains(c) && secondGroup[2].Contains(c))
            {
                commonCharSecondGroup = c;
                break;
            }
        }

        result = result + Utils.GetCharValue(commonCharFirstGroup);
        result = result + Utils.GetCharValue(commonCharSecondGroup);

        firstGroup.Clear();
        secondGroup.Clear();
    }
}

Console.WriteLine(result);