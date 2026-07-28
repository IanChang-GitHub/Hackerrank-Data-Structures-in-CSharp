using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

public class Result
{
    public static List<List<int>> swapNodes(List<List<int>> indexes, List<int> queries)
    {
        List<List<int>> result = new List<List<int>>();
        int num = indexes.Count;
        
        int[] leftChild = new int[num + 1]; //節點編號編號1~n，用陣列索引表示
        int[] rightChild = new int[num + 1];

        for (int i = 0; i < num; i++) //紀錄每個節點左右子節點
        {
            leftChild[i + 1] = indexes[i][0];
            rightChild[i + 1] = indexes[i][1];
        }


        foreach (int k in queries)
        {
            Swap(1, 1, k, leftChild, rightChild); //root, depth, query, leftchild, rightchild

            List<int> currentTraversal = new List<int>();
            InOrder(1, leftChild, rightChild, currentTraversal);

            result.Add(currentTraversal);
        }

        return result;
    }

    private static void Swap(int node, int depth, int k, int[] leftChild, int[] rightChild) //左右子樹對調
    {
        if (node == -1)
        {
            return;
        }

        if (depth % k == 0) //樹高第k,2k,3k...層左右子樹對調
        {
            int temp = leftChild[node];
            leftChild[node] = rightChild[node];
            rightChild[node] = temp;
        }

        Swap(leftChild[node], depth + 1, k, leftChild, rightChild); //往下遞迴左子樹與右子樹，深度+1
        Swap(rightChild[node], depth + 1, k, leftChild, rightChild);
    }

    private static void InOrder(int node, int[] leftChild, int[] rightChild, List<int> traversal) //中序走訪
    {
        if (node == -1)
        {
            return;
        }

        // 中序走訪順序：左-> 中-> 右
        InOrder(leftChild[node], leftChild, rightChild, traversal);
        traversal.Add(node); 
        InOrder(rightChild[node], leftChild, rightChild, traversal); 
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> indexes = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            indexes.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(indexesTemp => Convert.ToInt32(indexesTemp)).ToList());
        }

        int queriesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> queries = new List<int>();

        for (int i = 0; i < queriesCount; i++)
        {
            int queriesItem = Convert.ToInt32(Console.ReadLine().Trim());
            queries.Add(queriesItem);
        }

        List<List<int>> result = Result.swapNodes(indexes, queries);

        textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

        textWriter.Flush();
        textWriter.Close();
    }
}