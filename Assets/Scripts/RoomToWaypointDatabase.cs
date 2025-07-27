using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RoomWaypointPair
{
    public string roomNumber;
    public Waypoint nearestWaypoint;
    public int floor;
}

public class RoomToWaypointDatabase : MonoBehaviour
{
    public List<RoomWaypointPair> rooms;

    public Waypoint GetWaypoint(string roomNumber)
    {
        foreach (var pair in rooms)
        {
            if (pair.roomNumber == roomNumber)
            {
                Debug.Log("Waypoint ������ ��� ������� " + roomNumber + ": " + pair.nearestWaypoint.name);
                return pair.nearestWaypoint;
            }
        }

        Debug.LogWarning("�� ������ Waypoint ��� �������: " + roomNumber);
        return null;
    }

    public int GetFloor(string roomNumber)
    {
        foreach (var pair in rooms)
        {
            if (pair.roomNumber == roomNumber)
                return pair.floor;
        }
        return -1;
    }
}
