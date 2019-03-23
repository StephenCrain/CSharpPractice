using System;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = BuildTree();
            Codec codec = new Codec();
            codec.deserialize(codec.serialize(root));
            Console.WriteLine("Hello World!");
        }

        static TreeNode BuildTree() {
            var root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.right.left = new TreeNode(4);
            root.right.right = new TreeNode(5);

            return root;
        }
    }
}
