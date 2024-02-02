using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBridgeItems : MonoBehaviour
{
    public GameObject[] bridgeItems = new GameObject[4];
    void Start()
    {
        Instantiate(bridgeItems[0], new Vector3(Random.Range(260, 300), 8f, Random.Range(195, 235)), Quaternion.identity);
        Instantiate(bridgeItems[1], new Vector3(Random.Range(260, 300), 8f, Random.Range(195, 235)), Quaternion.identity);
        Instantiate(bridgeItems[2], new Vector3(Random.Range(260, 300), 8f, Random.Range(195, 235)), Quaternion.identity);
        Instantiate(bridgeItems[3], new Vector3(Random.Range(260, 300), 8f, Random.Range(195, 235)), Quaternion.identity);
    }

    void Update()
    {
        
    }
}
