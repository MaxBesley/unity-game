using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeItemCollect : MonoBehaviour
{
    public InventoryManager manager;
    public CharacterStats playerStats;
    private void OnCollisionEnter(Collision collision)
    {
        //find player object to check if the player collided with an item
        Transform collidedTransform = collision.transform;
        Transform playerObjChild = collidedTransform.Find("PlayerObj");


        if (playerObjChild != null)
        {
            
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Collect");
            switch (gameObject.name)
            {
                case "Rock(Clone)":
                    manager.collectBridgeItem(0);
                    break;
                
                case "Rope(Clone)":
                    manager.collectBridgeItem(1);
                    break;
                
                case "Wood(Clone)":
                    manager.collectBridgeItem(2);
                    break;
                
                case "Nail(Clone)":
                    manager.collectBridgeItem(3);
                    break;

                case "Health(Clone)":

                    playerStats.setHeartAmount(playerStats.getHeartAmount() + 1);
                    break;

                case "Damage(Clone)":
                    ItemStats[] itemScripts = FindObjectsOfType<ItemStats>();
                    //increase damage of equipped weapons
                    foreach (GameObject weapon in manager.collectedItems)
                    {
                        if (weapon != null)
                        {
                            weapon.GetComponent<ItemStats>().setDamage(weapon.GetComponent<ItemStats>().getDamage() + 1);
                        }
                    }
                    //increase damage of all weapons on the field
                    foreach (ItemStats stats in itemScripts)
                    {
                        stats.setDamage(stats.getDamage() + 1);
                    }
                    break;
            }
        }


    }
}
