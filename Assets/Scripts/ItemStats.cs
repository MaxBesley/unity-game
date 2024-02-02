using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//keep track of variables and methods pertaining to items
public class ItemStats : MonoBehaviour
{
    [SerializeField] int weaponDamage;
    [SerializeField] int weaponDurability;
    [SerializeField] Sprite inventoryImg;
    [SerializeField] Mesh weaponMesh;
    [SerializeField] string weaponType;
    [SerializeField] float attackSpeed;
    private int inventoryIndex;
    [SerializeField] GameObject test;
    public InventoryManager manager;
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text dur1;
    [SerializeField] TMP_Text dur2;
    [SerializeField] TMP_Text dur3;



    public void setIndex(int index)
    {
        inventoryIndex = index;
    }

    public float getAttackSpeed()
    {
        return attackSpeed;
    }

    public int getIndex()
    {
        return inventoryIndex;
    }
    
    public string getWeaponType()
    {
        return weaponType;
    }

    public void reduceDurability(string weaponName)
    {
        weaponDurability--;
        Debug.Log(weaponName);
        if (weaponDurability != 0)
        {
            switch (gameObject.name)
            {

                case "Axe(Clone)":
                    dur1.text = weaponDurability + "/1";
                    break;
                case "Hammer(Clone)":
                    dur2.text = weaponDurability + "/3";
                    break;
                case "Spear(Clone)":
                    dur3.text = weaponDurability + "/10";
                    break;
            }
        }

        else
        {
            switch (gameObject.name)
            {

                case "Axe(Clone)":
                    dur1.enabled = false;
                    break;
                case "Hammer(Clone)":
                    dur2.enabled = false;
                    break;
                case "Spear(Clone)":
                    dur3.enabled = false;
                    break;
            }
        }
        if(weaponDurability == 0)
        {
            manager.disableImg(inventoryIndex);
        }
    }
    public void setDamage(int weaponDamage) {
        this.weaponDamage = weaponDamage;
    }
    public int getDamage() {
        return weaponDamage;
    }
    public void setDurability(int weaponDurability) {
        this.weaponDurability = weaponDurability;
    }
    public int getDurability() {
        return weaponDurability;
    }

    public Sprite getImg()
    {
        return inventoryImg;
    }

    public Mesh getMesh()
    {
        return weaponMesh;
    }



}
