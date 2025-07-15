using UnityEngine;

[ExecuteAlways]
public class WaypointGizmo : MonoBehaviour
{
    public float size = 0.3f;

    void OnDrawGizmos()
    {
        bool isRoomPoint = GetComponent<RoomPointMarker>() != null;

        if (isRoomPoint)
            Gizmos.color = new Color(0.196f, 0.803f, 0.196f);
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, size);
    }
}
