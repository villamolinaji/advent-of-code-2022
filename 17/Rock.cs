namespace _17
{
    public class Rock
    {
        public List<(long, long)> Positions { get; set; }

        public static Rock GetNewRock(Rock[] rocks, long rockCount, long height)
        {
            var rock = rocks[rockCount % 5];
            var rockAux = new Rock();
            rockAux.Positions = new List<(long, long)>();

            foreach (var position in rock.Positions)
            {
                rockAux.Positions.Add((position.Item1, position.Item2 + height + 4));
            }

            return rockAux;
        }

        public static Rock MoveRock(Rock rock, string directions, long iterations, List<(long, long)> tower)
        {
            char direction = directions[(int)iterations % (int)directions.Length];

            var rockAux = new Rock();
            rockAux.Positions = new List<(long, long)>();

            foreach (var position in rock.Positions)
            {
                if (direction == '<')
                    rockAux.Positions.Add((position.Item1 - 1, position.Item2));
                else
                    rockAux.Positions.Add((position.Item1 + 1, position.Item2));
            }

            foreach (var position in rockAux.Positions)
            {
                if (position.Item1 < 0 || position.Item1 > 6 || position.Item2 < 0 || tower.Contains((position.Item1, position.Item2)))
                {
                    var sameRock = new Rock();
                    sameRock.Positions = new List<(long, long)>();
                    foreach (var pos in rock.Positions)
                    {
                        sameRock.Positions.Add((pos.Item1, pos.Item2));
                    }
                    return sameRock;
                }
            }

            return rockAux;
        }

        public static Rock DownRock(Rock rock, List<(long, long)> tower, out bool isFinish)
        {
            isFinish = false;

            var sameRock = new Rock();
            sameRock.Positions = new List<(long, long)>();
            foreach (var pos in rock.Positions)
            {
                sameRock.Positions.Add((pos.Item1, pos.Item2));
            }

            var newRock = new Rock();
            newRock.Positions = new List<(long, long)>();
            foreach (var pos in rock.Positions)
            {
                newRock.Positions.Add((pos.Item1, pos.Item2 - 1));
                if ((pos.Item2 - 1) < 0)
                    isFinish = true;
            }

            if (!isFinish)
            {
                foreach (var pos in newRock.Positions)
                {
                    if (tower.Contains((pos.Item1, pos.Item2)))
                        isFinish = true;
                }
            }

            if (!isFinish)
            {
                return newRock;
            }
            else
            {
                return sameRock;
            }
        }

        public static long AddRockIntoTower(Rock rock, List<(long, long)> tower)
        {
            foreach (var pos in rock.Positions)
            {
                tower.Add((pos.Item1, pos.Item2));
            }

            return tower.Max(t => t.Item2);
        }
    }
}
