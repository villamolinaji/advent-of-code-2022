

var data =
    (
        from line in File.ReadAllLines("input.txt")
        where !string.IsNullOrWhiteSpace(line)
        select long.Parse(line)
    ).ToArray();

// part 1
// var numbers = data.Select(d => new LinkedListNode<long>(d)).ToArray();

// part 2
var numbers = data.Select(d => new LinkedListNode<long>(d * 811589153)).ToArray();

var linkedList = new LinkedList<long>();
foreach (var number in numbers)
{
    linkedList.AddLast(number);
}

// part 1
//Decrypt(numbers, linkedList);

// part 2
for (int i = 0; i < 10; i++)
{
    Decrypt(numbers, linkedList);
}

long result = GetResult(numbers, linkedList);

Console.WriteLine(result);



void Decrypt(LinkedListNode<long>[] numbers, LinkedList<long> linkedList)
{
    foreach (var number in numbers)
    {
        var positions = number.Value % (numbers.Length - 1);

        if (positions > 0)
        {
            var after = number.Next ?? linkedList.First;
            linkedList.Remove(number);

            while (positions-- > 0)
            {
                after = after!.Next ?? linkedList.First;
            }

            linkedList.AddBefore(after!, number);
        }
        else if (positions < 0)
        {
            var before = number.Previous ?? linkedList.Last;
            linkedList.Remove(number);

            while (positions++ < 0)
            {
                before = before!.Previous ?? linkedList.Last;
            }

            linkedList.AddAfter(before!, number);
        }
    }
}

long GetResult(LinkedListNode<long>[] numbers, LinkedList<long> linkedList)
{
    long result = 0;
    var targetNode = numbers.Where(n => n.Value == 0L).Single();

    for (int i = 0; i < 3; ++i)
    {
        var moveCount = 1000 % linkedList.Count;
        while (moveCount-- > 0)
        {
            targetNode = targetNode!.Next ?? linkedList.First;
        }
        result += targetNode!.Value;
    }

    return result;
}
