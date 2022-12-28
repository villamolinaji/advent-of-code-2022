using _08;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;
int row = 0;
int column = 0;

Tree[,] trees = new Tree[lines.Length, lines[0].Length];

foreach (string line in lines)
{
    foreach(var s in line)
    {
        if (s != ' ')
        {
            trees[row, column] = new Tree() { Height = int.Parse(s.ToString()) };
        }     
        column++;
    }
    column = 0;
    row++;
}

for(int i = 0; i < lines.Length; i ++)
{
    for (int j = 0; j < lines[0].Length; j ++)
    {       
        trees[i, j].IsVisibleO = Tree.CalculateIsVisible0(trees, i, j, trees[i, j].Height);
        trees[i, j].IsVisibleN = Tree.CalculateIsVisibleN(trees, i, j, trees[i, j].Height);
        trees[i, j].IsVisibleE = Tree.CalculateIsVisibleE(trees, i, j, trees[i, j].Height, lines[0].Length);
        trees[i, j].IsVisibleS = Tree.CalculateIsVisibleS(trees, i, j, trees[i, j].Height, lines.Length);

        Tree.CalculateVisible0(trees, trees[i, j], i, j, trees[i, j].Height);
        Tree.CalculateVisibleN(trees, trees[i, j], i, j, trees[i, j].Height);
        Tree.CalculateVisibleE(trees, trees[i, j], i, j, trees[i, j].Height, lines[0].Length);
        Tree.CalculateVisibleS(trees, trees[i, j], i, j, trees[i, j].Height, lines.Length);
    }
}

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[0].Length; j++)
    {
        if (trees[i, j].IsVisibleO || trees[i, j].IsVisibleN || trees[i, j].IsVisibleE || trees[i, j].IsVisibleS)
        {
            result++;
        }        
    }

}

Console.WriteLine(result.ToString());

int result2 = 0;

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[0].Length; j++)
    {
        trees[i, j].ScenicValue = trees[i, j].VisibleO * trees[i, j].VisibleN * trees[i, j].VisibleE * trees[i, j].VisibleS;
    }
}

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[0].Length; j++)
    {
        if (trees[i, j].ScenicValue > result2)
        {
            result2 = trees[i, j].ScenicValue;
        }
    }
}


Console.WriteLine(result2.ToString());