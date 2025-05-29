using System.Diagnostics;

namespace Lab1._5
{
    public class TreeNode
    {

        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public int Weight { get; set; }

    }


    internal class Program
    {
        static Random rnd = new Random();
        static long total;

        public static void CreateRandomTree(TreeNode node, int level)
        {
            node.Left = new TreeNode();
            node.Right = new TreeNode();
            node.Weight = rnd.Next(100);
            total += node.Weight;
            level--;
            if (level == 0)
            {
                node.Left.Weight = rnd.Next(100);
                node.Right.Weight = rnd.Next(100);
                total += node.Left.Weight;
                total += node.Right.Weight;
                return;
            }
            CreateRandomTree(node.Left, level);
            CreateRandomTree(node.Right, level);
        }


        public static long weightTree(TreeNode root)
        {
            return
                (long)root.Weight +
                (root.Left != null ? weightTree(root.Left) : 0) +
                (root.Right != null ? weightTree(root.Right) : 0);

        }
        public static async Task<long> weightTreeAsync(TreeNode root, int tasklevel)
        {
            if (tasklevel <=0) return weightTree(root);
            Task<long> leftWeight = null;
            Task<long> rightWeight = null;
            long leftWeightValue = 0;
            long rightWeightValue = 0;
            tasklevel--;
                if (root.Left != null) leftWeight = Task.Run(() =>weightTreeAsync(root.Left, tasklevel));
                if (root.Right != null) rightWeight = Task.Run(() => weightTreeAsync(root.Right, tasklevel));
                leftWeightValue = (leftWeight != null ? await leftWeight : 0);
                rightWeightValue = (rightWeight != null ? await rightWeight : 0);
            return(long)root.Weight + leftWeightValue + rightWeightValue;
        }

        static void Main(string[] args)
        {
            int treeLevel = 25; // 2^(n+1)-1

            Console.WriteLine($"Starting tree creation with depth {treeLevel}...");
            TreeNode root = new TreeNode();
            CreateRandomTree(root, treeLevel);
            Console.WriteLine($"Tree created with total weight: {total}");


            Stopwatch t1 = new Stopwatch();
            t1.Start();
            long r1 = weightTree(root);
            t1.Stop();
            Console.WriteLine($"Single weight: {r1} Time {t1.ElapsedMilliseconds}");

            Stopwatch t2 = new Stopwatch();
            t2.Start();
            long r2 = weightTreeAsync(root, 1).Result;
            t2.Stop();
            Console.WriteLine($"Multi weight (1 recursive): {r2} Time {t2.ElapsedMilliseconds}");

            Stopwatch t3 = new Stopwatch();
            t3.Start();
            long r3 = weightTreeAsync(root, 2).Result;
            t3.Stop();
            Console.WriteLine($"Multi weight (2 recursive): {r3} Time {t3.ElapsedMilliseconds}");

            Stopwatch t4 = new Stopwatch();
            t4.Start();
            long r4 = weightTreeAsync(root, 3).Result;
            t4.Stop();
            Console.WriteLine($"Multi weight (3 recursive): {r4} Time {t4.ElapsedMilliseconds}");

            Stopwatch t5 = new Stopwatch();
            t5.Start();
            long r5 = weightTreeAsync(root, 4).Result;
            t5.Stop();
            Console.WriteLine($"Multi weight (4 recursive): {r5} Time {t5.ElapsedMilliseconds}");

        }
    }
}
