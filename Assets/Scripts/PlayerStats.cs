using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public float health;
    public float maxHealth;
    public GameObject player;
    void Awake()
    {
        if(playerStats !=null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
            
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        health = maxHealth;
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHealth();
    }
    private void CheckOverHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(player);
        }
    }
}
