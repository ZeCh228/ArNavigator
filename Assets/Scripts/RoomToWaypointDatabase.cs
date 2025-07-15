using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RoomWaypointPair
{
    public string roomNumber;
    public Waypoint nearestWaypoint;
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
