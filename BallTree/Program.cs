using System;
using System.Collections.Generic;
using System.Linq;

namespace BallTree
{
     class Program
    {
        private static int numberContainers;
        private static int numberBalls;
        private static int bucketIndex = 0;
        private static Random random = new Random();
        private static TreeNode root;
        private static char left = 'L';
        private static char right = 'R';
        private static char none = 'N';
        
        static void Main(string[] args)
        {
            int depth = Helpers.GetDepthFromArgs(args);
            if (depth < 1)
            {
                Helpers.ErrorMessage();
            }
            else
            {
                InitialiseSystem(depth);
                int prediction = PredictTheOutcome();
                int actual = GetTheOutcome(depth);
                Helpers.InformUser(prediction, actual);
            }

            Helpers.ContinueMessage();
        }        

        private static int PredictTheOutcome()
        {
            return random.Next(0, 16);
        }

        private static void InitialiseSystem(int depth)
        {
            numberContainers =(int) Math.Pow(2, depth);
            numberBalls = numberContainers - 1;
            InitialiseNodes(depth);
        }

        private static void InitialiseNodes(int depth)
        {
            root = new TreeNode
            {
                IsRoot = true,
                BucketNumber = null,
                BallPresent = null,
                Depth = 0,
                Gate = RandomGatePosition()
            };

            root.LeftNode = CreateChildNode(root, depth);
            root.RightNode = CreateChildNode(root, depth);

        }

        private static TreeNode CreateChildNode(TreeNode parent, int depth)
        {
            TreeNode thisNode;

            if (parent.Depth < depth - 1)
            {
                thisNode = new TreeNode
                {
                    IsRoot = false,
                    BucketNumber = null,
                    BallPresent = null,
                    Depth = parent.Depth + 1,
                    Gate = RandomGatePosition()
                };

                thisNode.LeftNode = CreateChildNode(thisNode, depth);
                thisNode.RightNode = CreateChildNode(thisNode, depth);
            }
            else
            {
                thisNode = new TreeNode
                {
                    IsRoot = false,
                    BucketNumber = bucketIndex,
                    BallPresent = false,
                    Depth = parent.Depth + 1,
                    Gate = none
                };

                bucketIndex++;
            }

            return thisNode;
        }

        private static char RandomGatePosition()
        {
            return random.Next(0, 2) == 0 ? left : right;
        }

        private static int GetTheOutcome(int depth)
        {
            for (int i = 0; i < numberBalls; i++)
            {
                ThrowABall(root);
            }

            return FindEmptyBucket(depth);
        }

        private static void ThrowABall(TreeNode treeNode)
        {
            if(treeNode.BucketNumber == null)
            {
                char localGate = treeNode.Gate;
                treeNode.Gate = FlipTheGate(localGate);

                if(localGate == left)
                {
                    ThrowABall(treeNode.LeftNode);
                }

                if (localGate == right)
                {
                    ThrowABall(treeNode.RightNode);
                }
            }
            else
            {
                treeNode.BallPresent = true;
            }
        }

        private static int FindEmptyBucket(int depth)
        {
            List<TreeNode> buckets = ConvertTreeToList(root);
            int? empty = buckets.Where(x => x.BallPresent == false && x.Depth == depth).Select(x => x.BucketNumber).First();
            
            return (int) empty;
        }

        private static List<TreeNode> ConvertTreeToList(TreeNode root)
        {
            var result = new List<TreeNode>();
            ConvertTreeToList(root, result);
            return result;
        }

        private static void ConvertTreeToList(TreeNode root, List<TreeNode> result)
        {
            if (root == null)
            {
                return;
            }

            result.Add(root);
            ConvertTreeToList(root.LeftNode, result);
            ConvertTreeToList(root.RightNode, result);
        }

        private static char FlipTheGate(char status)
        {
            return (status == left) ? right : left;
        }
    }
}
