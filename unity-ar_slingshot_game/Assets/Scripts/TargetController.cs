using System;
using System.Threading;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    private NavMeshAgent nma = null;
    private Bounds bndFloor;
    private Vector3 moveTo;
    private TextMeshProUGUI searchPlaneText = null;
    private bool isMoving = false;

    private GameObject selectedPlane = null;

    private float timer = 0f;

    private float speed = 0.1f;

    private int nbMovements = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        searchPlaneText = GameObject.Find("SearchPlaneText").GetComponent<TextMeshProUGUI>();
        searchPlaneText.text = "Lancement du TargetController";
        //nma = this.GetComponent<NavMeshAgent>();
        selectedPlane = GameObject.Find("SelectedPlane");
        bndFloor = selectedPlane.GetComponent<Renderer>().bounds;
        moveTo = transform.position;
        searchPlaneText.text = "TargetController initialisé";

    }

    void Update()
    {
        if (isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveTo) < 0.001f)
        {
            isMoving = true;
            SetRandomDestination();
            isMoving = false;
        }
        
    }

    private void SetRandomDestination()
    {
        //1. Pick a point
        float rx = UnityEngine.Random.Range(bndFloor.min.x, bndFloor.max.x);
        float ry = bndFloor.max.y + 0.05f;
        float rz = UnityEngine.Random.Range(bndFloor.min.z, bndFloor.max.z);

        //2. Move to this point
        nbMovements++;
        //searchPlaneText.text = String.Format("movement N° {0} : x={1}, y={2}, z={3}", nbMovements, rx, ry, rz);
        moveTo = new Vector3(rx, ry, rz);

        //MoveToDestination(moveTo);

    }

    private void MoveToDestination(Vector3 destination)
    {
        bool isDestinationReached = false;
        float xIncrement = 0.05f;
        float zIncrement = 0.05f;
        float xStep, zStep;
        Vector3 direction;
        if (transform.position.x > destination.x) xIncrement = -0.05f;
        if (transform.position.z > destination.z) zIncrement = -0.05f;
        while (isDestinationReached)
        {
            xStep = xIncrement;
            if (transform.position.x + xIncrement - destination.x == 0)
            {
                xStep = 0f;
            }
            else if (transform.position.x + xIncrement - destination.x > 0)
            {
                xStep = destination.x - transform.position.x;
            }

            zStep = zIncrement;
            if (transform.position.z + zIncrement - destination.z == 0)
            {
                zStep = 0f;
            }
            else if (transform.position.z + zIncrement - destination.z > 0)
            {
                zStep = destination.z - transform.position.z;
            }

            direction = new Vector3(xStep, 0f, zStep);
            transform.position += direction;

            if (transform.position == destination) isDestinationReached = true;

            //System.Threading.Thread.Sleep(200);
        }
    }
}
