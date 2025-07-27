using UnityEngine;
using UnityEngine.UI;

public class RouteUI : MonoBehaviour
{
    public Button findPathButton;
    public FloorRoomSelector floorRoomSelector;
    public RoomToWaypointDatabase roomDatabase;
    public PathFinder pathFinder;
    public PathVisualizer pathVisualizer;

    private void Start()
    {
        findPathButton.onClick.AddListener(FindPath);
    }

    private void FindPath()
    {
        string selectedRoom = floorRoomSelector.GetSelectedRoom();
        int selectedFloor = floorRoomSelector.GetSelectedFloor(); // если используешь по префиксу, этот метод должен быть

        Debug.Log("Выбран кабинет: " + selectedRoom);
        Debug.Log("Определён этаж: " + selectedFloor);

        if (string.IsNullOrEmpty(selectedRoom))
        {
            Debug.LogWarning("Кабинет не выбран.");
            return;
        }

        Waypoint endWaypoint = roomDatabase.GetWaypoint(selectedRoom);

        if (endWaypoint == null)
        {
            Debug.LogError("Не найден Waypoint для кабинета: " + selectedRoom);
            return;
        }

        Vector3 playerPosition = Camera.main.transform.position;
        Waypoint startWaypoint = pathFinder.GetNearestWaypoint(playerPosition, selectedFloor);

        if (startWaypoint == null)
        {
            Debug.LogError("Не найден ближайший Waypoint к игроку. Позиция: " + playerPosition + ", этаж: " + selectedFloor);
            return;
        }

        Debug.Log("Стартовый Waypoint: " + startWaypoint.name);
        Debug.Log("Конечный Waypoint: " + endWaypoint.name);

        var path = pathFinder.FindPath(startWaypoint, endWaypoint);

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("Путь не найден.");
            return;
        }

        Debug.Log("Путь найден. Кол-во точек: " + path.Count);
        pathVisualizer.ShowPath(path);
    }
}
