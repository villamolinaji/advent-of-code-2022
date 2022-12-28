using _11;

string[] lines = File.ReadAllLines("input.txt");

long result = 0;

/*
List<Monkey> monkeys = new List<Monkey>();
var monkey = new Monkey();
monkey.Index = 0;
monkey.Items = new List<long>() { 79, 98 };
monkey.IsSum = false;
monkey.OperationValue = 19;
monkey.DivisibleValue = 23;
monkey.IfTrueMonkey = 2;
monkey.IfFalseMonkey = 3;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 1;
monkey.Items = new List<long>() { 54, 65, 75, 74 };
monkey.IsSum = true;
monkey.OperationValue = 6;
monkey.DivisibleValue = 19;
monkey.IfTrueMonkey = 2;
monkey.IfFalseMonkey = 0;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 2;
monkey.Items = new List<long>() { 79, 60, 97 };
monkey.IsSum = false;
monkey.OperationValue = 0;
monkey.DivisibleValue = 13;
monkey.IfTrueMonkey = 1;
monkey.IfFalseMonkey = 3;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 3;
monkey.Items = new List<long>() { 74 };
monkey.IsSum = true;
monkey.OperationValue = 3;
monkey.DivisibleValue = 17;
monkey.IfTrueMonkey = 0;
monkey.IfFalseMonkey = 1;
monkey.InspectedCount = 0;
monkeys.Add(monkey);
*/

List<Monkey> monkeys = new List<Monkey>();
var monkey = new Monkey();
monkey.Index = 0;
monkey.Items = new List<long>() { 96, 60, 68, 91, 83, 57, 85 };
monkey.IsSum = false;
monkey.OperationValue = 2;
monkey.DivisibleValue = 17;
monkey.IfTrueMonkey = 2;
monkey.IfFalseMonkey = 5;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 1;
monkey.Items = new List<long>() { 75, 78, 68, 81, 73, 99 };
monkey.IsSum = true;
monkey.OperationValue = 3;
monkey.DivisibleValue = 13;
monkey.IfTrueMonkey = 7;
monkey.IfFalseMonkey = 4;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 2;
monkey.Items = new List<long>() { 69, 86, 67, 55, 96, 69, 94, 85 };
monkey.IsSum = true;
monkey.OperationValue = 6;
monkey.DivisibleValue = 19;
monkey.IfTrueMonkey = 6;
monkey.IfFalseMonkey = 5;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 3;
monkey.Items = new List<long>() { 88, 75, 74, 98, 80 };
monkey.IsSum = true;
monkey.OperationValue = 5;
monkey.DivisibleValue = 7;
monkey.IfTrueMonkey = 7;
monkey.IfFalseMonkey = 1;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 4;
monkey.Items = new List<long>() { 82 };
monkey.IsSum = true;
monkey.OperationValue = 8;
monkey.DivisibleValue = 11;
monkey.IfTrueMonkey = 0;
monkey.IfFalseMonkey = 2;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 5;
monkey.Items = new List<long>() { 72, 92, 92 };
monkey.IsSum = false;
monkey.OperationValue = 5;
monkey.DivisibleValue = 3;
monkey.IfTrueMonkey = 6;
monkey.IfFalseMonkey = 3;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 6;
monkey.Items = new List<long>() { 74, 61 };
monkey.IsSum = false;
monkey.OperationValue = 0;
monkey.DivisibleValue = 2;
monkey.IfTrueMonkey = 3;
monkey.IfFalseMonkey = 1;
monkey.InspectedCount = 0;
monkeys.Add(monkey);

monkey = new Monkey();
monkey.Index = 7;
monkey.Items = new List<long>() { 76, 86, 83, 55 };
monkey.IsSum = true;
monkey.OperationValue = 4;
monkey.DivisibleValue = 5;
monkey.IfTrueMonkey = 4;
monkey.IfFalseMonkey = 0;
monkey.InspectedCount = 0;
monkeys.Add(monkey);


long group = 1;
foreach (var m in monkeys)
{
    group *= m.DivisibleValue;
}

for (int round = 0; round < 10000; round ++)
{
    if (round == 1 || round == 20 || round == 1000)
    {
        Console.WriteLine($"Round {round}");
        foreach (var m in monkeys)
        {
            Console.WriteLine($"Monkey {m.Index} - {m.InspectedCount}");
        }
    }
    foreach(var m in monkeys)
    {
        var itemsAux = new List<long>();
        foreach (var item in m.Items)
        {
            itemsAux.Add(item);
        }
        foreach (var item in itemsAux)
        {
            Utils.CalculateItem2(m, item, monkeys, group);                       
        }
    }
}

var top = monkeys.OrderByDescending(m => m.InspectedCount).Take(2).ToList();

result = top[0].InspectedCount * top[1].InspectedCount;


Console.WriteLine(result.ToString());


class Utils
{
    public static void CalculateItem(Monkey monkey, long item, List<Monkey> monkeys)
    {
        long worry = item;
        long opValue = monkey.OperationValue;
        if (monkey.OperationValue == 0) 
            opValue = worry;

        if (monkey.IsSum) 
            worry = worry + opValue;
        else 
            worry = worry * opValue;

        long division = (long)worry / (long)3;
        var intDivision = (int)division;        

        int dest = monkey.IfFalseMonkey;
        if (worry % monkey.DivisibleValue == 0) 
            dest = monkey.IfTrueMonkey;


        monkey.InspectedCount++;
        var m2 = monkeys.First(m => m.Index == dest);
        m2.Items.Add(intDivision);        
        monkey.Items.RemoveAt(0);
    }

    public static void CalculateItem2(Monkey monkey, long item, List<Monkey> monkeys, long group)
    {
        long worry = item;
        long opValue = monkey.OperationValue;
        if (monkey.OperationValue == 0)
            opValue = worry;

        if (monkey.IsSum)
            worry = worry + opValue;
        else
            worry = worry * opValue;

        var intDivision = worry % group;

        int dest = monkey.IfFalseMonkey;
        if (worry % monkey.DivisibleValue == 0)
            dest = monkey.IfTrueMonkey;


        monkey.InspectedCount++;
        var m2 = monkeys.First(m => m.Index == dest);
        m2.Items.Add(intDivision);
        monkey.Items.RemoveAt(0);
    }
}