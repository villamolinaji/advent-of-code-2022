namespace _19
{
    public class Blueprint
    {
        public int Id { get; set; }
        public int OreCost { get; set; }
        public int ClayOreCost { get; set; }
        public int ObsidianOreCost { get; set; }
        public int ObsidianClayCost { get; set; }
        public int GeodeOreCost { get; set; }
        public int GeodeObsidianCost { get; set; }
        public int TotalGeodes { get; set; }

        public static int CollectGeodes(Blueprint blueprint)
        {            
            int maxTotalGeode = 0;            
            int maxMinutes = 24;
            
            HashSet<(int, int, int, int, int, int, int, int, int)> processed = new HashSet<(int, int, int, int, int, int, int, int, int)>();
            Queue<BlueprintAction> queue = new Queue<BlueprintAction>();
            queue.Enqueue(new BlueprintAction() { OreRobots = 1, ClayRobots = 0, ObsidianRobots = 0, GeodeRobots = 0, TotalOre = 0, TotalClay = 0, TotalObsidian = 0, TotalGeode = 0, Minute = maxMinutes });

            var geodeMinutes  = new List<GeodeMinute>();

            while(queue.Count > 0)
            {
                var blueprintAction = queue.Dequeue();

                if (blueprintAction.TotalGeode > maxTotalGeode)
                {
                    maxTotalGeode = blueprintAction.TotalGeode;
                }

                if (blueprintAction.Minute == 0)
                {
                    continue;
                }

                /*int core = Math.Max(Math.Max(Math.Max(blueprint.OreCost, blueprint.ClayOreCost), blueprint.ObsidianClayCost), blueprint.GeodeOreCost);
                if (blueprintAction.OreRobots >= core)
                    blueprintAction.OreRobots = core;
                if (blueprintAction.ClayRobots >= blueprint.ObsidianClayCost)
                    blueprintAction.ClayRobots = blueprint.ObsidianClayCost;
                if (blueprintAction.ObsidianRobots >= blueprint.GeodeObsidianCost)
                    blueprintAction.ObsidianRobots = blueprint.GeodeObsidianCost;
                if (blueprintAction.TotalOre >= (blueprintAction.Minute * core) - blueprintAction.OreRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalOre = (blueprintAction.Minute * core) - blueprintAction.OreRobots * (blueprintAction.Minute - 1);
                if (blueprintAction.TotalClay >= (blueprintAction.Minute * blueprint.ObsidianClayCost) - blueprintAction.ClayRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalClay = (blueprintAction.Minute * blueprint.ObsidianClayCost) - blueprintAction.ClayRobots * (blueprintAction.Minute - 1);
                if (blueprintAction.TotalObsidian >= (blueprintAction.Minute * blueprint.GeodeObsidianCost) - blueprintAction.ObsidianRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalObsidian = (blueprintAction.Minute * blueprint.GeodeObsidianCost) - blueprintAction.ObsidianRobots * (blueprintAction.Minute - 1);*/

                if (processed.Contains((blueprintAction.OreRobots, blueprintAction.ClayRobots, blueprintAction.ObsidianRobots, blueprintAction.GeodeRobots, blueprintAction.TotalOre, blueprintAction.TotalClay, blueprintAction.TotalObsidian, blueprintAction.TotalGeode, blueprintAction.Minute)))
                {
                    continue;
                }
                processed.Add(
                (
                    blueprintAction.OreRobots,
                    blueprintAction.ClayRobots,
                    blueprintAction.ObsidianRobots,
                    blueprintAction.GeodeRobots,
                    blueprintAction.TotalOre,
                    blueprintAction.TotalClay,
                    blueprintAction.TotalObsidian,
                    blueprintAction.TotalGeode,
                    blueprintAction.Minute
                ));                


                if (!geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode > blueprintAction.TotalGeode) /*&&
                    (blueprintAction.TotalGeode == 0 && !geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode == blueprintAction.TotalGeode && g.GeodeRobots > blueprintAction.GeodeRobots))*/)
                {
                    //if (!geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode == blueprintAction.TotalGeode && g.GeodeRobots == blueprintAction.GeodeRobots && g.TotalObsidian == blueprintAction.TotalObsidian && g.ObsidianRobots == blueprintAction.ObsidianRobots))
                        //geodeMinutes.Add(new GeodeMinute() { Minute = blueprintAction.Minute, TotalGeode = blueprintAction.TotalGeode, GeodeRobots = blueprintAction.GeodeRobots, TotalObsidian = blueprintAction.TotalObsidian, ObsidianRobots = blueprintAction.ObsidianRobots });
                    if (!geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode == blueprintAction.TotalGeode))
                        geodeMinutes.Add(new GeodeMinute() { Minute = blueprintAction.Minute, TotalGeode = blueprintAction.TotalGeode });

                    // build robot                                                            
                    if (blueprintAction.TotalOre >= blueprint.GeodeOreCost && blueprintAction.TotalObsidian >= blueprint.GeodeObsidianCost)
                    {
                        var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                        BlueprintAction.CollectItems(cloneBlueprintAction);
                        cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.GeodeOreCost;
                        cloneBlueprintAction.TotalObsidian = cloneBlueprintAction.TotalObsidian - blueprint.GeodeObsidianCost;
                        cloneBlueprintAction.GeodeRobots = cloneBlueprintAction.GeodeRobots + 1;
                        BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                    }
                    else
                    {                                                        
                        var cloneBlueprintAction2 = BlueprintAction.Clone(blueprintAction);
                        BlueprintAction.CollectItems(cloneBlueprintAction2);
                        BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction2);

                        if (blueprintAction.TotalOre >= blueprint.ObsidianOreCost && blueprintAction.TotalClay >= blueprint.ObsidianClayCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.ObsidianOreCost;
                            cloneBlueprintAction.TotalClay = cloneBlueprintAction.TotalClay - blueprint.ObsidianClayCost;
                            cloneBlueprintAction.ObsidianRobots = cloneBlueprintAction.ObsidianRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }
                        if (blueprintAction.TotalOre >= blueprint.ClayOreCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.ClayOreCost;
                            cloneBlueprintAction.ClayRobots = cloneBlueprintAction.ClayRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }
                        if (blueprintAction.TotalOre >= blueprint.OreCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.OreCost;
                            cloneBlueprintAction.OreRobots = cloneBlueprintAction.OreRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }                            
                    }

                    /*var cloneBlueprintAction2 = BlueprintAction.Clone(blueprintAction);
                    BlueprintAction.CollectItems(cloneBlueprintAction2);
                    BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction2);*/
                }                
            }

            return maxTotalGeode;
        }

        public static int CollectGeodes2(Blueprint blueprint)
        {
            int maxTotalGeode = 0;
            int maxMinutes = 32;

            /*if (blueprint.Id <= 3)
                maxMinutes = 32;*/

            HashSet<(int, int, int, int, int, int, int, int, int)> processed = new HashSet<(int, int, int, int, int, int, int, int, int)>();
            Queue<BlueprintAction> queue = new Queue<BlueprintAction>();
            queue.Enqueue(new BlueprintAction() { OreRobots = 1, ClayRobots = 0, ObsidianRobots = 0, GeodeRobots = 0, TotalOre = 0, TotalClay = 0, TotalObsidian = 0, TotalGeode = 0, Minute = maxMinutes });

            var geodeMinutes = new List<GeodeMinute>();

            while (queue.Count > 0)
            {
                var blueprintAction = queue.Dequeue();

                if (blueprintAction.TotalGeode > maxTotalGeode)
                {
                    maxTotalGeode = blueprintAction.TotalGeode;
                }

                if (blueprintAction.Minute == 0)
                {
                    continue;
                }

                int core = Math.Max(Math.Max(Math.Max(blueprint.OreCost, blueprint.ClayOreCost), blueprint.ObsidianClayCost), blueprint.GeodeOreCost);
                if (blueprintAction.OreRobots >= core)
                    blueprintAction.OreRobots = core;
                if (blueprintAction.ClayRobots >= blueprint.ObsidianClayCost)
                    blueprintAction.ClayRobots = blueprint.ObsidianClayCost;
                if (blueprintAction.ObsidianRobots >= blueprint.GeodeObsidianCost)
                    blueprintAction.ObsidianRobots = blueprint.GeodeObsidianCost;
                if (blueprintAction.TotalOre >= (blueprintAction.Minute * core) - blueprintAction.OreRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalOre = (blueprintAction.Minute * core) - blueprintAction.OreRobots * (blueprintAction.Minute - 1);
                if (blueprintAction.TotalClay >= (blueprintAction.Minute * blueprint.ObsidianClayCost) - blueprintAction.ClayRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalClay = (blueprintAction.Minute * blueprint.ObsidianClayCost) - blueprintAction.ClayRobots * (blueprintAction.Minute - 1);
                if (blueprintAction.TotalObsidian >= (blueprintAction.Minute * blueprint.GeodeObsidianCost) - blueprintAction.ObsidianRobots * (blueprintAction.Minute - 1))
                    blueprintAction.TotalObsidian = (blueprintAction.Minute * blueprint.GeodeObsidianCost) - blueprintAction.ObsidianRobots * (blueprintAction.Minute - 1);


                if (processed.Contains((blueprintAction.OreRobots, blueprintAction.ClayRobots, blueprintAction.ObsidianRobots, blueprintAction.GeodeRobots, blueprintAction.TotalOre, blueprintAction.TotalClay, blueprintAction.TotalObsidian, blueprintAction.TotalGeode, blueprintAction.Minute)))
                {
                    continue;
                }
                processed.Add(
                (
                    blueprintAction.OreRobots,
                    blueprintAction.ClayRobots,
                    blueprintAction.ObsidianRobots,
                    blueprintAction.GeodeRobots,
                    blueprintAction.TotalOre,
                    blueprintAction.TotalClay,
                    blueprintAction.TotalObsidian,
                    blueprintAction.TotalGeode,
                    blueprintAction.Minute
                ));

                //if (!geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode > blueprintAction.TotalGeode))
                //{                    
                    if (!geodeMinutes.Any(g => g.Minute == blueprintAction.Minute && g.TotalGeode == blueprintAction.TotalGeode))
                        geodeMinutes.Add(new GeodeMinute() { Minute = blueprintAction.Minute, TotalGeode = blueprintAction.TotalGeode });

                    // build robot                                                            
                    if (blueprintAction.TotalOre >= blueprint.GeodeOreCost && blueprintAction.TotalObsidian >= blueprint.GeodeObsidianCost)
                    {
                        var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                        BlueprintAction.CollectItems(cloneBlueprintAction);
                        cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.GeodeOreCost;
                        cloneBlueprintAction.TotalObsidian = cloneBlueprintAction.TotalObsidian - blueprint.GeodeObsidianCost;
                        cloneBlueprintAction.GeodeRobots = cloneBlueprintAction.GeodeRobots + 1;
                        BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                    }
                    else
                    {
                        var cloneBlueprintAction2 = BlueprintAction.Clone(blueprintAction);
                        BlueprintAction.CollectItems(cloneBlueprintAction2);
                        BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction2);

                        if (blueprintAction.TotalOre >= blueprint.ObsidianOreCost && blueprintAction.TotalClay >= blueprint.ObsidianClayCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.ObsidianOreCost;
                            cloneBlueprintAction.TotalClay = cloneBlueprintAction.TotalClay - blueprint.ObsidianClayCost;
                            cloneBlueprintAction.ObsidianRobots = cloneBlueprintAction.ObsidianRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }
                        if (blueprintAction.TotalOre >= blueprint.ClayOreCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.ClayOreCost;
                            cloneBlueprintAction.ClayRobots = cloneBlueprintAction.ClayRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }
                        if (blueprintAction.TotalOre >= blueprint.OreCost)
                        {
                            var cloneBlueprintAction = BlueprintAction.Clone(blueprintAction);
                            BlueprintAction.CollectItems(cloneBlueprintAction);
                            cloneBlueprintAction.TotalOre = cloneBlueprintAction.TotalOre - blueprint.OreCost;
                            cloneBlueprintAction.OreRobots = cloneBlueprintAction.OreRobots + 1;
                            BlueprintAction.EnqueBlueprintAction(queue, cloneBlueprintAction);
                        }
                    }                    
                //}
            }

            return maxTotalGeode;
        }
    }    

    public class BlueprintAction
    {
        public int OreRobots { get; set; }
        public int ClayRobots { get; set; }
        public int ObsidianRobots { get; set; }
        public int GeodeRobots { get; set; }
        public int TotalOre { get; set; }
        public int TotalClay { get; set; }
        public int TotalObsidian { get; set; }
        public int TotalGeode { get; set; }
        public int Minute { get; set; }
        /*public int FutureObsidian
        {
            get
            {
                return TotalObsidian + ((25 - Minute) * ObsidianRobots);
            }
        }
        public int FutureGeode
        {
            get
            {
                return TotalGeode + ((25 - Minute) * GeodeRobots);
            }
        }*/

        public static BlueprintAction Clone(BlueprintAction blueprintAction)
        {
            BlueprintAction cloneBlueprintAction = new BlueprintAction();
            cloneBlueprintAction.OreRobots = blueprintAction.OreRobots;
            cloneBlueprintAction.ClayRobots = blueprintAction.ClayRobots;
            cloneBlueprintAction.ObsidianRobots = blueprintAction.ObsidianRobots;
            cloneBlueprintAction.GeodeRobots = blueprintAction.GeodeRobots;
            cloneBlueprintAction.TotalOre = blueprintAction.TotalOre;
            cloneBlueprintAction.TotalClay = blueprintAction.TotalClay;
            cloneBlueprintAction.TotalObsidian = blueprintAction.TotalObsidian;
            cloneBlueprintAction.TotalGeode = blueprintAction.TotalGeode;
            cloneBlueprintAction.Minute = blueprintAction.Minute;

            return cloneBlueprintAction;
        }

        public static void CollectItems(BlueprintAction blueprintAction)
        {
            blueprintAction.TotalOre = blueprintAction.TotalOre + blueprintAction.OreRobots;
            blueprintAction.TotalClay = blueprintAction.TotalClay + blueprintAction.ClayRobots;
            blueprintAction.TotalObsidian = blueprintAction.TotalObsidian + blueprintAction.ObsidianRobots;
            blueprintAction.TotalGeode = blueprintAction.TotalGeode + blueprintAction.GeodeRobots;
        }

        public static void EnqueBlueprintAction(Queue<BlueprintAction> queue, BlueprintAction blueprintAction)
        {
            
            queue.Enqueue(new BlueprintAction()
            {
                OreRobots = blueprintAction.OreRobots,
                ClayRobots = blueprintAction.ClayRobots,
                ObsidianRobots = blueprintAction.ObsidianRobots,
                GeodeRobots = blueprintAction.GeodeRobots,
                TotalOre = blueprintAction.TotalOre,
                TotalClay = blueprintAction.TotalClay,
                TotalObsidian = blueprintAction.TotalObsidian,
                TotalGeode = blueprintAction.TotalGeode,
                Minute = blueprintAction.Minute - 1
            });                            
        }
    }  
    
    public class GeodeMinute
    {
        public int Minute { get; set; }
        public int TotalGeode { get; set; }
        public int GeodeRobots { get; set; }
        public int TotalObsidian { get; set; }

        public int ObsidianRobots { get; set; }

        /*public int FutureObsidian
        {
            get
            {
                return TotalObsidian + ((25 - Minute) * ObsidianRobots);
            }
        }

        public int FutureGeode
        {
            get
            {
                return TotalGeode + ((25 - Minute) * GeodeRobots);
            }
        }*/
    }
}
