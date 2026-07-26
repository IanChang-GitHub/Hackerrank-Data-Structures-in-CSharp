using System;
using System.Collections.Generic;
using System.IO;
class Queue
{
    private Stack<int> stackIn = new Stack<int>(); //push用
    private Stack<int> stackOut = new Stack<int>(); //pop用
    
    public void Enqueue(int x)
    {
        stackIn.Push(x);
    }
    public void Dequeue()
    {
        reverseStack();
        stackOut.Pop();

    }
    public int Peek() //只看不取
    {
        reverseStack();
        return stackOut.Peek();
        
    }
    
    public void reverseStack() //反轉順序到另一個stack
    {
        if (stackOut.Count == 0) //沒東西pop才做反轉
        {
            while(stackIn.Count > 0)
            {
                stackOut.Push(stackIn.Pop());
            }
        }
    }
}
class Solution {
    static void Main(String[] args) {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        string input = Console.In.ReadToEnd();
        string[] tokens = input.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        
        int q = int.Parse(tokens[0]);
        int index =1;
        
        Queue queue = new Queue();
        for (int i=0;i<q;i++)
        {
            int type = int.Parse(tokens[index++]);
            if (type == 1)
            {
                int x = int.Parse(tokens[index++]);
                queue.Enqueue(x);
            }
            else if(type == 2)
            {
                queue.Dequeue();
            }
            else if(type == 3)
            {
                Console.WriteLine(queue.Peek());
            }
        }
    }
}