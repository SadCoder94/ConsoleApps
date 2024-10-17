using System;

class SegmentTreeNode
{
    public int Start { get; set; }
    public int End { get; set; }
    public float Supply { get; set; }
    public float Demand { get; set; }
    public float Inventory { get; set; }
    public float LazySupply { get; set; }
    public float LazyDemand { get; set; }
}

class InventorySegmentTree
{
    private readonly SegmentTreeNode[] tree;
    private readonly int n;

    public InventorySegmentTree(int n)
    {
        this.n = n;
        tree = new SegmentTreeNode[4 * n];
        BuildTree(0, 0, n - 1);
    }

    private void BuildTree(int node, int start, int end)
    {
        if (node >= tree.Length) return; // Prevent out-of-bounds access

        if (tree[node] == null)
        {
            tree[node] = new SegmentTreeNode { Start = start, End = end };
        }

        if (start == end) return; // Leaf node
        int mid = (start + end) / 2;
        BuildTree(2 * node + 1, start, mid); // Left child
        BuildTree(2 * node + 2, mid + 1, end); // Right child
    }

    public void AddSupply(int bucket, float delta)
    {
        Update(0, bucket, n - 1, bucket, n - 1, delta, 0);
    }

    public void AddDemand(int bucket, float delta)
    {
        Update(0, bucket, n - 1, bucket, n - 1, 0, delta);
    }

    public float GetInventory(int bucket)
    {
        return Query(0, bucket, n - 1, bucket);
    }

    private void Update(int node, int start, int end, int l, int r, float supply, float demand)
    {
        if (node >= tree.Length || tree[node] == null) return; // Prevent out-of-bounds access

        if (tree[node].LazySupply != 0 || tree[node].LazyDemand != 0)
        {
            tree[node].Supply += (tree[node].End - tree[node].Start + 1) * tree[node].LazySupply;
            tree[node].Demand += (tree[node].End - tree[node].Start + 1) * tree[node].LazyDemand;
            tree[node].Inventory = tree[node].Supply - tree[node].Demand;

            if (tree[node].Start != tree[node].End)
            {
                tree[2 * node + 1].LazySupply += tree[node].LazySupply;
                tree[2 * node + 1].LazyDemand += tree[node].LazyDemand;
                tree[2 * node + 2].LazySupply += tree[node].LazySupply;
                tree[2 * node + 2].LazyDemand += tree[node].LazyDemand;
            }

            tree[node].LazySupply = 0;
            tree[node].LazyDemand = 0;
        }

        if (start > end || start > r || end < l) return;

        if (start >= l && end <= r)
        {
            tree[node].Supply += (end - start + 1) * supply;
            tree[node].Demand += (end - start + 1) * demand;
            tree[node].Inventory = tree[node].Supply - tree[node].Demand;

            if (start != end)
            {
                tree[2 * node + 1].LazySupply += supply;
                tree[2 * node + 1].LazyDemand += demand;
                tree[2 * node + 2].LazySupply += supply;
                tree[2 * node + 2].LazyDemand += demand;
            }
            return;
        }

        int mid = (start + end) / 2;
        Update(2 * node + 1, start, mid, l, r, supply, demand);
        Update(2 * node + 2, mid + 1, end, l, r, supply, demand);

        tree[node].Supply = tree[2 * node + 1].Supply + tree[2 * node + 2].Supply;
        tree[node].Demand = tree[2 * node + 1].Demand + tree[2 * node + 2].Demand;
        tree[node].Inventory = tree[node].Supply - tree[node].Demand;
    }

    private float Query(int node, int start, int end, int bucket)
    {
        if (node >= tree.Length || tree[node] == null) return 0; // Prevent out-of-bounds access

        if (tree[node].LazySupply != 0 || tree[node].LazyDemand != 0)
        {
            tree[node].Supply += (tree[node].End - tree[node].Start + 1) * tree[node].LazySupply;
            tree[node].Demand += (tree[node].End - tree[node].Start + 1) * tree[node].LazyDemand;
            tree[node].Inventory = tree[node].Supply - tree[node].Demand;

            if (tree[node].Start != tree[node].End)
            {
                tree[2 * node + 1].LazySupply += tree[node].LazySupply;
                tree[2 * node + 1].LazyDemand += tree[node].LazyDemand;
                tree[2 * node + 2].LazySupply += tree[node].LazySupply;
                tree[2 * node + 2].LazyDemand += tree[node].LazyDemand;
            }

            tree[node].LazySupply = 0;
            tree[node].LazyDemand = 0;
        }

        if (start > end || start > bucket || end < bucket) return 0;

        if (start == end) return tree[node].Inventory;

        int mid = (start + end) / 2;
        if (bucket <= mid)
            return Query(2 * node + 1, start, mid, bucket);
        else
            return Query(2 * node + 2, mid + 1, end, bucket);
    }
}

class Program
{
    static void Main()
    {
        int n = 10; // Number of buckets
        var inventoryTree = new InventorySegmentTree(n);
        inventoryTree.AddSupply(3, 10); // Add supply 10 to bucket 3
        inventoryTree.AddDemand(3, 5); // Add demand 5 to bucket 3
        for (int i = 3; i < n; i++)
        {
            float inventory = inventoryTree.GetInventory(i); // Get inventory for each bucket from 3 to n-1
            Console.WriteLine($"Inventory for bucket {i}: {inventory}");
        }
    }
}
