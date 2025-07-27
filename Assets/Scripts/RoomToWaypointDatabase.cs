using UnityEngine;
using System.Collections.Generic;

public enum Floor
{
    Floor6,
    Floor7
}

[System.Serializable]
public class RoomWaypointPair
{
    public string roomNumber;
    public Waypoint nearestWaypoint;
    public Floor floor;
}


public class RoomToWaypointDatabase : MonoBehaviour
{
    public List<RoomWaypointPair> rooms;

    public Waypoint GetWaypoint(string roomNumber)
    {
        foreach (var pair in rooms)
        {
            if (pair.roomNumber == roomNumber)
                return pair.nearestWaypoint;
        }
        return null;
    }
}
