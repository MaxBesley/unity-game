using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadSpawnText : MonoBehaviour
{
    [SerializeField] GameObject spawnText;
    void Start()
    {
        Invoke("removeSpawnText", 5f);
    }
    public void removeSpawnText()
    {
        spawnText.SetActive(false);
    }
}
