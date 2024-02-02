using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the main camera's transform.
    public Vector3 weaponOffset = new Vector3(0.5f, -0.5f, 1f); // Adjust as needed.

    private void Update()
    {
        
        if (cameraTransform != null)
        {
            // Match the weapon's position and rotation to the camera.
            transform.position = cameraTransform.position + cameraTransform.TransformDirection(weaponOffset);
            transform.rotation = cameraTransform.rotation;
        }
    }
}