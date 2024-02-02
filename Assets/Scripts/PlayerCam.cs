using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCam : MonoBehaviour
{
    private static float mouseSensitivity = 300; // default value

	private Vector2 _rotation = Vector2.zero;
    private const float yRotationLimit = 90f;
	private const string xAxis = "Mouse X";
	private const string yAxis = "Mouse Y";


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<AudioManager>().Play("Background");
        FindObjectOfType<AudioManager>().Play("Horror");
        FindObjectOfType<AudioManager>().Play("Waves");


    }

    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw(xAxis) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw(yAxis) * mouseSensitivity * Time.deltaTime;

        // set rotation and restrict y rotation
		_rotation.x += mouseX;
		_rotation.y += mouseY;
		_rotation.y = Mathf.Clamp(_rotation.y, -yRotationLimit, yRotationLimit);

        // rotate player camera
        Quaternion xRot = Quaternion.AngleAxis(_rotation.x, Vector3.up);
		Quaternion yRot = Quaternion.AngleAxis(_rotation.y, Vector3.left);
		transform.localRotation = xRot * yRot;
        // or use this line instead...
        // transform.localRotation = Quaternion.Euler(-_rotation.y, _rotation.x, 0f);
    }

    public static void SetMouseSensitivity(float value)
    {
        PlayerCam.mouseSensitivity = value;
    }
}
