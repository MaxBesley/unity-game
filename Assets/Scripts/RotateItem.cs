using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//rotate items when they have not been collected
public class RotateItem : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    
    void Update()
    {
        //rotate items around the y-axis
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }

    
}
