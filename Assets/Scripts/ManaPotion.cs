using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    public float heal = 20;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerStats.playerStats.mana != PlayerStats.playerStats.maxMana)
        {
            PlayerStats.playerStats.ManaCharacter(heal);
            Destroy(gameObject);
        }
    }

}
