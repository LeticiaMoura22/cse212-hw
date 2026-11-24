public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: ignore duplicates so the tree stays a sorted set.
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Found the value at this node.
        if (value == Data)
            return true;

        // Search the left subtree for smaller values, otherwise the right subtree.
        if (value < Data)
            return Left is not null && Left.Contains(value);

        return Right is not null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // Height is one plus the taller of the two subtrees (an empty subtree has height 0).
        int leftHeight = Left is null ? 0 : Left.GetHeight();
        int rightHeight = Right is null ? 0 : Right.GetHeight();
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}