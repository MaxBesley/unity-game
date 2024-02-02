using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
//using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GetCollected : MonoBehaviour
{
    
    
    public ItemStats weaponStat;
    [SerializeField] InventoryManager manager;
    
    //collect item the player collides with
    private void OnCollisionEnter(Collision collision)
    {
        //find player object to check if the player collided with an item
        Transform collidedTransform = collision.transform;
        Transform playerObjChild = collidedTransform.Find("PlayerObj");
        

        if (playerObjChild != null)
        {
            FindObjectOfType<AudioManager>().Play("WeaponCollect");
            manager.collectItem(weaponStat.getImg(), weaponStat.getMesh(), weaponStat.getWeaponType(), weaponStat, gameObject);
        }


    }
}

