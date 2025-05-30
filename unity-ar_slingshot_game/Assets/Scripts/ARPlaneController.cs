using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ARPlaneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI searchPlaneText = null;

    public static ARPlane selectedARPlane = null;
    public Camera arCamera;
    public ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARPlaneManager arPlaneManager = null;

    private bool isFound;
    private Touchscreen touchScreen;

    // fonction appelée au lancement du script
    void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        isFound = false;
    }

    void Update()
    {


        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits))
                {
                    searchPlaneText.text = string.Format("{0}", hits[0].trackableId);
                    HandleRaycastHit(hits[0]);
                }
            }
        }*/
        touchScreen = Touchscreen.current;
        TouchControl touch = touchScreen.primaryTouch;
        if (touch.press.wasPressedThisFrame)
        {
            Vector2 touchPos = touch.position.ReadValue();
            if (raycastManager.Raycast(touchPos, hits))
            {
                searchPlaneText.text = string.Format("{0}", hits[0].trackableId);
                HandleRaycastHit(hits[0]);
            }
        }
    }

    public void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> changes)
    {
        if (!isFound)
        {
            searchPlaneText.text = "SELECT A PLANE";
            isFound = true;
        }

        /*foreach (ARPlane plane in changes.added)
        {
            plane.gameObject.AddComponent<ARAnchor>();
        }*/

    }


    void HandleRaycastHit(ARRaycastHit hit)
    {
        searchPlaneText.text = string.Format("HandleRaycastHit");
        //if (hit.trackable is ARPlane)
        //{
            selectedARPlane = arPlaneManager.GetPlane(hits[0].trackableId);
            selectedARPlane.gameObject.AddComponent<ARAnchor>();
            searchPlaneText.text = string.Format("InstanceID: {0}", selectedARPlane.GetInstanceID());
            DisableOtherARPlane(selectedARPlane);
        //}
    }

    void DisableOtherARPlane(ARPlane selectedPlane)
    {
        if (selectedPlane != null)
        {
            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                if (plane.GetInstanceID() != selectedPlane.GetInstanceID())
                {
                    plane.gameObject.SetActive(false);
                }
            }

            searchPlaneText.text = "PLANE SELECTED !";
            arPlaneManager.enabled = false;
        }
    }
}
