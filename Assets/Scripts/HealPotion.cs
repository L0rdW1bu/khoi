using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public float heal = 20;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerStats.playerStats.health != PlayerStats.playerStats.maxHealth)
        {
            PlayerStats.playerStats.HealCharacter(heal);
            Destroy(gameObject);
        }
    }

}
