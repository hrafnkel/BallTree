namespace BallTree
{
    public class TreeNode
    {
        public bool IsRoot { get; set; }
        public TreeNode LeftNode { get; set; }
        public TreeNode RightNode { get; set; }
        public char Gate { get; set; }
        public int? BucketNumber { get; set; }
        public bool? BallPresent { get; set; }
        public int Depth { get; set; }
    }
}
