using System;
using System.Text;
using System.Linq;

public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
 }
 

public class Codec {
    private const string nullString = "null";
    
    // Encodes a tree to a single string.
    public string serialize(TreeNode root) {
        var depth = FindDepth(root, 0);

        if (depth == 0) {
            return nullString;
        }

        var sb = new StringBuilder();
        AppendValue(root, sb);
        var res = stringify(root, sb, 1, depth);
        sb.Remove(sb.Length - 1, 1);
        return res;
    }
    
    private string stringify(TreeNode node, StringBuilder sb, int curDepth, int depth) {
        if (curDepth == depth) {
            return String.Empty;
        }
        
        TreeNode left = null;
        TreeNode right = null;
        
        if (node != null) {
            left = node.left;
            right = node.right;
        }
        
        AppendValue(left, sb);
        AppendValue(right, sb);
        Console.WriteLine($"L+R Appended. curDepth: {curDepth}. {sb}");

        sb.Append(stringify(left, new StringBuilder(), curDepth + 1, depth));
        Console.WriteLine($"LeftTree Appended. curDepth: {curDepth}. {sb}");
        sb.Append(stringify(right, new StringBuilder(), curDepth + 1, depth));
        Console.WriteLine($"LeftTree Appended. curDepth: {curDepth}. {sb}");
        
        Console.WriteLine($"Returning. curDepth: {curDepth}. {sb}");
        return sb.ToString();
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

        if (leftIndex < nodes.Length && nodes[leftIndex].HasValue) {
            node.left = new TreeNode(nodes[leftIndex].Value);
            treeify(node.left, nodes, leftIndex);
        }
        
        if (rightIndex < nodes.Length && nodes[rightIndex].HasValue) {
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