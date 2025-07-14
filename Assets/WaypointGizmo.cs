using UnityEngine;

[ExecuteAlways]
public class WaypointGizmo : MonoBehaviour
{
    public float size = 0.3f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, size);
    }
}
