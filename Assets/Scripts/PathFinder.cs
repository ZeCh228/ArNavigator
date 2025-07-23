using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public List<Waypoint> FindPath(Waypoint start, Waypoint end)
    {
        Queue<Waypoint> queue = new();
        Dictionary<Waypoint, Waypoint> cameFrom = new();

        queue.Enqueue(start);
        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
                break;

            foreach (var neighbor in current.neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        List<Waypoint> path = new();
        var temp = end;
        while (temp != null)
        {
            path.Insert(0, temp);
            temp = cameFrom.ContainsKey(temp) ? cameFrom[temp] : null;
        }

        return path;
    }

    public Waypoint GetNearestWaypoint(Vector3 position)
    {
        Waypoint nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var wp in FindObjectsOfType<Waypoint>())
        {
            float dist = Vector3.Distance(position, wp.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = wp;
            }
        }

        return nearest;
    }
}
