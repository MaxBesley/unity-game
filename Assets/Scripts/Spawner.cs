using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public int numberOfEnemiesToSpawn = 1; // Number of enemies to spawn
    public float spawnInterval = 10f; // Time between spawns
    public bool inTask;
    public GameObject[] items = new GameObject[3];
    public GameObject[] bridgeItems = new GameObject[4];
    public Transform island1marker1;
    public Transform island1marker2;
    public Transform island2marker1;
    public Transform island2marker2;
    public Transform island3marker1;
    public Transform island3marker2;
    public currentIsland currentIsland;

    private Transform currentMarker1;
    private Transform currentMarker2;
    public GameObject[] powerUps = new GameObject[2];
    int index;
    public GameObject[] enemies;

    private void Start() {

        currentMarker1 = island1marker1;
        currentMarker2 = island1marker2;

        InvokeRepeating("spawnEnemies", 0, 10);
        InvokeRepeating("spawnItems", 0, 5);
        setMarkers();
        //spawn powerups and birdge items
        instantiateBridgeItems(island1marker1.position.x, island1marker2.position.x, island1marker1.position.z, island1marker2.position.z);
        instantiateBridgeItems(island2marker1.position.x, island2marker2.position.x, island2marker1.position.z, island2marker2.position.z);
        instantiatePowerUps(island2marker1.position.x, island2marker2.position.x, island2marker1.position.z, island2marker2.position.z);
        instantiatePowerUps(island3marker1.position.x, island3marker2.position.x, island3marker1.position.z, island3marker2.position.z);

    }

    void Update() 
    {
        setMarkers();
    }
    
    private void spawnEnemies() {
        if (!inTask) {
            for (int i = 0; i < 4; i++) {
                // Calculate a spawn position 
                Vector3 spawnPosition = new Vector3(Random.Range(currentMarker1.position.x, currentMarker2.position.x), 7.3f, Random.Range(currentMarker1.position.z, currentMarker2.position.z));
                // Spawn the enemy at the calculated position
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    
    //spawn weapons
    private void spawnItems() {
        if (!inTask) {
            for (int i = 0; i < 3; i++) {
                index = Random.Range(0, 3);
                // Calculate a spawn position 
                Vector3 spawnPosition = new Vector3(Random.Range(currentMarker1.position.x, currentMarker2.position.x), 7.3f, Random.Range(currentMarker1.position.z, currentMarker2.position.z));

                // Spawn the enemy at the calculated position
                Instantiate(items[index], spawnPosition, Quaternion.identity);
            }
        }
    }

    //spawn bridge items
    public void instantiateBridgeItems(float x1, float x2, float z1, float z2) {
        Instantiate(bridgeItems[0], new Vector3(Random.Range(x1, x2), 8f, Random.Range(z1, z2)), Quaternion.identity);
        Instantiate(bridgeItems[1], new Vector3(Random.Range(x1, x2), 8f, Random.Range(z1, z2)), Quaternion.identity);
        Instantiate(bridgeItems[2], new Vector3(Random.Range(x1, x2), 8f, Random.Range(z1, z2)), Quaternion.identity);
        Instantiate(bridgeItems[3], new Vector3(Random.Range(x1, x2), 8f, Random.Range(z1, z2)), Quaternion.identity);
    }

    //spawn powerups 
    public void instantiatePowerUps(float x1, float x2, float z1, float z2)
    {
        Instantiate(powerUps[0], new Vector3(Random.Range(x1, x2), 7f, Random.Range(z1, z2)), Quaternion.Euler(90, 0, 0));
        Instantiate(powerUps[1], new Vector3(Random.Range(x1, x2), 7f, Random.Range(z1, z2)), Quaternion.Euler(90, 0, 0));
    }

    public void setMarkers()
    {
        switch (currentIsland.currentIslandTag)
        {
            case "island1":
                currentMarker1 = island1marker1;
                currentMarker2 = island1marker2;
                break;
            
            case "island2":
                currentMarker1 = island2marker1;
                currentMarker2 = island2marker2;
                break;
           
            case "island3":
                currentMarker1 = island3marker1;
                currentMarker2 = island3marker2;
                break;
        }
    }
}