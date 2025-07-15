using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new();

    void Update()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.wasPressedThisFrame == false)
            return;

        var touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(arrowPrefab, hitPose.position, Quaternion.LookRotation(Camera.main.transform.forward));
        }
    }
}
