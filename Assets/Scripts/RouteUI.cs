using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RouteUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField inputFrom;
    public TMP_InputField inputTo;
    public Toggle fromCameraToggle;
    public Transform arCameraTransform;
    public TMP_Text errorText;

    [Header("Logic References")]
    public RoomToWaypointDatabase roomDatabase;
    public PathFinder pathFinder;
    public PathVisualizer pathVisualizer;

    private CanvasGroup inputFromGroup;

    void Start()
    {
        fromCameraToggle.isOn = false; 

        inputFromGroup = inputFrom.GetComponent<CanvasGroup>();
        if (inputFromGroup == null)
        {
            inputFromGroup = inputFrom.gameObject.AddComponent<CanvasGroup>();
        }

        ApplyFromToggleVisual(); 
        fromCameraToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        ApplyFromToggleVisual();
    }

    void ApplyFromToggleVisual()
    {
        bool isOn = fromCameraToggle.isOn;

        inputFrom.interactable = !isOn;
        inputFromGroup.alpha = isOn ? 0.2f : 1f;
    }

    public void OnFindPathClicked()
    {
        string to = inputTo.text.Trim();
        Waypoint start = null;
        Waypoint end = null;

        if (fromCameraToggle.isOn)
        {
            start = pathFinder.GetNearestWaypoint(arCameraTransform.position);
        }
        else
        {
            string from = inputFrom.text.Trim();
            if (string.IsNullOrEmpty(from))
            {
                ShowError("Введите кабинет отправления или включите галочку");
                return;
            }

            start = roomDatabase.GetWaypoint(from);
        }

        if (string.IsNullOrEmpty(to))
        {
            ShowError("Введите кабинет назначения");
            return;
        }

        end = roomDatabase.GetWaypoint(to);

        if (start == null || end == null)
        {
            ShowError("Не удалось найти указанные кабинеты");
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

    void ShowError(string message)
    {
        Debug.LogWarning(message);
        errorText.text = message;
        errorText.color = Color.red;
        errorText.fontStyle = FontStyles.Bold;
        errorText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideError));
        Invoke(nameof(HideError), 3f);
    }

    void HideError()
    {
        errorText.gameObject.SetActive(false);
    }
}
