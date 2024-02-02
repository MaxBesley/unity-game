using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToTaskTransition : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject orientation;
    [SerializeField] InventoryManager manager;
    private GameObject[] enemies;
    [SerializeField] GameObject enemyParent;
    [SerializeField] GameObject itemParent;
    [SerializeField] Spawner spawner;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask tableLayer;
    [SerializeField] Image crosshair;
    [SerializeField] GameObject canCraftText;
    [SerializeField] GameObject cantCraftText;
    public GameObject bridgeCreatedText;
    public bool bridgeCreated;
    public bool taskCompleted;
    
    void Update()
    {
        if (isAtTable() && manager.bridgeItemAmount < 4 && !bridgeCreated && !bridgeCreated)
        {
            cantCraftText.SetActive(true);
        }

        else if(isAtTable() && manager.bridgeItemAmount == 4 && !bridgeCreated)
        {
            cantCraftText.SetActive(false);
            canCraftText.SetActive(true);
        }
        else
        {
            cantCraftText.SetActive(false);
            canCraftText.SetActive(false);
        }

        if (taskCompleted)
        {
            Invoke("disableBridgeCreated", 3f);
        }

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (manager.bridgeItemAmount == 4 && isAtTable() && Input.GetKeyDown("e"))
        {
            spawner.inTask = true;
            //re-enable scene manager
            manager.bridges[manager.bridgeIndex].SetActive(true);
            manager.bridgeIndex++;

            crosshair.enabled = false;
            cantCraftText.SetActive(false);
            canCraftText.SetActive(false);

            foreach (GameObject obj in enemies)
            {
                obj.GetComponent<EnemyMovement>().enabled = false;
            }
            
            this.enabled = false;
            
            for (int i = 0; i < manager.bridgeItemAmount; i++)
            {
                manager.bridgeItems[i].enabled = false;
            }
            manager.bridgeItemAmount = 0;
            
            SceneManager.LoadScene("TaskScene", LoadSceneMode.Additive);
            pauseMenu.GetComponent<PauseMenu>().enabled = false;
            playerCam.GetComponent<PlayerCam>().enabled = false; 
            playerCam.GetComponent<Camera>().enabled = false;
        }
    }

    public bool isAtTable()
    {
        return Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, 1.5f, tableLayer);
    }
    public void disableBridgeCreated()
    {
        Debug.Log("disables");
        bridgeCreatedText.SetActive(false);
        bridgeCreated = false;
        taskCompleted = false;
    }



}
