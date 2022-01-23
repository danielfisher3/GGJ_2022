using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthUI : MonoBehaviour
{

    [Tooltip("Enemy Health Slider")]
    [SerializeField] Slider enemyHealthSlider;

    [SerializeField] int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        enemyHealthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        enemyHealthSlider.value = currentHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthSlider.value = currentHealth;
    }

    public void DecreaseEnemyHealth(int decreaseAmount)
    {
        currentHealth -= decreaseAmount;
    }
}
