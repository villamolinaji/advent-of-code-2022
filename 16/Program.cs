using _16;
using System.Linq;
using System.Runtime.CompilerServices;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;

HashSet<Valve> valves = new HashSet<Valve>();
foreach (var line in lines)
{
    Valve valve= new Valve();
    valve.Id = line.Substring(6, 2);
    valve.Rate = int.Parse(line.Substring(line.IndexOf('=') + 1, line.IndexOf(";") - line.IndexOf("=") - 1));
    valve.Destinations = new List<string>();

    string aux = string.Empty;
    string replacement = string.Empty;
    if (line.Contains("valves"))
        replacement = "valves";
    else
        replacement = "valve";

    aux = line.Substring(line.IndexOf(replacement), line.Length - line.IndexOf(replacement));
    aux = aux.Replace(replacement + " ", "");
    foreach(var dest in aux.Split(','))
    { 
        valve.Destinations.Add(dest.Trim());
    }


    valves.Add(valve);
}

int minutes = 1;
List<Pressure> pressures = new List<Pressure>();
List<Step> steps = new List<Step>();
var firstValve = valves.First();

var rates = new Dictionary<string, int>();
var destinations = new Dictionary<string, List<string>>();
foreach (var valve in valves)
{
    rates.Add(valve.Id, valve.Rate);
    destinations.Add(valve.Id, valve.Destinations);
}

//result = Utils.DoMovement(valves, minutes, firstValve, pressures, result, steps, firstValve.Id, 0);

var queue = new Queue<QueueItem>();
var visitedValves = new List<VisitedValve>();
queue.Enqueue(new QueueItem() { CurrentValve = "AA", SecondValve = "AA", Minute = 1, Score = 0, Opened = new List<string>() });
//result = Utils.FindSolution(queue, rates, destinations);
result = Utils.FindSolutionPart2(queue, rates, destinations);

Console.WriteLine(result);


class Utils
{
    public static int FindSolution(Queue<QueueItem> queue, Dictionary<string, int> rates, Dictionary<string, List<string>> destinations)
    {
        int totalMinutes = 30;
        int bestScore = 0;
        var visitedValves = new List<VisitedValve>();
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();            
            int currentRate = rates[current.CurrentValve];
            

            if (visitedValves.Any(v => v.Valve == current.CurrentValve /*&& v.Minute == current.Minute*/ && v.Score > current.Score))
            {
                continue;
            }

            visitedValves.Add(new VisitedValve() { Minute = current.Minute, Valve = current.CurrentValve, Score = current.Score });

            if (current.Minute >= totalMinutes)
            {
                if (current.Score > bestScore)
                    bestScore = current.Score;

                continue;
            }

                        
            if (currentRate > 0 && !current.Opened.Contains(current.CurrentValve))
            {
                var auxOpened = new List<string>();
                foreach (var openVale in current.Opened)
                {
                    auxOpened.Add(openVale);
                }
                auxOpened.Add(current.CurrentValve);

                int nextScore = current.Score + auxOpened.Sum(v => rates[v]);                
                queue.Enqueue(new QueueItem() { CurrentValve = current.CurrentValve, Minute = current.Minute + 1, Score = nextScore, Opened = auxOpened });                
            }

            
            foreach (var dest in destinations[current.CurrentValve])
            {
                var auxOpened = new List<string>();
                foreach (var openVale in current.Opened)
                {
                    auxOpened.Add(openVale);
                }
                int nextScore = current.Score + auxOpened.Sum(v => rates[v]);
                queue.Enqueue(new QueueItem() { CurrentValve = dest, Minute = current.Minute + 1, Score = nextScore, Opened = auxOpened });
            }
        }

