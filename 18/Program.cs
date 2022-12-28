using _18;

string[] lines = File.ReadAllLines("input.txt");

HashSet<Cube> cubes = new HashSet<Cube>();
foreach (var line in lines)
{
    var aux = line.Split(',');
    Cube cube = new Cube()
    {
        X = int.Parse(aux[0]),
        Y = int.Parse(aux[1]),
        Z = int.Parse(aux[2]),
        SidesExposed = 6
    };
    cubes.Add(cube);
}


foreach(var cube in cubes)
{
    if (cubes.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z))
    {
        cube.SidesExposed--;
    }
    if (cubes.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z))
    {
        cube.SidesExposed--;
    }
    if (cubes.Any(c => c.Y == cube.Y + 1 && c.X == cube.X && c.Z == cube.Z))
    {
        cube.SidesExposed--;
    }
    if (cubes.Any(c => c.Y == cube.Y - 1 && c.X == cube.X && c.Z == cube.Z))
    {
        cube.SidesExposed--;
    }
    if (cubes.Any(c => c.Z == cube.Z + 1 && c.Y == cube.Y && c.X == cube.X))
    {
        cube.SidesExposed--;
    }
    if (cubes.Any(c => c.Z == cube.Z - 1 && c.Y == cube.Y && c.X == cube.X))
    {
        cube.SidesExposed--;
    }
}

int result = 0;
foreach (var cube in cubes)
{
    result = result + cube.SidesExposed;
}

Console.WriteLine(result);
//3432


