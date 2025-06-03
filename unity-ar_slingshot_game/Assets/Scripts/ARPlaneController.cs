using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneController : MonoBehaviour
{
    //public static GameObject selectedPlane = null;
    public static ARPlane selectedPlane = null;

    [SerializeField] private TextMeshProUGUI searchPlaneText = null;

    [SerializeField] private GameObject startButton = null;

    [SerializeField] private Material selectedPlaneMaterial;
    

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARPlaneManager arPlaneManager = null;
    private ARRaycastManager raycastManager;
    private bool firstPlaneTracked = false;
    private Touchscreen touchScreen;
    private bool planeSelected = false;

    // fonction appelée au lancement du script
    void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        firstPlaneTracked = false;
    }

    void Update()
    {
        if (planeSelected)
        {
            return;  
        }

        touchScreen = Touchscreen.current;
        TouchControl touch = touchScreen.primaryTouch;
        if (touch.press.wasPressedThisFrame)
        {
            Vector2 touchPos = touch.position.ReadValue();
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                HandleRaycastHit(hits[0]);
            }
        }
    }

    void HandleRaycastHit(ARRaycastHit hit)
    {
        selectedPlane = arPlaneManager.GetPlane(hits[0].trackableId);
        planeSelected = true;

        DisableOtherARPlane(selectedPlane);

        if (selectedPlane.TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            meshRenderer.material = selectedPlaneMaterial;
        }

        startButton.SetActive(true);
        searchPlaneText.text = "PLANE SELECTED !";
    }

    void DisableOtherARPlane(ARPlane selectedARPlane)
    {
        if (selectedARPlane != null)
        {
            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                if (plane.trackableId != selectedARPlane.trackableId)
                {
                    plane.gameObject.SetActive(false);
                }
            }

            arPlaneManager.enabled = false;
        }
    }

    void DisableAllARPlane(ARPlane selectedARPlane)
    {
        if (selectedARPlane != null)
        {
            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                var meshRenderer = plane.GetComponent<MeshRenderer>();
                if (meshRenderer != null) meshRenderer.enabled = false;

                var meshVis = plane.GetComponent<ARPlaneMeshVisualizer>();
                if (meshVis != null) meshVis.enabled = false;

                var arPlane = plane.GetComponent<ARPlane>();
                if (arPlane != null) arPlane.enabled = false; else arPlane.enabled = true;
            }

            arPlaneManager.enabled = false;
        }
    }

    public void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> changes)
    {
        if (!firstPlaneTracked)
        {
            searchPlaneText.text = "SELECT A PLANE";
            firstPlaneTracked = true;
        }
    }
}
