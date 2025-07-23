using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public float spacing = 0.5f;

    private List<GameObject> spawnedArrows = new();

    public void ClearPath()
    {
        foreach (var arrow in spawnedArrows)
        {
            Destroy(arrow);
        }
        spawnedArrows.Clear();
    }

    public void ShowPath(List<Waypoint> waypoints)
    {
        ClearPath();

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector3 start = waypoints[i].transform.position;
            Vector3 end = waypoints[i + 1].transform.position;
            float distance = Vector3.Distance(start, end);
            int steps = Mathf.FloorToInt(distance / spacing);

            for (int j = 0; j < steps; j++)
            {
                float t = j / (float)steps;
                Vector3 pos = Vector3.Lerp(start, end, t);
                Quaternion rot = Quaternion.LookRotation(end - start);

                GameObject arrow = Instantiate(arrowPrefab, pos, rot);
                spawnedArrows.Add(arrow);
            }
        }
    }
}
