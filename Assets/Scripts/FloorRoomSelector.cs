using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloorRoomSelector : MonoBehaviour
{
    public RoomToWaypointDatabase roomDatabase;
    public TMP_Dropdown dropdown;
    public Toggle floor6Toggle;
    public Toggle floor7Toggle;

    private void Start()
    {
        floor6Toggle.onValueChanged.AddListener(_ => OnToggleChanged(floor6Toggle));
        floor7Toggle.onValueChanged.AddListener(_ => OnToggleChanged(floor7Toggle));

        floor6Toggle.isOn = false;
        floor7Toggle.isOn = false;

        UpdateDropdown();
    }

    private void OnToggleChanged(Toggle changedToggle)
    {
        if (changedToggle == floor6Toggle && floor6Toggle.isOn)
            floor7Toggle.isOn = false;
        else if (changedToggle == floor7Toggle && floor7Toggle.isOn)
            floor6Toggle.isOn = false;

        UpdateDropdown();
    }

    private void UpdateDropdown()
    {
        List<string> filteredRooms = new();

        foreach (var entry in roomDatabase.rooms)
        {
            if ((floor6Toggle.isOn && entry.floor == Floor.Floor6) ||
                (floor7Toggle.isOn && entry.floor == Floor.Floor7))
            {
                filteredRooms.Add(entry.roomNumber);
            }
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(filteredRooms);
    }

    public string GetSelectedRoom() =>
        dropdown.options.Count > 0 ? dropdown.options[dropdown.value].text : null;
}
