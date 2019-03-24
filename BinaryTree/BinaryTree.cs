using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
 }
 

public class Codec {
    private const string nullString = "X";
    
    // Encodes a tree to a single string.
     public string serialize(TreeNode root) {
        if (root == null) {
            return nullString;
        }
        
        var levels = new List<List<TreeNode>>();
        levels.Add(new List<TreeNode>(1));
        levels[0].Add(root);
        
        levelOrder(root, levels, 1);
        
        var sb = new StringBuilder();
        
        // last level will be all nulls;
        levels.RemoveAt(levels.Count - 1);

        // remove trailing nulls from real last level.
        var lastLevel = levels[levels.Count - 1];
        for (int i = lastLevel.Count - 1; i >= 0; i--) {
            if (lastLevel[i] == null) {
                lastLevel.RemoveAt(i);
            } else {
                break;
            }
        }

        levels.ForEach(level => level.ForEach(node => AppendValue(node, sb)));
        
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    
    private void levelOrder(TreeNode node, List<List<TreeNode>> levels, int curDepth) {
        if (levels.Count == curDepth) {
            levels.Add(new List<TreeNode>());
        }
        
        levels[curDepth].Add(node.left);
        levels[curDepth].Add(node.right);
        
        if (node.left != null) {
            levelOrder(node.left, levels, curDepth + 1);
        }
        
        if (node.right != null) {
            levelOrder(node.right, levels, curDepth + 1);
        }
    }
    
    private void AppendValue(TreeNode node, StringBuilder sb) {
        string val = node != null ? node.val.ToString() : nullString;
        sb.Append($"{val},");
    }
    
    private int FindDepth(TreeNode node, int currentDepth) {
        if (node == null) {
            return currentDepth;
        }
        
        var leftDepth = FindDepth(node.left, currentDepth + 1);
        var rightDepth = FindDepth(node.right, currentDepth + 1);
        
        return leftDepth > rightDepth ? leftDepth : rightDepth;
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data) {
        int?[] nodes = data.Split(",").Select(node => {
            try {
                return new Nullable<int>(Int32.Parse(node));
            } catch {
                return null;
            }
        }).ToArray();
        
        if (!nodes[0].HasValue) {
            return null;
        }
        
        var root = new TreeNode(nodes[0].Value);
        treeify(root, nodes, 0);
        return root;
    }
    
    private void treeify(TreeNode node, int?[] nodes, int index) {
        int leftIndex = (index * 2) + 1;
        int rightIndex = (index * 2) + 2;

        if (nodes[leftIndex].HasValue) {
            node.left = new TreeNode(nodes[leftIndex].Value);
            treeify(node.left, nodes, leftIndex);
        }
        
        if (nodes[rightIndex].HasValue) {
            node.right = new TreeNode(nodes[rightIndex].Value);
            treeify(node.right, nodes, rightIndex);
        }
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));

/*

What about a highly unbalanced tree.
Linked list worst case for ex?

Binary tree to array/string

root  at 0
left  at 1. left children at 3 (left->left) and 4 (left->right)
right at 2. right children at 5 (right->left) and 6 (right->right)




*/