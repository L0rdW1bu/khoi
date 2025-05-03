using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject healPotionPrefab;
    public GameObject manaPotionPrefab;

    [Range(0f, 1f)] public float dropChance = 0.2f; // 20% drop chance
    [Range(0f, 1f)] public float manaDropChance = 0.5f; // 50% of drops are mana
    void Start()
    {
        health = maxHealth;   
    }
    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        healthBarSlider.value = CaculateHealthPercentage();
        health -= damage;
        CheckDeath();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHealth();
        healthBarSlider.value = CaculateHealthPercentage();

    }
    private void CheckOverHealth()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private float CaculateHealthPercentage()
    {
        return health / maxHealth;
    }
    private void CheckDeath()
    {
        if(health <=0)
        {
            Destroy(gameObject);
            TryDropPotion();
        }
    }
    void TryDropPotion()
    {
        float roll = Random.value;
        if (roll <= dropChance)
        {
            GameObject potionToDrop = (Random.value <= manaDropChance) ? manaPotionPrefab : healPotionPrefab;
            Instantiate(potionToDrop, transform.position, Quaternion.identity);
        }
    }
}
