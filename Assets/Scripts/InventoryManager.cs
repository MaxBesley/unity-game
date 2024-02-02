using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

//class to display and modify items in the inventory slots
public class InventoryManager : MonoBehaviour
{

    public GameObject[] collectedItems = new GameObject[4];
    [SerializeField] Image[] slots;
    [SerializeField] Image[] borders;
    [SerializeField] GameObject weapon;
    [SerializeField] Mesh[] weaponMesh;
    private Mesh[] meshList = new Mesh[4];
    private MeshFilter meshStorer;
    public static int itemAmt = 0;
    private bool hasAxe = false;
    private bool hasHammer = false;
    private bool hasSpear = false;
    private string invKeyPressed = "nothingPressed";
    public GameObject currentWeapon;
    public ItemStats currentStat;
    public ItemStats[] statList = new ItemStats[4];
    public GameObject oldWeapon;
    public bool hasTaskItems = true;
    public bool taskCompleted;
    public bool inTask;
    public int bridgeItemAmount;
    public GameObject test;
    [SerializeField] TMP_Text dur1;
    [SerializeField] TMP_Text dur2;
    [SerializeField] TMP_Text dur3;

    private void Start()
    {
        meshStorer = weapon.GetComponent<MeshFilter>();
        dur1.enabled = false;
        dur2.enabled = false;
        dur3.enabled = false;
    }

    void Update()
    {
        //keep track of last pressed inventory key
        invKeyPressed = keyPressed(invKeyPressed);
        unhighlightItem();
        equipItem(invKeyPressed);
        
    }

    public GameObject getCurrentWeapon()
    {
        return currentWeapon;
    }

    public GameObject getOldWeapon()
    {
        return oldWeapon;
    }

    //remove red border around previously equipped item
    public void unhighlightItem()
    {
        if (Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3"))
        {
            foreach (Image border in borders)
            {
                border.color = Color.white;
            }
        }
    }

    //equip the item corresponding to the inventory key pressed
    public void equipItem(string invKeyPressed)
    {
        //create red border, show equipped item in hand and track currently equipped item
        switch (invKeyPressed)
        {
            case "1":
                borders[0].color = Color.red;
                meshStorer.mesh = meshList[0];
                oldWeapon = currentWeapon;
                currentWeapon = collectedItems[0];
                break;

            case "2":
                borders[1].color = Color.red;
                meshStorer.mesh = meshList[1];
                oldWeapon = currentWeapon;
                currentWeapon = collectedItems[1];
                break;

            case "3":
                borders[2].color = Color.red;
                meshStorer.mesh = meshList[2];
                oldWeapon = currentWeapon;
                currentWeapon = collectedItems[2];
                break;
        }
    }

    //determine which inventory slot is selected
    public string keyPressed(string lastPressed)
    {
        if (Input.GetKeyDown("1"))
        {
            return "1";
        }
        if (Input.GetKeyDown("2"))
        {
            return "2";
        }
        if (Input.GetKeyDown("3"))
        {
            return "3";
        }
        return lastPressed;
    }

    //called when item is collected
    public void collectItem(Sprite itemImg, Mesh weaponMesh, string weaponType, ItemStats weaponStats, GameObject weapon)
    {
        //COULD GET RID OF SWTICH AND JUST USE AN AND STATEMENT WITH THE WEAPON TYPE
        //adjust HUD according to weapon types, make an equipped weapon type not collectable until it breaks
        switch (weaponType)
        {
            case "axe":
                if (hasAxe == false)
                {
                    adjustHUD(itemImg, weaponMesh, weaponStats, weapon, 0);
                    dur1.enabled = true;
                    hasAxe = true;
                }
                break;

            case "hammer":
                if (hasHammer == false)
                {
                    adjustHUD(itemImg, weaponMesh, weaponStats, weapon, 1);
                    dur2.enabled = true;
                    hasHammer = true;
                }
                break;

            case "spear":
                if (hasSpear == false)
                {
                    adjustHUD(itemImg, weaponMesh, weaponStats, weapon, 2);
                    dur3.enabled = true;
                    hasSpear = true;
                }
                break;
        }

    }

    //update the HUD when collecting an item
    public void adjustHUD(Sprite itemImg, Mesh weaponMesh, ItemStats weaponStats, GameObject weapon, int index)
    {
        weapon.SetActive(false);
        slots[index].sprite = itemImg;
        slots[index].enabled = true;
        meshList[index] = weaponMesh;
        collectedItems[index] = weapon;
        weapon.GetComponent<ItemStats>().setIndex(index);
    }

    //remove item from HUD when its durability is zero
    public void disableImg(int index)
    {
        slots[index].enabled = false;
        meshList[index] = null;
        //allow broken item type to be picked up again
        switch (index)
        {
            case 0:
                hasAxe = false;
                currentWeapon = null;
                collectedItems[0] = null;
                break;
            case 1:
                hasHammer = false;
                currentWeapon = null;
                collectedItems[1] = null;
                break;
            case 2:
                hasSpear = false;
                currentWeapon = null;
                collectedItems[2] = null;
                break;
            case 3:
                break;
        }
    }

    public Image[] bridgeItems = new Image[4];
    public GameObject[] bridges = new GameObject[1];
    public int bridgeIndex = 0;
    
    public void collectBridgeItem(int index)
    {
        bridgeItemAmount++;
        bridgeItems[index].enabled = true;
    }
}