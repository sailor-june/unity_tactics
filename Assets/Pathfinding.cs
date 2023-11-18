using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Tilemap tilemap;


    public List<Vector3Int> FindPath(Vector3Int startPos, Vector3Int endPos)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        PriorityQueue<Vector3Int> openSet = new PriorityQueue<Vector3Int>();

        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float>();
        gScore[startPos] = 0;

        openSet.Enqueue(startPos, Heuristic(startPos, endPos));

        while (openSet.Count > 0)
        {
            Vector3Int current = openSet.Dequeue();

            if (current == endPos)
            {
                path = ReconstructPath(cameFrom, current);
                break;
            }

            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Heuristic(current, neighbor);

                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    float fScore = tentativeGScore + Heuristic(neighbor, endPos);
                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor, fScore);
                }
            }
        }

        return path;
    }

    float Heuristic(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    List<Vector3Int> GetNeighbors(Vector3Int cell)
{
    List<Vector3Int> neighbors = new List<Vector3Int>();

    Vector3Int[] directions =
    {
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0)
    };

    foreach (Vector3Int dir in directions)
    {
        Vector3Int neighbor = cell + dir;
        neighbors.Add(neighbor);
    }

    return neighbors;
}



    List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}

public class PriorityQueue<T>
{
    private readonly SortedDictionary<float, Queue<T>> _sortedItems = new SortedDictionary<float, Queue<T>>();

    public int Count
    {
        get
        {
            int count = 0;
            foreach (var pair in _sortedItems)
                count += pair.Value.Count;
            return count;
        }
    }

    public bool Contains(T item)
    {
        foreach (var pair in _sortedItems)
        {
            if (pair.Value.Contains(item))
                return true;
        }
        return false;
    }

    public void Enqueue(T item, float priority)
    {
        if (!_sortedItems.ContainsKey(priority))
        {
            _sortedItems[priority] = new Queue<T>();
        }

        _sortedItems[priority].Enqueue(item);
    }

    public T Dequeue()
{
    var enumerator = _sortedItems.GetEnumerator();
    enumerator.MoveNext();
    var pair = enumerator.Current;
    var queue = pair.Value;

    var item = queue.Dequeue();
    if (queue.Count == 0)
    {
        _sortedItems.Remove(pair.Key);
    }

    return item;
}
}