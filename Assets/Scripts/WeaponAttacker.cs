using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAttacker : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] InventoryManager manager;
    private bool canAttack = true;
    private float attackCooldown;
    [SerializeField] Animator animator;
    [SerializeField] Image crosshair;
    
    private void Awake()
    {
        animator = weapon.GetComponent<Animator>();
    }

    void Update()
    {
        hoveringOverEnemy();
        //get cooldown of the currently equipped weapon
        if (manager.getCurrentWeapon() != null)
        {
            attackCooldown = manager.getCurrentWeapon().GetComponent<ItemStats>().getAttackSpeed();
        }
        //fix cooldown delay when swapping weapons
        if (manager.getOldWeapon() != manager.getCurrentWeapon())
        {
            canAttack = true;
        }
        //attack with in hand weapon
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            //attack if there is a weapon in hand
            if (manager.getCurrentWeapon() != null)
            {
                attack();
                Invoke(nameof(reduce), animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                Invoke(nameof(resetAttack), attackCooldown);
                //start weapon swinging animation
                StartCoroutine(playAndStopAnimation());
                canAttack = false;
            }
        }
    }

    public void reduce()
    {
        manager.getCurrentWeapon().GetComponent<ItemStats>().reduceDurability(manager.getCurrentWeapon().name);
    }

    //allow the player to attack again
    public void resetAttack()
    {
        canAttack = true;
    }

    //stop and start logic of weapon swinging animations
    private IEnumerator playAndStopAnimation()
    {
        animator.SetBool("Swinging", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        /*if (manager.getCurrentWeapon().GetComponent<ItemStats>().getIsDestroyed())
        {
            manager.disableImg(manager.getCurrentWeapon().GetComponent<ItemStats>().getIndex());
        }*/
        animator.SetBool("Swinging", false);
    }

    public float attackSpeed = 0f;
    public float attackDelay = 0.5f;
    public float attackDistance = 2.5f;
    public LayerMask enemyLayer;

    public void attack()
    {
        animator.SetBool("Swinging", true);
        attackCast();
        switch (manager.getCurrentWeapon().GetComponent<ItemStats>().getWeaponType())
        {
            case "axe":
                FindObjectOfType<AudioManager>().Play("Electricity");
                FindObjectOfType<AudioManager>().Play("Axe");

                break;
            case "hammer":
                FindObjectOfType<AudioManager>().Play("LightningHit");
                break;
            case "spear":
                FindObjectOfType<AudioManager>().Play("WeaponSwing");
                break;
        }
    }

    //damage the enemy we are looking at
    public void attackCast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackDistance, enemyLayer))
        {
            hit.collider.GetComponent<EnemyStats>().takeDamage(manager.getCurrentWeapon().GetComponent<ItemStats>().getDamage());
            hit.collider.transform.Find("Canvas/Health Bar").GetComponent<EnemyHealthBar>().updateBar();
            hit.collider.GetComponent<EnemyMovement>().moving = false;
        }
    }

    public void hoveringOverEnemy()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackDistance, enemyLayer))
        {
            crosshair.color = Color.red;
        } 

        else
        {
            crosshair.color = Color.white;
        }
    }




}