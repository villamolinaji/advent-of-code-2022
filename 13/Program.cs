string[] lines = File.ReadAllLines("input.txt");

int result = 0;

string firstLine = "";
string secondLine = "";

int index = 0;
var correctIndexs = new List<int>();

var secondPart = new List<List<List<int>>>();

foreach (var line in lines)
{
    if (!line.StartsWith("["))
    {
        firstLine = "";
        secondLine = "";

        continue;
    }

    if (string.IsNullOrEmpty(firstLine))
    {
        firstLine = line;
    }
    else if (string.IsNullOrEmpty(secondLine))
    {
        secondLine = line;
    }

    if (!string.IsNullOrEmpty(firstLine) && !string.IsNullOrEmpty(secondLine))
    {
        List<List<int>> leftPackages = Utils.GetPackages(firstLine);
        List<List<int>> rightPackages = Utils.GetPackages(secondLine);

        secondPart.Add(Utils.GetPackages(firstLine));
        secondPart.Add(Utils.GetPackages(secondLine));

        index++;
        if (Utils.CheckCorrectIndex(leftPackages, rightPackages))
        {
            correctIndexs.Add(index);
        }
    }
}

result = correctIndexs.Sum();
Console.WriteLine(result);

Utils.OrderList(secondPart);

foreach (var line in secondPart)
{
    Console.WriteLine();
   
    var aux = line;
    while(aux.Count > 0)
    {
        var print = aux.Last();
        aux.RemoveAt(aux.Count - 1);
        Console.Write("[");
        foreach(var number in print)
        {
            Console.Write(number);
        }
        Console.Write("]");
    }    
}



class Utils
{
    public static List<List<int>> GetPackages(string line)
    {
        int brackets = 0;
        var lineAux = line;
        List<int> openBracktes = new List<int>();
        List<int> closeBracktes = new List<int>();
        List<List<int>> result = new List<List<int>>();

        //var s = lineAux.First();

        int cont = 0;
        foreach (var s in line)
        {
            if (s == '[')
            {
                brackets++;
                openBracktes.Add(cont);
            }
            else if (s == ']')
            {

                closeBracktes.Add(cont);

                int openPos = 0;
                for (int i = 0; i < brackets; i++)
                {
                    if (i > 0)
                        openPos = lineAux.IndexOf('[', openPos + 1);
                    else
                        openPos = lineAux.IndexOf('[', openPos);
                }
                brackets--;
              
                var s2 = lineAux.Substring(openPos + 1, (lineAux.IndexOf(']') - openPos) - 1);
                lineAux = lineAux.Substring(0, openPos) + lineAux.Substring(lineAux.IndexOf(']') + 1, lineAux.Length - (lineAux.IndexOf(']') + 1));

                openBracktes.RemoveAt(openBracktes.Count - 1);
                closeBracktes.RemoveAt(closeBracktes.Count - 1);

                if (s2 != ",")
                {
                    var numbers = new List<int>();
                    foreach (var number in s2.Split(","))
                    {
                        int outNumber = 0;
                        if (int.TryParse(number, out outNumber))
                        {
                            numbers.Add(outNumber);
                        }
                    }
                    result.Add(numbers);
                }
            }
            cont++;
        }

        return result;
    }

    public static bool CheckCorrectIndex(List<List<int>> left, List<List<int>> right)
    {
        bool result = true;

        while (left.Count > 0 && result)
        {
            if (right.Count == 0)
            {
                result = false;
                break;
            }

            var firstLeft = left.First();
            var firstRight = right.First();

            left.RemoveAt(0);
            right.RemoveAt(0);

            for (int i = 0; i < firstLeft.Count; i++)
            {
                if (firstRight.Count <= i)
                {
                    result = false;
                    break;
                }
                if (firstRight[i] < firstLeft[i])
                {
                    result = false;
                    break;
                }
                else if (firstRight[i] > firstLeft[i])
                {
                    break;
                }
            }
        }

        return result;
    }

    public static void OrderList(List<List<List<int>>> list)
    {
        var orderedList = new List<List<List<int>>>();
        var auxList = new List<List<List<int>>>();
        foreach(var li in list)
        {
            auxList.Add(li);
        }
        while (auxList.Count > 0)
        {
            var aux = auxList[0];
            for (int i = 1; i < auxList.Count - 1; i++)
            {
                if (!Utils.IsLower(aux, auxList[i]))
                {
                    aux = list[i];
                }
            }
            orderedList.Add(aux);
            auxList.Remove(aux);
        }              
    }

    public static bool IsLower(List<List<int>> left, List<List<int>> right)
    {
        bool result = true;

        List<List<int>> leftCopy = new List<List<int>>();
        List<List<int>> rightCopy = new List<List<int>>();
        foreach(var l in left)
        {
            leftCopy.Add(l);
        }
        foreach (var r in right)
        {
            rightCopy.Add(r);
        }

        while (leftCopy.Count > 0 && result)
        {
            if (rightCopy.Count == 0)
            {
                result = false;
                break;
            }

            var lastLeft = leftCopy.Last();
            var lastRight = rightCopy.Last();

            leftCopy.RemoveAt(leftCopy.Count - 1);
            rightCopy.RemoveAt(rightCopy.Count - 1);

            for (int i = 0; i < lastLeft.Count; i++)
            {
                if (lastRight.Count <= i)
                {
                    result = true;
                    break;
                }
                if (lastLeft[i] > lastRight[i])
                {
                    result = false;
                    break;
                }
                if (lastLeft[i] < lastRight[i])
                {
                    result = true;
                    break;
                }
            }
        }

        return result;
    }
}