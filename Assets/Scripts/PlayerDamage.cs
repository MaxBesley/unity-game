using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    [SerializeField] private GameObject player;

    [SerializeField] CharacterStats _playerHealthScript;

    void Awake() {
        _playerHealthScript = player.GetComponent<CharacterStats>();
    }

    private bool damageable = true;

    void OnCollisionEnter(Collision collision) {
        // take damage if colliding with enemy
        // implement different conditions for different enemies
        // depending on the type of enemy health reduction will vary
        if (collision.gameObject.tag == "enemy") {
            takeDamage(1);

            //  ^^ replace with something like "takeDamage(BananaMan.hitDamage);"
        }

    }

    void takeDamage(int damage) {
        if (damageable) {
            _playerHealthScript.setHealth(_playerHealthScript.getHealth() - damage);

            //give player a taking damage cooldown of 2 seconds
            damageable = false;
            Invoke("changeDamageable", 2);
        }
    }

    private bool changeDamageable() {

        damageable = true;
        return damageable;
    }
}

