using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyRecieveDamage2 : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject healPotionPrefab;
    public GameObject manaPotionPrefab;
    public Animator animator;  

    [Range(0f, 1f)] public float dropChance = 0.2f; // 20% drop chance
    [Range(0f, 1f)] public float manaDropChance = 0.5f; // 50% of drops are mana

    public float deathAnimationTime = 1f; 

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();  
    }

    public void DealDamage(float damage)
    {
        health -= damage;  
        healthBar.SetActive(true);  
        healthBarSlider.value = CalculateHealthPercentage();  
        CheckDeath();  
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");  
               
            }

            
            StartCoroutine(WaitForDeathAnimation());  
            TryDropPotion();  
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        
        yield return new WaitForSeconds(deathAnimationTime);

     
        Destroy(gameObject);
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

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}
