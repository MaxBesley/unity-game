using UnityEngine;
using UnityEngine.SceneManagement;

public class Preserve : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    
}
