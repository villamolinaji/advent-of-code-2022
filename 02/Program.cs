using RockPaperScissors;

string[] lines = File.ReadAllLines("input.txt");

List<Round> rounds = new List<Round>();

foreach (var line in lines)
{
    string elf = line.Substring(0, 1);
    string me = line.Substring(2, 1);
    rounds.Add(new Round() { Elf = elf, Me = me });
}

int score = 0;
foreach (var round in rounds)
{
    switch (round.Me)
    {
        case "X": score = score + 1; break;
        case "Y": score = score + 2; break;
        case "Z": score = score + 3; break;
        default: break;
    }

    switch (round.Elf)
    {
        case "A":
            if (round.Me == "Y") score = score + 6;
            else if (round.Me == "X") score = score + 3;
            break;
        case "B":
            if (round.Me == "Z") score = score + 6;
            else if (round.Me == "Y") score = score + 3;
            break;
        case "C":
            if (round.Me == "X") score = score + 6;
            else if (round.Me == "Z") score = score + 3;
            break;
        default: break;
    }
}

Console.WriteLine(score);

int score2 = 0;
foreach (var round in rounds)
{
    switch (round.Me)
    {
        case "X": //lose
            if (round.Elf == "A") score2 = score2 + 3;
            else if (round.Elf == "B") score2 = score2 + 1;
            else if (round.Elf == "C") score2 = score2 + 2;
            break;
        case "Y": //draw
            if (round.Elf == "A") score2 = score2 + 1 + 3;
            else if (round.Elf == "B") score2 = score2 + 2 + 3;
            else if (round.Elf == "C") score2 = score2 + 3 + 3;
            break;
        case "Z": //win
            if (round.Elf == "A") score2 = score2 + 2 + 6;
            else if (round.Elf == "B") score2 = score2 + 3 + 6;
            else if (round.Elf == "C") score2 = score2 + 1 + 6;
            break;
        default: break;
    }
}

Console.WriteLine(score2);