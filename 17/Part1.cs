/*
using _17;

string[] lines = File.ReadAllLines("input.txt");

string directions = lines[0];

var rocks = new Rock[5];
rocks[0] = new Rock()
{
    Positions = new List<(long, long)>()
    {
        (2, 0),
        (3, 0),
        (4, 0),
        (5, 0),
    }
};
rocks[1] = new Rock()
{
    Positions = new List<(long, long)>()
    {
        (3, 0),
        (2, 1),
        (3, 1),
        (4, 1),
        (3, 2),
    }
};
rocks[2] = new Rock()
{
    Positions = new List<(long, long)>()
    {
        (2, 0),
        (3, 0),
        (4, 0),
        (4, 1),
        (4, 2),
    }
};
rocks[3] = new Rock()
{
    Positions = new List<(long, long)>()
    {
        (2, 0),
        (2, 1),
        (2, 2),
        (2, 3),
    }
};
rocks[4] = new Rock()
{
    Positions = new List<(long, long)>()
    {
        (2, 0),
        (3, 0),
        (2, 1),
        (3, 1),
    }
};

long result = Utils.DoSimulation(rocks, directions);
Console.WriteLine(result);


class Utils
{
    public static long DoSimulation(Rock[] rocks, string directions)
    {
        //long numRocks = 2022;
        long numRocks = 1000000000000;

        long rockCount = 0;
        long iterations = 0;
        long height = -1;
        List<(long, long)> tower = new List<(long, long)>();

        int loopSize = 500;
        bool initLoop = false;
        bool isInLoop = false;
        long loopRockCountStart = 0;
        long loopIterationsStart = 0;
        long loopHeight = 0;
        long loopRockCount = 0;


        while (rockCount < numRocks)
        {
            Rock rock = Rock.GetNewRock(rocks, rockCount, height);

            bool isFalling = true;

            while (isFalling)
            {
                //if (iterations % directions.Length == 0)
                //{
                //    if (rockCount > loopSize)
                //    {
                //        initLoop = true;
                //    }
                //}

                rock = Rock.MoveRock(rock, directions, iterations, tower);
                iterations++;

                bool isFinish = false;
                rock = Rock.DownRock(rock, tower, out isFinish);
                if (isFinish)
                {
                    long newHeight = Rock.AddRockIntoTower(rock, tower);
                    height = Math.Max(height, newHeight);
                    rockCount++;
                    isFalling = false;
                }
            }

            //if (rockCount > loopSize && initLoop)
            //{
            //    if (isInLoop &&
            //        (rockCount % directions.Length) == loopRockCountStart &&
            //        (iterations % directions.Length) == loopIterationsStart)
            //    {
            //        long diffLoopHeight = height - loopHeight;
            //        long currentRockCount = rockCount - loopRockCount;
            //        long prevHeight = height;
            //        List<(long, long)> newTower = new List<(long, long)>();
            //        foreach (var position in tower)
            //        {
            //            newTower.Add((position.Item1, position.Item2));
            //        }
            //        long numLoops = (numRocks - rockCount) / currentRockCount;
            //        rockCount = rockCount + (currentRockCount * numLoops);
            //        height = height + (diffLoopHeight * numLoops);

            //        long diffHeight = height - prevHeight;
            //        tower = Utils.NewTower(tower, diffHeight);
            //    }
            //    else if (!isInLoop)
            //    {
            //        isInLoop = true;
            //        loopRockCountStart = rockCount % directions.Length;
            //        loopIterationsStart = iterations % directions.Length;
            //        loopHeight = height;
            //        loopRockCount = rockCount;
            //    }
            //}
        }

        return height + 1;
    }

    public static List<(long, long)> NewTower(List<(long, long)> tower, long newHeight)
    {
        List<(long, long)> newTower = new List<(long, long)>();
        foreach (var position in tower)
        {
            newTower.Add((position.Item1, position.Item2 + newHeight));
        }

        return newTower;
    }
}
*/