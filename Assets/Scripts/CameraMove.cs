using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public Transform cameraPosition;

    void Update()
    {
    if (cameraPosition != null)
     transform.position = cameraPosition.position;

    }
}