        return bestScore;
    }


    public static int FindSolutionPart2(Queue<QueueItem> queue, Dictionary<string, int> rates, Dictionary<string, List<string>> destinations)
    {
        int totalMinutes = 26;
        int bestScore = 0;
        var visitedValves = new List<VisitedValve>();

        int sumRates = 0;
        foreach (var rate in rates)
        {
            sumRates += rate.Value;
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int currentRate = rates[current.CurrentValve];
            int currentRate2 = rates[current.SecondValve];


            if (visitedValves.Any(v => v.Valve == current.CurrentValve && v.Score > current.Score))
            {
                continue;
            }

            visitedValves.Add(new VisitedValve() { Minute = current.Minute, Valve = current.CurrentValve, Score = current.Score });

            if (current.Minute >= totalMinutes)
            {
                if (current.Score > bestScore)
                    bestScore = current.Score;

                continue;
            }


            var currentFlow = current.Opened.Sum(v => rates[v]);
            if (currentFlow >= sumRates)
            {
                int newScore = current.Score + currentFlow;
                int newMinute = current.Minute;
                for (int i = current.Minute; i < totalMinutes - 1; i++)
                {
                    newMinute++;
                    newScore = newScore + currentFlow;
                }

                var auxOpened2 = new List<string>();
                foreach (var openVale in current.Opened)
                {
                    auxOpened2.Add(openVale);
                }

                queue.Enqueue(new QueueItem() { CurrentValve = current.CurrentValve, SecondValve = current.SecondValve, Minute = newMinute + 1, Score = newScore, Opened = auxOpened2 });

                continue;

            }

            if (currentRate > 0 && !current.Opened.Contains(current.CurrentValve))
            {
                var auxOpened = new List<string>();
                foreach (var openVale in current.Opened)
                {
                    auxOpened.Add(openVale);
                }
                auxOpened.Add(current.CurrentValve);

                if (currentRate2 > 0 && !auxOpened.Contains(current.SecondValve))
                {
                    var auxOpened2 = new List<string>();
                    foreach (var openVale in auxOpened)
                    {
                        auxOpened2.Add(openVale);
                    }                    
                    auxOpened2.Add(current.SecondValve);

                    int nextScore2 = current.Score + auxOpened2.Sum(v => rates[v]);
                    queue.Enqueue(new QueueItem() { CurrentValve = current.CurrentValve, SecondValve = current.SecondValve, Minute = current.Minute + 1, Score = nextScore2, Opened = auxOpened2 });

                }
                
                foreach (var dest in destinations[current.SecondValve])
                {
                    var auxOpened2 = new List<string>();
                    foreach (var openVale in auxOpened)
                    {
                        auxOpened2.Add(openVale);
                    }
                    int nextScore2 = current.Score + auxOpened2.Sum(v => rates[v]);
                    queue.Enqueue(new QueueItem() { CurrentValve = current.CurrentValve, SecondValve = dest, Minute = current.Minute + 1, Score = nextScore2, Opened = auxOpened2 });
                }
            }


            foreach (var dest in destinations[current.CurrentValve])
            {
                if (currentRate2 > 0 && !current.Opened.Contains(current.SecondValve))
                {
                    var auxOpened2 = new List<string>();
                    foreach (var openVale in current.Opened)
                    {
                        auxOpened2.Add(openVale);
                    }
                    auxOpened2.Add(current.SecondValve);

                    int nextScore2 = current.Score + auxOpened2.Sum(v => rates[v]);
                    queue.Enqueue(new QueueItem() { CurrentValve = dest, SecondValve = current.SecondValve, Minute = current.Minute + 1, Score = nextScore2, Opened = auxOpened2 });

                }
                
                foreach (var dest2 in destinations[current.SecondValve])
                {
                    var auxOpened2 = new List<string>();
                    foreach (var openVale in current.Opened)
                    {
                        auxOpened2.Add(openVale);
                    }
                    int nextScore2 = current.Score + auxOpened2.Sum(v => rates[v]);
                    queue.Enqueue(new QueueItem() { CurrentValve = dest, SecondValve = dest2, Minute = current.Minute + 1, Score = nextScore2, Opened = auxOpened2 });
                }
            }
        }

        return bestScore;
    }


    public static int DoMovement(List<Valve> valves, int minutes, Valve currentValve, List<Pressure> pressures, int currentTotal, List<Step> steps, string prevValve, int currentPressure)
    {
        int newTotal = currentTotal + currentPressure;
        List<int> totals = new List<int>(); 
        
        int totalMinutes = 30;
        if (minutes == totalMinutes)
        {
            return newTotal;
        }

        var auxSteps = new List<Step>();
        foreach (var stepAux in steps)
        {
            auxSteps.Add(stepAux);
        }
        if (prevValve != currentValve.Id)
            auxSteps.Add(new Step() { FromValve = prevValve, ToValve = currentValve.Id, Minute = minutes, Total = newTotal });

        if (auxSteps.Any(s => s.ToValve == currentValve.Id && s.Total > newTotal)) 
        {
            return newTotal;
        }

        if (currentValve.Rate > 0 && !pressures.Any(p => p.ValveId == currentValve.Id))
        {
            //moving first
            foreach (var dest in currentValve.Destinations)
            {                
                var destValve = valves.First(v => v.Id == dest);
                if (!auxSteps.Any(s => s.FromValve == currentValve.Id && 
                    s.ToValve == destValve.Id 
                    && auxSteps.Any(s2 => s2.FromValve == prevValve && s2.ToValve == currentValve.Id && s2.Minute == s.Minute - 1)))
                {
                    int auxTotal = newTotal;
                    auxTotal = Utils.DoMovement(valves, minutes + 1, destValve, pressures, newTotal, auxSteps, currentValve.Id, currentPressure);
                    totals.Add(auxTotal);
                }
            }

            //opening first
            int newMinutes = minutes + 1;            
            int leftMinutes = totalMinutes - newMinutes;
            //if (leftMinutes > 0)
            //{
                Pressure pressure = new Pressure() { ValveId = currentValve.Id, Rate = currentValve.Rate, Minute = leftMinutes, Total = currentValve.Rate * leftMinutes };
                var auxPressures = new List<Pressure>();
                foreach (var pressureAux in pressures)
                {
                    auxPressures.Add(pressureAux);
                }
                auxPressures.Add(pressure);
                int newPressure = currentPressure + currentValve.Rate;

                return Utils.DoMovement(valves, minutes + 1, currentValve, auxPressures, newTotal, auxSteps, prevValve, newPressure);

                //var newTotal = currentTotal + pressure.Total;

                /*if (newMinutes == totalMinutes || newMinutes + 1 == totalMinutes)
                {
                    totals.Add(newTotal);
                }
                else
                {*/
                    /*foreach (var dest in currentValve.Destinations)
                    {
                        int auxTotal = newTotal;
                        var destValve = valves.First(v => v.Id == dest);
                        if (!auxSteps.Any(s => s.FromValve == currentValve.Id && s.ToValve == destValve.Id))
                        {                            
                            auxTotal = Utils.DoMovement(valves, minutes + 1, destValve, auxPressures, newTotal, auxSteps, currentValve.Id, newPressure);
                            totals.Add(auxTotal);
                        }
                    }*/
                //}
            //}
        }
        else
        {
            //if (minutes + 1 < totalMinutes)
            //{
                foreach (var dest in currentValve.Destinations)
                {                    
                    var destValve = valves.First(v => v.Id == dest);
                    if (!auxSteps.Any(s => s.FromValve == currentValve.Id &&
                        s.ToValve == destValve.Id
                        && auxSteps.Any(s2 => s2.FromValve == prevValve && s2.ToValve == currentValve.Id && s2.Minute == s.Minute - 1)))
                    {
                        int auxTotal = newTotal;
                        auxTotal = Utils.DoMovement(valves, minutes + 1, destValve, pressures, newTotal, auxSteps, currentValve.Id, currentPressure);
                        totals.Add(auxTotal);
                    }
                }
            //}
        }

        if (totals.Count == 0)
            return Utils.DoMovement(valves, minutes + 1, currentValve, pressures, newTotal, auxSteps, prevValve, currentPressure);
        else
            return totals.Max();
    }
}