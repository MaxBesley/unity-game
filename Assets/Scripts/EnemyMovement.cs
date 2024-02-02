using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Animator anim;
    public float speed;
    private bool enemySound;
    public bool moving = true;
    public int runSpeedMultiplier;



    void Start()
    {
        anim = GetComponent<Animator>();
        if (Random.Range(1, 2) == 1)
        {
            pursuePlayer();
        }

    }
    void Update()
    {
        if (player != null)
        {
            if (moving && player != null)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, speed * Time.deltaTime);
                enemy.LookAt(player);
            }
            else
            {
                {
                    GetComponent<Rigidbody>().AddForce(-transform.forward, ForceMode.Impulse);
                    Invoke("enableMove", 0.05f);
                }
            }
            // Play sound once per enemy if zombie is close to player
            if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 7 && !enemySound)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
                anim.SetBool("Idle", false);
                speed = speed * runSpeedMultiplier;
                moving = true;
                // Reduce overload of zombie noises
                if (Random.Range(1, 3) == 1)
                {
                    FindObjectOfType<AudioManager>().Play("Zombie");
                }
                enemySound = true;
            }
        }
    }
    // Zombie has attacked player
    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(playAndStopAnimation());
        if (collision.transform.name == "Player")
        {
            moving = false;
        }
        
    }
    private IEnumerator playAndStopAnimation()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", true); 
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.SetBool("Walking", true);
        anim.SetBool("Attacking", false);
        moving = true;
    }
    // Zombie will start walking towards player
    void pursuePlayer()
    {
        anim.SetBool("Walking", true);
        anim.SetBool("Running", false);
        anim.SetBool("Idle", false);
        moving = true;
    }
    void enableMove()
    {
        moving = true;
    }
}

