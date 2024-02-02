using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTask : MonoBehaviour
{
    private int pointsToWin;
    private int currentPoints;
    public GameObject myitems;
    public bool completed;
    public InventoryManager manager;
    public GameObject originalObject;
    public GameObject HUD;
    public GameObject[] enemies;
    public GameObject spawner;
    public GameObject crosshair;
    public GameObject sceneManager;


    void Start()
    {
        //access disabled objects
        showCursor(true);
        pointsToWin = myitems.transform.childCount;
        originalObject = GameObject.FindGameObjectWithTag("MainCamera");
        spawner = GameObject.FindGameObjectWithTag("spawner");
        HUD = GameObject.Find("HUD");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        crosshair = GameObject.FindGameObjectWithTag("crosshair");
        sceneManager = GameObject.Find("SceneManager");
    }

    void Update()
    {
        //return to gamescene when task is complete
        if(currentPoints >= pointsToWin)
        {
            foreach(GameObject obj in enemies)
            {
                obj.GetComponent<EnemyMovement>().enabled = true;
            }
            FindObjectOfType<AudioManager>().Play("Shimmer");
            showCursor(false);
            spawner.GetComponent<Spawner>().inTask = false;
            HUD.GetComponent<PauseMenu>().enabled = true;
            originalObject.GetComponent<Camera>().enabled = true;
            originalObject.GetComponent<PlayerCam>().enabled = true;
            crosshair.GetComponent<Image>().enabled = true;
            sceneManager.GetComponent<ToTaskTransition>().enabled = true;
            sceneManager.GetComponent<ToTaskTransition>().bridgeCreatedText.SetActive(true);
            sceneManager.GetComponent<ToTaskTransition>().bridgeCreated = true;
            Invoke("disableBridgeCreated", 3f);
            sceneManager.GetComponent<ToTaskTransition>().taskCompleted = true;
            SceneManager.UnloadSceneAsync("TaskScene");
        }
    }
    public void AddPoints()
    {
        currentPoints++;
    }

    //show cursor when in task
    public void showCursor(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void disableBridgeCreated()
    {
        Debug.Log("disables");
        sceneManager.GetComponent<ToTaskTransition>().bridgeCreatedText.SetActive(false);
        sceneManager.GetComponent<ToTaskTransition>().bridgeCreated = false;

    }
}
