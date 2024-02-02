using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    void Start()
    {
        UnlockMouse();
    }
    public void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Start");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToStartScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
