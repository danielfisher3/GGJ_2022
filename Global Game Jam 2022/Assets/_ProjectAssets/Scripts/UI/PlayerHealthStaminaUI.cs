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

    [SerializeField] int currentHealth;
    [SerializeField] int currentStamina;


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
        StartCoroutine(StaminaRegenerator());
    }

    // Update is called once per frame
    void Update()
    {
        pStaminaSlider.value = currentStamina;
        pHealthSlider.value = currentHealth;
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
    IEnumerator StaminaRegenerator()
    {
        while(currentStamina != maxStamina)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            currentStamina += 1;
        }
    }
}
