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

class Result
{

    /*
     * Complete the 'matchingStrings' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. STRING_ARRAY stringList
     *  2. STRING_ARRAY queries
     *  
     *  解題策略:
     *  1.先整個掃過一次list，計算每一個字串出現次數並記錄下來
     *  2.查詢直接查表看出現幾次
     */

    public static List<int> matchingStrings(List<string> stringList, List<string> queries)
    {
        //改良版:用Dictionary
        Dictionary<string, int> frequencyMap = new Dictionary<string, int>();
        List<int> result = new List<int>();

        foreach (string str in stringList) //紀錄每個字串出現次數
        {
            frequencyMap.TryGetValue(str, out int count); //搜尋Dictionary內有無符合的key並回傳value，若無找到會回傳0
            frequencyMap[str] = count + 1;
        }

        foreach (string query in queries)
        {
            frequencyMap.TryGetValue(query, out int count); //查表
            result.Add(count);
        }

        return result;


        /*
        List<int> result = new List<int>();
        foreach (string query in queries)
        {
            int num = 0;
            foreach (string str in stringList)
            {
                if (query ==  str)
                    num++;
            }
            result.Add(num);
        }
        return result;
        */
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int stringListCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> stringList = new List<string>();

        for (int i = 0; i < stringListCount; i++)
        {
            string stringListItem = Console.ReadLine();
            stringList.Add(stringListItem);
        }

        int queriesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> queries = new List<string>();

        for (int i = 0; i < queriesCount; i++)
        {
            string queriesItem = Console.ReadLine();
            queries.Add(queriesItem);
        }

        List<int> res = Result.matchingStrings(stringList, queries);

        textWriter.WriteLine(String.Join("\n", res));

        textWriter.Flush();
        textWriter.Close();
    }
}
