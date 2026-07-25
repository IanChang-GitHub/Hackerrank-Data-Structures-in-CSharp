using System;
using System.Collections.Generic;
using System.IO;

class Node
{
    public int frequency;
    public char data;
    public Node left, right;

    public Node(char data, int frequency)
    {
        this.data = data;
        this.frequency = frequency;
    }
}

class Solution
{

    static void decode(string s, Node root) //解碼
    {
        if (root == null || string.IsNullOrEmpty(s)) 
            return;

        Node current = root;
        for (int i = 0; i < s.Length; i++)
        {
            current = (s[i] == '0') ? current.left : current.right; //0走左子樹,1走右子樹

            if (current.left == null && current.right == null)
            {
                Console.Write(current.data);
                current = root;
            }
        }
    }

    static void BuildCodeMap(Node node, string currentCode, Dictionary<char, string> codeMap) //DFS
    {
        if (node == null) 
            return;

        if (node.left == null && node.right == null)
        {
            codeMap[node.data] = currentCode;
            return;
        }

        BuildCodeMap(node.left, currentCode + "0", codeMap);
        BuildCodeMap(node.right, currentCode + "1", codeMap);
    }

    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) 
            return;

        Dictionary<char, int> freqMap = new Dictionary<char, int>();
        foreach (char c in input) //計算每個字母出現次數
        {
            if (!freqMap.ContainsKey(c))
            {
                freqMap[c] = 0;
            }
            freqMap[c]++;
        }

        List<Node> nodes = new List<Node>();
        foreach (var element in freqMap) //將每個字母做成節點
        {
            nodes.Add(new Node(element.Key, element.Value));
        }

        if (nodes.Count == 1) //字串都是同一個字母
        {
            nodes.Add(new Node('\0', 1)); //製造假節點建樹
        }

        while (nodes.Count > 1) //不斷合併最小的兩個節點成一個父節點，直到剩下一個節點
        {
            nodes = nodes.OrderBy(n => n.frequency).ToList(); //依照出現次數由小到大排序

            Node left = nodes[0];
            Node right = nodes[1];

            Node parent = new Node('\0', left.frequency + right.frequency);//建立空字元父節點，加總出現次數
            parent.left = left;
            parent.right = right;

            nodes.RemoveRange(0, 2);
            nodes.Add(parent);
        }

        Node root = nodes[0];
        Dictionary<char, string> codeMap = new Dictionary<char, string>();
        BuildCodeMap(root, "", codeMap); //產生每個字母編碼表

        string encodedString = "";
        foreach (char c in input) //依照編碼表將每個字母做編碼
        {
            encodedString += codeMap[c];
        }

        decode(encodedString, root);
        Console.WriteLine();

    }
}