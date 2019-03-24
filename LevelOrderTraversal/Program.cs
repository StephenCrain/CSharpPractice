using System;
using System.Linq;
using System.Collections.Generic;


namespace LevelOrderTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new TreeNode(3);
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.right.left = new TreeNode(15);
            root.right.right = new TreeNode(7);
            var thing = new Solution();
            var answer = thing.LevelOrder(root);
            foreach(var line in answer) {
                Console.WriteLine("[{0}]", String.Join(",", line));
            }
        }
    }


 
    public class Solution {
        public IList<IList<int>> LevelOrder(TreeNode root) {
            List<IList<int>> levels = new List<IList<int>>();
            
            if (root != null) {
                levels.Add(new List<int>());
                levels[0].Add(root.val);
                visit(root, levels, 1);
            }
            
            return levels;
        }
    
        private void visit(TreeNode node, List<IList<int>> levels, int curLevel) {
            if (curLevel == levels.Count && (node?.left != null || node?.right != null)) {
                levels.Add(new List<int>());  
            }

            if (node?.left != null) {
                levels[curLevel].Add(node.left.val);
                visit(node.left, levels, curLevel + 1);
            }

            if (node?.right != null) {
                levels[curLevel].Add(node.right.val);
                visit(node.right, levels, curLevel + 1);
            }
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
}
