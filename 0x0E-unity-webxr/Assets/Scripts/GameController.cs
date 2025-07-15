using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Range(0, 9)] private int nbSpawnedObstacle = 5;
    [SerializeField] private GameObject alleyFloor;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject obstacleGroup;

    [SerializeField] private TextMeshProUGUI scoreText = null;

    private Bounds bndFloor;
    private GameObject[] spawnedObstacles;

    private float minZ;
    private float maxZ;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bndFloor = alleyFloor.GetComponent<Renderer>().bounds;
        minZ = bndFloor.min.z + ((bndFloor.max.z - bndFloor.min.z) / 4f);
        maxZ = bndFloor.max.z - ((bndFloor.max.z - bndFloor.min.z) / 4f);
        spawnedObstacles = new GameObject[nbSpawnedObstacle];
        scoreText.text = string.Format("{0}", score);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < nbSpawnedObstacle; i++)
        {
            if (spawnedObstacles[i] != null)
                Destroy(spawnedObstacles[i]);

            SpawnObstacleToRandomDestination(i);
        }
    }

    private void SpawnObstacleToRandomDestination(int i)
    {
        float rx = UnityEngine.Random.Range(bndFloor.min.x, bndFloor.max.x);
        float ry = 0f;
        float rz = UnityEngine.Random.Range(minZ, maxZ);
        Vector3 moveTo = new Vector3(rx, ry, rz);
        Debug.Log(string.Format("rx={0}  ry={1}  rz={2}", rx, ry, rz));
        spawnedObstacles[i] = Instantiate(obstaclePrefab, moveTo, obstaclePrefab.gameObject.transform.rotation, obstacleGroup.transform);
         Debug.Log(string.Format("Obstaclex={0}  Obstacley={1}  Obstaclez={2}", spawnedObstacles[i].transform.position.x, spawnedObstacles[i].transform.position.y, spawnedObstacles[i].transform.position.z));
        spawnedObstacles[i].transform.position = new Vector3(spawnedObstacles[i].transform.position.x, 0.104f, spawnedObstacles[i].transform.position.z);
    }

    public void IncrementScore()
    {
        score += 1;
        scoreText.text = string.Format("{0}", score);
    }
}
