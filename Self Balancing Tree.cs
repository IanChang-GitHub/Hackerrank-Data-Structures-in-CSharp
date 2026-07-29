public class Node
{
    public int value;
    public int height; //Leaf的高度為0
    public Node left;
    public Node right;
}

public class Solution
{
    public static void Main(string[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        List<int> inputs = new List<int>();
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string p in parts)
            {
                inputs.Add(int.Parse(p));
            }
        }

        if (inputs.Count == 0) 
            return;

        int nodeNum = inputs[0];
        Node root = null;

        for (int i = 1; i <= nodeNum; i++) //建立初始的AVL樹
        {
            if (i < inputs.Count)
            {
                root = Insert(root, inputs[i]);
            }
        }

        if (nodeNum + 1 < inputs.Count) //要插入的值
        {
            int newNode = inputs[nodeNum + 1];
            root = Insert(root, newNode);
        }

        List<string> inOrderResult = new List<string>();
        List<string> preOrderResult = new List<string>();

        InOrder(root, inOrderResult);
        PreOrder(root, preOrderResult);

        Console.WriteLine(string.Join(" ", inOrderResult));
        Console.WriteLine(string.Join(" ", preOrderResult));
    }

    private static void InOrder(Node root, List<string> result)
    {
        if (root != null) //中序走訪: 左 -> 中 -> 右
        {
            InOrder(root.left, result);
            result.Add($"{root.value}(BF={GetBalance(root)})"); 
            InOrder(root.right, result);
        }
    }

    private static void PreOrder(Node root, List<string> result)
    {
        if (root != null) //前序走訪:中 -> 左 -> 右
        {
            result.Add($"{root.value}(BF={GetBalance(root)})");
            PreOrder(root.left, result);
            PreOrder(root.right, result);
        }
    }

    private static int GetHeight(Node node)
    {
        if (node == null) 
            return -1;
        else
            return node.height;
    }

    private static int GetBalance(Node node)
    {
        if (node == null) 
            return 0;
        else
            return GetHeight(node.left) - GetHeight(node.right);
    }

    private static Node RightRotate(Node y)
    {
        Node x = y.left;
        Node temp = x.right;

        x.right = y;
        y.left = temp;

        y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
        x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;

        return x;
    }

    private static Node LeftRotate(Node x)
    {
        Node y = x.right;
        Node temp = y.left;

        y.left = x;
        x.right = temp;

        x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;
        y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;

        return y;
    }

    public static Node Insert(Node root, int value)
    {
        if (root == null)
        {
            Node node = new Node();
            node.value = value;
            node.left = null;
            node.right = null;
            node.height = 0;
            return node;
        }

        if (value < root.value)
            root.left = Insert(root.left, value);
        else if (value > root.value)
            root.right = Insert(root.right, value);
        else
            return root;

        root.height = 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));
        int balance = GetBalance(root);

        if (balance > 1 && value < root.left.value) //LL
            return RightRotate(root);

        if (balance < -1 && value > root.right.value) //RR 
            return LeftRotate(root);

        if (balance > 1 && value > root.left.value) //LR 
        {
            root.left = LeftRotate(root.left);
            return RightRotate(root);
        }

        if (balance < -1 && value < root.right.value) //RL 
        {
            root.right = RightRotate(root.right);
            return LeftRotate(root);
        }

        return root;
    }
}