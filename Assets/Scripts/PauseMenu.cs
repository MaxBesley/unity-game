using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool gameIsPaused;
    [SerializeField] GameObject instructions;
    private bool inInstructions;
    [SerializeField] Image crosshair;
    [SerializeField] TimeHandler timer;


    void Start()
    {
        // Initally the pause menu should be off
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // for toggling in/out of pause menu
        if (Input.GetKeyDown(KeyCode.Tab) && !SceneManager.GetActiveScene().Equals("TaskScene") && timer.timeRemain < 295)
        {
            
            if (inInstructions)
            {
                instructions.SetActive(false);
                inInstructions = false;
                PauseGame();
            }
            else if (gameIsPaused)
            {
                ResumeGame();
            }
            
            else
            {   
                PauseGame();
            }

        }
    }

    public void loadInstructions()
    {
        instructions.SetActive(true);
        Time.timeScale = 0f;
        inInstructions = true;

    }

    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("Background");
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        crosshair.GetComponent<Image>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        crosshair.GetComponent<Image>().enabled = true;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
