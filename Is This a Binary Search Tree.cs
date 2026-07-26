using System;
using System.Collections.Generic;
using System.IO;

class Node 
{
    public int data;
    public Node left;
    public Node right;
    
    public Node() 
    {
        this.data = -1;
        this.left = null;
        this.right = null;
    }
}

class Tree 
{
    public List<int> values;
    private int count;
    
    public Tree() 
    {
        this.values = new List<int>();
        this.count = 0;
    }
    
    public void inOrder(Node root, int levels) //中序:左中右
    {
        if(root != null) 
        {
            if (levels > 0) 
            {
                root.left = new Node();
                inOrder(root.left, levels - 1);
            }    
            
            root.data = values[count];
            count++;
            
            if (levels > 0) 
            {
                root.right = new Node();
                inOrder(root.right, levels - 1);
            }
        }
    }
}

class Solution 
{

    /*
     * 解題策略:
     * 1.建立一個數值限制範圍上限與下線
     * 2.在root時，範圍無限大
     * 3.往左走時，上限調整為當前節點的值
     * 4.往右走時，下限調整為當前節點的值
     * 5.若節點的值超出範圍，則二元搜尋樹不合法
    */

    public static bool checkBST(Node root) 
    {
        return CheckBSTHelper(root, int.MinValue, int.MaxValue); //上限與下限
    }

    private static bool CheckBSTHelper(Node node, int min, int max) 
    {
        if (node == null) 
        {
            return true;
        }

        if (node.data <= min || node.data >= max) //超過範圍
        {
            return false;
        }

        return CheckBSTHelper(node.left, min, node.data) && 
               CheckBSTHelper(node.right, node.data, max);
    }

    public static void Main(string[] args) 
    {
        
        string input = Console.In.ReadToEnd();
        string[] tokens = input.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        
        if (tokens.Length == 0)  
            return;

        int treeHeight = int.Parse(tokens[0]);
        
        Tree tree = new Tree();
        for (int i = 1; i < tokens.Length; i++) 
        {
            tree.values.Add(int.Parse(tokens[i]));
        }
        
        Node root = new Node(); 
        tree.inOrder(root, treeHeight); //建樹
        
        Console.WriteLine(checkBST(root) ? "Yes" : "No");
    }
}