string[] lines = File.ReadAllLines("input.txt");

var numbers = new List<long>();
foreach (var line in lines)
{
    numbers.Add(CalculateToDecimal(line));
}

long total = numbers.Sum();
var result = ConvertToSNAFU(total);

Console.WriteLine(result);


long CalculateToDecimal(string snafu)
{
    long factor = 1;
    long result = 0;

    for (int i = snafu.Length - 1; i >= 0; i--)
    {
        long value = 0;
        switch(snafu[i])
        {
            case '2':
                value = 2 * factor;
                break;
            case '1':
                value = 1 * factor;
                break;
            case '0':
                value = 0 * factor;
                break;
            case '-':
                value = -1 * factor;
                break;
            case '=':
                value = -2 * factor;
                break;
        }
        result = result + value;
        factor = factor * 5;
    }  

    return result;
}

string ConvertToSNAFU(long number)
{
    var result = "";
    var snafuMapping = new Dictionary<long, char>
    {
        { -2, '=' },
        { -1, '-' },
        { 0, '0' },
        { 1, '1' },
        { 2, '2' }
    };

    while (number > 0)
    {
        result = snafuMapping[((number + 2) % 5) - 2] + result ;
        number = (number + 2) / 5;
    }

    return result;
}