// part 2
HashSet<Cube> trappeds = new HashSet<Cube>();
foreach (var cube in cubes)
{
    if (cube.SidesExposed > 0)
    {
        if (!cubes.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z))
        {
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X + 1, cube.Y, cube.Z, trappeds);
            trappeds.Add(new Cube() { X = cube.X + 1, Y = cube.Y, Z = cube.Z, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }
        if (!cubes.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z))
        {
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X - 1, cube.Y, cube.Z, trappeds);
            trappeds.Add(new Cube() { X = cube.X - 1, Y = cube.Y, Z = cube.Z, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }


        if (!cubes.Any(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z))
        {
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X, cube.Y + 1, cube.Z, trappeds);
            trappeds.Add(new Cube() { X = cube.X, Y = cube.Y + 1, Z = cube.Z, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }
        if (!cubes.Any(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z))
        {
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X, cube.Y - 1, cube.Z, trappeds);
            trappeds.Add(new Cube() { X = cube.X, Y = cube.Y - 1, Z = cube.Z, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }


        if (!cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1))
        { 
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X, cube.Y, cube.Z + 1, trappeds);
            trappeds.Add(new Cube() { X = cube.X, Y = cube.Y, Z = cube.Z + 1, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }

        if (!cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1))
        {
            bool isTrapped = Utils.IsTrappedQueue(cubes, cube.X, cube.Y, cube.Z - 1, trappeds);
            trappeds.Add(new Cube() { X = cube.X, Y = cube.Y, Z = cube.Z - 1, IsTrapped = isTrapped });
            if (isTrapped)
            {
                cube.SidesExposed--;
            }
        }
    }
}

result = 0;
foreach (var cube in cubes)
{
    result = result + cube.SidesExposed;
}

Console.WriteLine(result);
//2042

class Utils
{
    public static bool IsTrapped(HashSet<Cube> cubes, int x, int y, int z, HashSet<Cube> trappeds)
    {
        bool isTrapped = false;
        bool isTrappedLeft = true;
        bool isTrappedRight = true;
        bool isTrappedUp = true;
        bool isTrappedDown = true;
        bool isTrappedFront = true;
        bool isTrappedBack = true;

        if (trappeds.Any(c => c.X == x && c.Y == y && c.Z == z))
        {
            return trappeds.First(c => c.X == x && c.Y == y && c.Z == z).IsTrapped;
        }

        if (cubes.Any(c => c.Y == y && c.Z == z))
        {
            if (x < cubes.Where(c => c.Y == y && c.Z == z).Min(c => c.X))
            {     
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
            if (x > cubes.Where(c => c.Y == y && c.Z == z).Max(c => c.X))
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
        }
        else
        {
            trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
            return false;
        }

        if (cubes.Any(c => c.X == x && c.Z == z))
        {
            if (y < cubes.Where(c => c.X == x && c.Z == z).Min(c => c.Y))
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
            if (y > cubes.Where(c => c.X == x && c.Z == z).Max(c => c.Y))
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
        }
        else
        {
            trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
            return false;
        }

        if (cubes.Any(c => c.Y == y && c.X == x))
        {
            if (z < cubes.Where(c => c.Y == y && c.X == x).Min(c => c.Z))
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
            if (z > cubes.Where(c => c.Y == y && c.X == x).Max(c => c.Z))
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }
        }
        else
        {
            trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
            return false;
        }


        if (!cubes.Any(c => c.X == x - 1 && c.Y == y && c.Z < z))
        {
            isTrappedLeft = Utils.IsTrapped(cubes, x - 1, y , z, trappeds);
        }
        else
        {
            isTrappedLeft = true;
        }
        if (isTrappedLeft && !cubes.Any(c => c.X == x + 1 && c.Y == y && c.Z < z))
        {
            isTrappedRight = Utils.IsTrapped(cubes, x + 1, y, z, trappeds);
        }
        else
        {
            isTrappedRight = true;
        }
        if (isTrappedRight && !cubes.Any(c => c.X == x && c.Y == y + 1 && c.Z < z))
        {
            isTrappedUp = Utils.IsTrapped(cubes, x, y + 1, z, trappeds);
        }
        else
        {
            isTrappedUp = true;
        }
        if (isTrappedUp && !cubes.Any(c => c.X == x && c.Y == y - 1 && c.Z < z))
        {
            isTrappedDown = Utils.IsTrapped(cubes, x, y - 1, z, trappeds);
        }
        else
        {
            isTrappedDown = true;
        }
        if (isTrappedDown && !cubes.Any(c => c.X == x && c.Y == y && c.Z < z + 1))
        {
            isTrappedBack = Utils.IsTrapped(cubes, x, y, z + 1, trappeds);
        }
        else
        {
            isTrappedBack = true;
        }
        if (isTrappedBack && !cubes.Any(c => c.X == x && c.Y == y && c.Z < z - 1))
        {
            isTrappedFront = Utils.IsTrapped(cubes, x, y, z - 1, trappeds);
        }
        else
        {
            isTrappedFront = true;
        }


        if (isTrappedLeft && isTrappedRight && isTrappedDown && isTrappedUp && isTrappedFront && isTrappedBack)
        {                                  
            isTrapped = true;
            trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = true });
        }

        return isTrapped;
    }

    public static bool IsTrappedQueue(HashSet<Cube> cubes, int x, int y, int z, HashSet<Cube> trappeds)
    {
        bool isTrapped = true;
        Queue<Cube> queue = new Queue<Cube>();
        queue.Enqueue(new Cube() { X = x, Y = y, Z = z });
        while (queue.Count > 0)
        {
            Cube cube = queue.Dequeue();
            
            bool isTrappedLeft = true;
            bool isTrappedRight = true;
            bool isTrappedUp = true;
            bool isTrappedDown = true;
            bool isTrappedFront = true;
            bool isTrappedBack = true;

            if (trappeds.Any(c => c.X == x && c.Y == y && c.Z == z))
            {
                return trappeds.First(c => c.X == x && c.Y == y && c.Z == z).IsTrapped;
            }

            if (cubes.Any(c => c.Y == y && c.Z == z))
            {
                if (x < cubes.Where(c => c.Y == y && c.Z == z).Min(c => c.X))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
                if (x > cubes.Where(c => c.Y == y && c.Z == z).Max(c => c.X))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
            }
            else
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }

            if (cubes.Any(c => c.X == x && c.Z == z))
            {
                if (y < cubes.Where(c => c.X == x && c.Z == z).Min(c => c.Y))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
                if (y > cubes.Where(c => c.X == x && c.Z == z).Max(c => c.Y))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
            }
            else
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }

            if (cubes.Any(c => c.Y == y && c.X == x))
            {
                if (z < cubes.Where(c => c.Y == y && c.X == x).Min(c => c.Z))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
                if (z > cubes.Where(c => c.Y == y && c.X == x).Max(c => c.Z))
                {
                    trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                    return false;
                }
            }
            else
            {
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = false });
                return false;
            }


            if (!cubes.Any(c => c.X == x - 1 && c.Y == y && c.Z < z))
            {                
                queue.Enqueue(new Cube() { X = x - 1, Y = y, Z = z });
            }            
            if (!cubes.Any(c => c.X == x + 1 && c.Y == y && c.Z < z))
            {
                queue.Enqueue(new Cube() { X = x + 1, Y = y, Z = z });
            }            
            if (!cubes.Any(c => c.X == x && c.Y == y + 1 && c.Z < z))
            {
                queue.Enqueue(new Cube() { X = x, Y = y + 1, Z = z });
            }            
            if (!cubes.Any(c => c.X == x && c.Y == y - 1 && c.Z < z))
            {
                queue.Enqueue(new Cube() { X = x, Y = y - 1, Z = z });
            }            
            if (!cubes.Any(c => c.X == x && c.Y == y && c.Z < z + 1))
            {
                queue.Enqueue(new Cube() { X = x, Y = y, Z = z + 1 });
            }
            if (!cubes.Any(c => c.X == x && c.Y == y && c.Z < z - 1))
            {
                queue.Enqueue(new Cube() { X = x, Y = y, Z = z - 1 });
            }


            if (isTrappedLeft && isTrappedRight && isTrappedDown && isTrappedUp && isTrappedFront && isTrappedBack)
            {
                isTrapped = true;
                trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = true });
            }            
        }

        if (isTrapped)
        {            
            trappeds.Add(new Cube() { X = x, Y = y, Z = z, IsTrapped = true });
        }
        return isTrapped;
    }
}