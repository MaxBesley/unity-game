using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const string Tag = "GameManager";
    public const string GameSceneName = "GameScene";
    public const string MenuSceneName = "StartScene";


    private void Awake()
    {
        // singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PauseMenu.gameIsPaused = false;
    }

}
