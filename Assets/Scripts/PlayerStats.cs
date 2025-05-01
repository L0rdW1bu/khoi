using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public TextMeshProUGUI manaText;
    public Slider manaSlider;
    public GameObject player;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
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
        healthSlider.value = 1;
        mana = maxMana;
        manaSlider.value = 1;
        SetHealthUI();
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
    }
    public void UseSkill(float cost)
    {
        mana -= cost;
        CheckDeath();
        SetManaUI();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHealth();
        SetHealthUI();
    }
    private void SetHealthUI()
    {
        healthSlider.value = CaculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }
    private void SetManaUI()
    {
        manaSlider.value = CaculateManaPercentage();
        manaText.text = Mathf.Ceil(mana).ToString() + " / " + Mathf.Ceil(maxMana).ToString();
    }
    public void ManaCharacter(float healmana)
    {
        mana += healmana;
        CheckOverMana();
        SetManaUI();
    }
    private void CheckOverMana()
    {
        if (mana > maxMana)
        {
            mana = maxMana;
        }
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
            health = 0;
            mana = 0;
            Destroy(player);
        }
    }
    float CaculateHealthPercentage()
    {
        return health / maxHealth;
    }
    float CaculateManaPercentage()
    {
        return mana / maxMana;
    }
}
