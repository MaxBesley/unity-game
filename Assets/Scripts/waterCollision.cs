using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waterCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            SceneManager.LoadScene("RespawnScene");
    }
}
