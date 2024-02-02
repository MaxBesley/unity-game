using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    private int damage;
    [SerializeField] EnemyHealthBar healthbar;
    private int maxHealth;
    public Animator anim;
    public GameObject enemy;
    private bool dead;
    private bool playDead;


    private void Start()
    {
        maxHealth = health;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isDead();
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void isDead()
    {
        if (health <= 0)
        {
            // change enemy layer to ignoreCollision to not damage player after death
            dead = true;
            if (dead && !playDead)
            {
                playDeathSound();
            }
            enemy.layer = 3;
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Died", true);
            Destroy(gameObject, 3.0f);
            
        }
    }
    public void playDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("ZombieDeath");
        playDead = true;
        ScoreManager.instance.GetPoints();
    }

    public void takeDamage(int damage)
    {
        health = health - damage;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getHealth()
    {
        return health;
    }
    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getDamage()
    {
        return damage;
    }



}
