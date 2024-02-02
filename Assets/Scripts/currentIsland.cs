using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentIsland : MonoBehaviour
{
    static public string currentIslandTag = "island1";
    static public string oldIslandTag = "island1";
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Player")) {
            if (!currentIslandTag.Equals(gameObject.tag))
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
                {
                    if (enemy.name.Equals("Zombie1(Clone)"))
                    {
                        Destroy(enemy);
                    }
                }
            }
            currentIslandTag = gameObject.tag;
        }
    }
}
