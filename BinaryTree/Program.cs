using System;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = BuildTree2();
            Codec codec = new Codec();
            root = codec.deserialize(codec.serialize(root));
            Console.WriteLine(root.val);
        }

        static TreeNode BuildTree() {
            var root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.right.left = new TreeNode(4);
            root.right.right = new TreeNode(5);

            return root;
        }

        static TreeNode BuildTree2() {
            var root = new TreeNode(5);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.right.left = new TreeNode(2);
            root.right.left.left = new TreeNode(3);
            root.right.left.right = new TreeNode(1);
            root.right.right = new TreeNode(4);

            return root;
        }
    }
}
