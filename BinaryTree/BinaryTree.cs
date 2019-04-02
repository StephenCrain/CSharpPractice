using System;
using System.Globalization;
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
    private const string nullString = "X,";
    
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
        string val = node != null ? node.val.ToString("0','", CultureInfo.InvariantCulture) : nullString;
        sb.Append(val);
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
        TreeNode[] nodes = data.Split(",").Select(stringVal => Int32.TryParse(stringVal, out int val) ? new TreeNode(val) : null).ToArray();
        
        if (nodes.Length == 0 || nodes[0] == null) {
            return null;
        }

        // increment offSet by two each time we hit a null.
        // for each null we are missing two nodes in the next level.
        int offSet = 0;

        // set up the pointers
        for (int i = 0; i < nodes.Length; i++) {
            var curNode = nodes[i];
            
            if (curNode == null) {
                offSet += 2;
                continue;
            }

            int leftIndex = i * 2 + 1 - offSet;
            if (leftIndex < nodes.Length) {
                curNode.left = nodes[leftIndex];
            }

            int rightIndex = i * 2 + 2 - offSet;
            if (rightIndex < nodes.Length) {
                curNode.right = nodes[rightIndex];
            }
        }
        
        
        return nodes[0];
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