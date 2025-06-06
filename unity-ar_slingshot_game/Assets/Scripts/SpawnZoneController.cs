using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class SpawnZoneController : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int nbSpawnedTarget = 5;

    [SerializeField] private GameObject selectedPlane;

    [SerializeField] private TextMeshProUGUI searchPlaneText = null;

    private GameObject[] spawnedTargets;
    private int maxTries = 10;

    private NavMeshAgent nma = null;
    private Bounds bndFloor;
    private Vector3 moveTo;


    // Start is call by Unity
    void Start()
    {
        searchPlaneText.text = "Lancement du SpawnZoneController";
        spawnedTargets = new GameObject[nbSpawnedTarget];
        bndFloor = selectedPlane.GetComponent<Renderer>().bounds;

        // spawn of the targets
        searchPlaneText.text = String.Format("min.x={0}, max.x={1}\nmin.y={2}, max.y={3}\nmin.z={4}, max.z={5}",
                                            bndFloor.min.x, bndFloor.max.x,
                                            bndFloor.min.y, bndFloor.max.y,
                                            bndFloor.min.z, bndFloor.max.z);
        for (int i = 0; i < nbSpawnedTarget; i++)
        {
            SpawnTargetToRandomDestination(i);
        /*spawnedTargets[0] = Instantiate(targetPrefab, selectedPlane.transform.position, selectedPlane.transform.rotation, transform);
        spawnedTargets[0].transform.position += new Vector3(0f, 0f, 0f);
        spawnedTargets[1] = Instantiate(targetPrefab, selectedPlane.transform.position, selectedPlane.transform.rotation, transform);
        spawnedTargets[1].transform.position += new Vector3(0f, 0.1f, 0f);
        spawnedTargets[2] = Instantiate(targetPrefab, selectedPlane.transform.position, selectedPlane.transform.rotation, transform);
        spawnedTargets[2].transform.position += new Vector3(0f, 0.2f, 0f);
        spawnedTargets[3] = Instantiate(targetPrefab, selectedPlane.transform.position, selectedPlane.transform.rotation, transform);
        spawnedTargets[3].transform.position += new Vector3(0f, 0.3f, 0f);
        spawnedTargets[4] = Instantiate(targetPrefab, selectedPlane.transform.position, selectedPlane.transform.rotation, transform);
        spawnedTargets[4].transform.position += new Vector3(0f, 0.4f, 0f);*/
        }
    }

    void SpawnTargetToRandomDestination(int i)
    {
        //nma = spawnedTargets[i].GetComponent<NavMeshAgent>();
        //1. Pick a point
        float rx = UnityEngine.Random.Range(bndFloor.min.x, bndFloor.max.x);
        float ry = bndFloor.max.y + 0.05f;
        float rz = UnityEngine.Random.Range(bndFloor.min.z, bndFloor.max.z);
        moveTo = new Vector3(rx, ry, rz);
        spawnedTargets[i] = Instantiate(targetPrefab, moveTo, selectedPlane.transform.rotation, transform);                                     
    }

}
