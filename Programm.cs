using System;
using System.Collections.Generic;
using System.IO;

public class Tree
{
    private Dictionary<int, List<int>> adjacencyList;

    public Tree()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int parent, int child)
    {
        if (!adjacencyList.ContainsKey(parent))
            adjacencyList[parent] = new List<int>();

        adjacencyList[parent].Add(child);

        if (!adjacencyList.ContainsKey(child))
            adjacencyList[child] = new List<int>();

        adjacencyList[child].Add(parent);
    }

    public (int, int) FindDepthAndDeepestNode(int start)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<(int, int)>();
        queue.Enqueue((start, 0));
        visited.Add(start);
        int maxDepth = 0;
        int deepestNode = start;

        while (queue.Count > 0)
        {
            var (node, depth) = queue.Dequeue();

            if (depth > maxDepth)
            {
                maxDepth = depth;
                deepestNode = node;
            }

            foreach (var neighbor in adjacencyList[node])
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue((neighbor, depth + 1));
                    visited.Add(neighbor);
                }
            }
        }

        return (maxDepth, deepestNode);
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "graph.txt";
        Tree tree = new Tree();
        using (StreamReader reader = new StreamReader(filePath))
        {
            int verticesCount = int.Parse(reader.ReadLine());
            for (int i = 0; i < verticesCount; i++)
            {
                string[] line = reader.ReadLine().Split(' ');
                int node = int.Parse(line[0]);

                for (int j = 1; j < line.Length; j++)
                {
                    int neighbor = int.Parse(line[j]);
                    tree.AddEdge(node, neighbor);
                }
            }
        }

        Console.Write("Введите номер вершины для поиска глубины: ");
        int startNodeValue = int.Parse(Console.ReadLine());

        var (depth, deepestNode) = tree.FindDepthAndDeepestNode(startNodeValue);
        Console.WriteLine($"Глубина от вершины {startNodeValue}: {depth}");
        Console.WriteLine($"Самая глубокая вершина: {deepestNode}"); 
    }
}
