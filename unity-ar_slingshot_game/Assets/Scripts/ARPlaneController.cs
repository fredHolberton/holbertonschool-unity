using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using Unity.AI.Navigation;

public class ARPlaneController : MonoBehaviour
{
    [SerializeField] private GameObject selectedPlane = null;

    [SerializeField] private TextMeshProUGUI searchPlaneText = null;

    [SerializeField] private GameObject startButton = null;

    [SerializeField] private Material selectedPlaneMaterial;

    [SerializeField] private GameObject spawnZone;

    

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
        ClonerSelectedPlane(arPlaneManager.GetPlane(hits[0].trackableId));

        DisableAllARPlane();
        arPlaneManager.enabled = false;
        planeSelected = true;

        startButton.SetActive(true);
        searchPlaneText.text = "PLANE SELECTED !";

        // Set the selected plane static, build a navmesh data into it and activate it
        if (!selectedPlane.activeSelf) selectedPlane.SetActive(true);
        
        // activate the spawnZone
        spawnZone.SetActive(true);
    }

    void ClonerSelectedPlane(ARPlane selectedARPlane)
    {
        Mesh clonedMesh = Instantiate(selectedARPlane.GetComponent<MeshFilter>().mesh);
        selectedPlane.GetComponent<MeshFilter>().mesh = clonedMesh;
        selectedPlane.GetComponent<MeshCollider>().sharedMesh = clonedMesh;

        // Apply position / rotation / scale
        selectedPlane.transform.position = selectedARPlane.transform.position;
        selectedPlane.transform.rotation = selectedARPlane.transform.rotation;
        selectedPlane.transform.localScale = selectedARPlane.transform.localScale;
    }

    void DisableAllARPlane()
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
