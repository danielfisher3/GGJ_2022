using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthStaminaUI : MonoBehaviour
{
    [Tooltip("Player Health Slider")]
    [SerializeField] Slider pHealthSlider;

    [Tooltip("Player Stamina Slider")]
    [SerializeField] Slider pStaminaSlider;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int maxStamina = 100;

    public int currentHealth;
    public int currentStamina;

    [SerializeField] Text hpText;
    int maxHealthP = 6;
    int currentHP;
    float StaminaTimer;
    private void Awake()
    {
        pHealthSlider.maxValue = maxHealth;
        pStaminaSlider.maxValue = maxStamina;
        currentHealth = maxHealth;
        pHealthSlider.value = currentHealth;
        currentStamina = maxStamina;
        pStaminaSlider.value = currentStamina;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHealthP;
        hpText.text = "x " + currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        pStaminaSlider.value = currentStamina;
        pHealthSlider.value = currentHealth;
        StaminaRegen();
        hpText.text = "x " + currentHP;

        if (currentHealth < maxHealth && currentHP > 0)
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                currentHealth = maxHealth;
                currentHP--;
            }
        }
    }

    public void DecreaseHealth(int decreaseAmount)
    {
        currentHealth -= decreaseAmount;
    }

    public void DecreaseStamina(int decreaseAmount)
    {
        currentStamina -= decreaseAmount;
    }
    public void IncreaseHealth(int increaseAmount)
    {
        currentHealth += increaseAmount;
    }
    public void StaminaRegen()
    {
       if(currentStamina < maxStamina)
        {
            StaminaTimer += Time.deltaTime;
            if(StaminaTimer >= 0.1f)
            {
                currentStamina += 1;
                StaminaTimer = 0;
            }
        }
    }
}
