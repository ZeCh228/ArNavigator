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
        floor6Toggle.isOn = false;
        floor7Toggle.isOn = false;

        floor6Toggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) floor7Toggle.isOn = false;
            UpdateDropdown();
        });

        floor7Toggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) floor6Toggle.isOn = false;
            UpdateDropdown();
        });

        UpdateDropdown();
    }

    private void UpdateDropdown()
    {
        List<string> filteredRooms = new();

        foreach (var entry in roomDatabase.rooms)
        {
            string roomNumber = entry.roomNumber;

            if ((floor6Toggle.isOn && roomNumber.StartsWith("06")) ||
                (floor7Toggle.isOn && roomNumber.StartsWith("07")))
            {
                filteredRooms.Add(roomNumber);
            }
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(filteredRooms);
    }

    public int GetSelectedFloor()
    {
        if (floor6Toggle.isOn) return 6;
        if (floor7Toggle.isOn) return 7;
        return -1;
    }

    public string GetSelectedRoom() => dropdown.options.Count > 0 ? dropdown.options[dropdown.value].text : null;
}
