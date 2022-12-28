using _21;

string[] lines = File.ReadAllLines("input.txt");

var monkeys = new Dictionary<string, Monkey>();

foreach (var line in lines)
{
    var monkey = new Monkey();

    var auxLine = line.Split(':');
    monkey.Name = auxLine[0];

    if (auxLine[1].Contains("+") || auxLine[1].Contains("-") || auxLine[1].Contains("*") || auxLine[1].Contains("/"))
    {
        if (auxLine[1].Contains("+"))
        {
            var auxLine2 = auxLine[1].Split('+');
            monkey.Operation = '+';
            monkey.Monkey1 = auxLine2[0].Trim();
            monkey.Monkey2 = auxLine2[1].Trim();
        }
        else if (auxLine[1].Contains("-"))
        {
            var auxLine2 = auxLine[1].Split('-');
            monkey.Operation = '-';
            monkey.Monkey1 = auxLine2[0].Trim();
            monkey.Monkey2 = auxLine2[1].Trim();
        }
        else if (auxLine[1].Contains("*"))
        {
            var auxLine2 = auxLine[1].Split('*');
            monkey.Operation = '*';
            monkey.Monkey1 = auxLine2[0].Trim();
            monkey.Monkey2 = auxLine2[1].Trim();
        }
        else if (auxLine[1].Contains("/"))
        {
            var auxLine2 = auxLine[1].Split('/');
            monkey.Operation = '/';
            monkey.Monkey1 = auxLine2[0].Trim();
            monkey.Monkey2 = auxLine2[1].Trim();
        }
    }
    else
    {
        monkey.Value = long.Parse(auxLine[1]);
    }

    monkeys.Add(monkey.Name, monkey);
}


//long root = DoPart1(monkeys);
//Console.WriteLine(root);
// 160274622817992

long root = DoPart2(monkeys);
Console.WriteLine(root);
// 3087390115721

long DoPart1(Dictionary<string, Monkey> monkeys)
{
    bool isFilled = false;
    long root = 0;
    while (!isFilled)
    {
        //isFilled= true;
        foreach (var monkey in monkeys)
        {
            if (monkey.Value.Value == null)
            {
                long? value1 = monkeys[monkey.Value.Monkey1].Value;
                long? value2 = monkeys[monkey.Value.Monkey2].Value;

                if (value1 != null && value2 != null)
                {
                    switch (monkey.Value.Operation)
                    {
                        case '+':
                            monkey.Value.Value = value1 + value2;
                            break;
                        case '-':
                            monkey.Value.Value = value1 - value2;
                            break;
                        case '*':
                            monkey.Value.Value = value1 * value2;
                            break;
                        case '/':
                            monkey.Value.Value = value1 / value2;
                            break;
                    }

                    if (monkey.Key == "root")
                    {
                        root = (long)monkey.Value.Value;
                        isFilled = true;
                        break;
                    }
                }                
            }
        }
    }

    return root;
}

long DoPart2(Dictionary<string, Monkey> monkeys)
{
    bool isFilled = false;    
    long? valueToCompare = 0;
    bool isMonkey1 = false;

    monkeys["humn"].Value = null;

    while (!isFilled)
    {
        //isFilled= true;
        foreach (var monkey in monkeys)
        {
            if (monkey.Value.Value == null && monkey.Key != "humn")
            {
                long? value1 = monkeys[monkey.Value.Monkey1].Value;
                long? value2 = monkeys[monkey.Value.Monkey2].Value;

                if (value1 != null && value2 != null)
                {
                    switch (monkey.Value.Operation)
                    {
                        case '+':
                            monkey.Value.Value = value1 + value2;
                            break;
                        case '-':
                            monkey.Value.Value = value1 - value2;
                            break;
                        case '*':
                            monkey.Value.Value = value1 * value2;
                            break;
                        case '/':
                            monkey.Value.Value = value1 / value2;
                            break;
                    }

                    
                }
                if (monkey.Key == "root")
                {
                    if (value1 != null)
                    {
                        valueToCompare = value1;
                        isMonkey1 = false;
                        isFilled = true;
                        break;
                    }
                    else if (value2 != null)
                    {
                        valueToCompare = value2;
                        isMonkey1 = true;
                        isFilled = true;
                        break;
                    }
                }
            }
        }
    }

    var result = ResolveValueToCompare(monkeys, (long)valueToCompare, isMonkey1);

    monkeys["humn"].Value = result;
    isFilled = false;
    while (!isFilled)
    {
        //isFilled= true;
        foreach (var monkey in monkeys)
        {
            if (monkey.Value.Value == null)
            {
                long? value1 = monkeys[monkey.Value.Monkey1].Value;
                long? value2 = monkeys[monkey.Value.Monkey2].Value;

                if (value1 != null && value2 != null)
                {
                    switch (monkey.Value.Operation)
                    {
                        case '+':
                            monkey.Value.Value = value1 + value2;
                            break;
                        case '-':
                            monkey.Value.Value = value1 - value2;
                            break;
                        case '*':
                            monkey.Value.Value = value1 * value2;
                            break;
                        case '/':
                            monkey.Value.Value = value1 / value2;
                            break;
                    }


                }
                if (monkey.Key == "root")
                {
                    if (value1 != null && value2 != null)
                    {                        
                        isFilled = true;
                        break;
                    }                    
                }
            }
        }
    }

    return result;
}

long ResolveValueToCompare(Dictionary<string, Monkey> monkeys, long valueToCompare, bool isMonkey1)
{
    string monkeyStart = "";
    string monkeyNext = "";
    bool isNextMonkey1 = false;
    long humn = 0;
    long nextValueToCompare = valueToCompare;

    if (isMonkey1)
    {
        monkeyStart = monkeys["root"].Monkey1;
    }
    else
    { 
        monkeyStart = monkeys["root"].Monkey2; 
    }

    bool isFound = false;

    while(!isFound)
    {
        var monkey = monkeys[monkeyStart];
        long value = 0;

        if (monkeys[monkey.Monkey1].Value != null)
        {
            value = (long)monkeys[monkey.Monkey1].Value;
            isNextMonkey1 = false;
            monkeyNext = monkey.Monkey2;
        }
        else if (monkeys[monkey.Monkey2].Value != null)
        {
            value = (long)monkeys[monkey.Monkey2].Value;
            isNextMonkey1 = true;
            monkeyNext = monkey.Monkey1;
        }

        switch (monkey.Operation)
        {
            case '+':                
                if (isNextMonkey1)
                    nextValueToCompare = nextValueToCompare - value;
                else
                    nextValueToCompare = nextValueToCompare - value;
                break;
            case '-':                
                if (isNextMonkey1)
                    nextValueToCompare = nextValueToCompare + value;
                else
                    nextValueToCompare = value - nextValueToCompare;
                break;                
            case '*':                
                if (isNextMonkey1)
                    nextValueToCompare = nextValueToCompare / value;
                else
                    nextValueToCompare = nextValueToCompare / value;                
                break;
            case '/':                
                if (isNextMonkey1)
                    nextValueToCompare = nextValueToCompare * value;
                else
                    nextValueToCompare = value / nextValueToCompare;
                break;
        }

        if (monkeyNext == "humn")
        {
            isFound = true;
            humn = nextValueToCompare;
        }
        else
        {
            monkeyStart = monkeyNext;
        }

    }

    return humn;
}