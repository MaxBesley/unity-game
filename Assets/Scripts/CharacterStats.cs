using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject character;
    public int health;
    public int heartAmount;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool heartBeat = false;


    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (health > heartAmount)
            {
                health = heartAmount;
            }

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < heartAmount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (!heartBeat)
        {
            if (health <= 2)
            {
                heartBeat = true;
                FindObjectOfType<AudioManager>().Play("HeartBeat");

            }
        }
        if (health < 1)
        {
            Time.timeScale = 1f;
            Debug.Log("Player is dead");
            SceneManager.LoadScene("RespawnScene");
        }
    }
    public int getHealth() { return health; }
    public void setHealth(int health) { this.health = health; }

    public int getHeartAmount() { return heartAmount; }
    public void setHeartAmount(int heartAmount) { 
        this.heartAmount = heartAmount;
        this.health = heartAmount;
    }

}
