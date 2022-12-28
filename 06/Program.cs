string[] lines = File.ReadAllLines("input.txt");

int result = 0;

string input = lines[0];
int cont = 0;
char[] array = new char[14];

foreach(var s in input)
{
    result++;
    if (!array.Contains(s))
    {
        array[cont] = s;
        cont++;
        if (cont == 14)
        {
            break;
        }
    }
    else
    {
        for (int i = 0; i < cont; i++)
        {
            if (array[i] == s)
            {
                for (int j = 0; j < cont - i - 1; j++)
                {
                    array[j] = array[i + j + 1];
                }
                cont = cont - (i + 1);
                array[cont] = s;                
                cont++;
                for (int j = cont; j < 14; j++)
                {
                    array[j] = ' ';
                }
                break;
            }
        }
    }    
}

Console.WriteLine(result.ToString());