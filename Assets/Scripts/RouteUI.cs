using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteUI : MonoBehaviour
{
    public TMP_InputField inputFrom;
    public Toggle fromMeToggle;
    public Button findPathButton;
    public FloorRoomSelector floorRoomSelector;
    public RoomToWaypointDatabase roomDatabase;
    public PathFinder pathFinder;
    public PathVisualizer pathVisualizer;

    public GameObject errorPanel;
    public TMP_Text errorText;

    private void Start()
    {
        findPathButton.onClick.AddListener(OnFindPathClicked);
        errorPanel.SetActive(false);
    }

    private void OnFindPathClicked()
    {
        Waypoint start = null;

        if (fromMeToggle.isOn)
        {
            start = pathFinder.FindNearestWaypointToCamera();
        }
        else
        {
            string from = inputFrom.text.Trim();
            if (string.IsNullOrEmpty(from))
            {
                ShowError("Введите кабинет отправления");
                return;
            }

            start = roomDatabase.GetWaypoint(from);
        }

        string to = floorRoomSelector.GetSelectedRoom();
        if (string.IsNullOrEmpty(to))
        {
            ShowError("Выберите кабинет назначения");
            return;
        }

        Waypoint end = roomDatabase.GetWaypoint(to);

        if (start == null || end == null)
        {
            ShowError("Не удалось найти точки маршрута");
            return;
        }

        var path = pathFinder.FindPath(start, end);
        if (path != null && path.Count > 1)
        {
            pathVisualizer.ShowPath(path);
        }
        else
        {
            ShowError("Путь не найден");
        }
    }

    private void ShowError(string message)
    {
        Debug.LogWarning(message);
        errorText.text = message;
        errorPanel.SetActive(true);
        CancelInvoke(nameof(HideError));
        Invoke(nameof(HideError), 3f);
    }

    private void HideError()
    {
        errorPanel.SetActive(false);
    }
}
