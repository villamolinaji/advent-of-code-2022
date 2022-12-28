using _04;

string[] lines = File.ReadAllLines("input.txt");

int count = 0;
int count2 = 0;
foreach (var line in lines)
{
    var elf1 = new Elf();
    elf1.MySections = new List<int>();
    var elf2 = new Elf();
    elf2.MySections = new List<int>();
    var twoSection = line.Split(',');
    var sectionFirst = twoSection[0].Split('-');
    var sectionSecond = twoSection[1].Split('-');
    var section1 = sectionFirst[0];
    var section2 = sectionFirst[1];
    var section3 = sectionSecond[0];
    var section4 = sectionSecond[1];  

    int s1 = int.Parse(section1);
    int s2 = int.Parse(section2);
    int s3 = int.Parse(section3);
    int s4 = int.Parse(section4);

    for (int i = s1; i<= s2; i++)
    {
        elf1.MySections.Add(i);
    }
    for (int i = s3; i <= s4; i++)
    {
        elf2.MySections.Add(i);
    }

    if (s1 <= s3 && s2 >= s4) count++;
    else if (s3 <= s1 && s4 >= s2) count++;

    foreach (var section in elf1.MySections)
    {
        if (elf2.MySections.Contains(section))
        {
            count2 = count2 + 1; 
            break;
        }
    }    
}

Console.WriteLine(count);
Console.WriteLine(count2);