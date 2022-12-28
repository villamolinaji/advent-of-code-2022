namespace _08
{
    public class Tree
    {
        public int Height { get; set; }
        public bool IsVisibleN { get; set; }
        public bool IsVisibleS { get; set; }
        public bool IsVisibleO { get; set; }
        public bool IsVisibleE { get; set; }

        public int VisibleN { get; set; }
        public int VisibleS { get; set; }
        public int VisibleO { get; set; }
        public int VisibleE { get; set; }

        public int ScenicValue { get; set; }

        public static bool CalculateIsVisible0(Tree[,] trees, int i, int j, int height)
        {
            if (j > 0)
            {                
                if (trees[i, j - 1].Height < height)
                {
                    if (j - 1 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return Tree.CalculateIsVisible0(trees, i, j - 1, height);
                    }
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return true;
            }
        }

        public static bool CalculateIsVisibleN(Tree[,] trees, int i, int j, int height)
        {            
            if (i > 0)
            {                
                if (trees[i - 1, j].Height < height)
                {
                    if (i - 1 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return Tree.CalculateIsVisibleN(trees, i - 1, j, height);
                    }
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return true;
            }
        }

        public static bool CalculateIsVisibleE(Tree[,] trees, int i, int j, int height, int length)
        {            
            if (j < length - 1)
            {                
                if (trees[i, j + 1].Height < height)
                {
                    if (j + 1 == length - 1)
                    {
                        return true;
                    }
                    else
                    {
                        return Tree.CalculateIsVisibleE(trees, i, j + 1, height, length);
                    }
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return true;
            }
        }

        public static bool CalculateIsVisibleS(Tree[,] trees, int i, int j, int height, int length)
        {            
            if (i < length - 1)
            {                
                if (trees[i + 1, j].Height < height)
                {
                    if (i + 1 == length - 1)
                    {
                        return true;
                    }
                    else
                    {
                        return Tree.CalculateIsVisibleS(trees, i + 1, j, height, length);
                    }
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return true;
            }
        }



        public static void CalculateVisible0(Tree[,] trees, Tree tree, int i, int j, int height)
        {
            if (j > 0)
            {
                tree.VisibleO = tree.VisibleO + 1;
                
                if (trees[i, j - 1].Height < height)
                {                        
                    Tree.CalculateVisible0(trees, tree, i, j - 1, height);
                }                              
            }            
        }

        public static void CalculateVisibleN(Tree[,] trees, Tree tree, int i, int j, int height)
        {
            if (i > 0)
            {
                tree.VisibleN = tree.VisibleN + 1;
                
                if (trees[i - 1, j].Height < height)
                {
                    Tree.CalculateVisibleN(trees, tree, i - 1, j, height);
                }                
            }
        }

        public static void CalculateVisibleE(Tree[,] trees, Tree tree, int i, int j, int height, int length)
        {
            if (j < length - 1)
            {
                tree.VisibleE = tree.VisibleE + 1;
                
                if (trees[i, j + 1].Height < height)
                {
                    Tree.CalculateVisibleE(trees, tree, i, j + 1, height, length);
                }                                                 
            }            
        }

        public static void CalculateVisibleS(Tree[,] trees, Tree tree, int i, int j, int height, int length)
        {
            if (i < length - 1)
            {
                tree.VisibleS = tree.VisibleS + 1;
                
                if (trees[i + 1, j].Height < height)
                {
                    Tree.CalculateVisibleS(trees, tree, i + 1, j, height, length);
                }                                                  
            }            
        }
    }
}
