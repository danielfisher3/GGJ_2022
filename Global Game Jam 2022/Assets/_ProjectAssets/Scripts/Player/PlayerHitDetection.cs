using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitDetection : MonoBehaviour
{
    [SerializeField] PlayerHealthStaminaUI playerHealth;
    bool beenHitBoss,beenHitEnemy;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthStaminaUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (beenHitBoss)
        {
            playerHealth.DecreaseHealth(25);
            beenHitBoss = false;
        }

        if (beenHitEnemy)
        {
            playerHealth.DecreaseHealth(5);
            beenHitEnemy = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BossAttack" && !beenHitBoss)
        {
            beenHitBoss = true;
        }
        if(other.gameObject.tag == "EnemyAttack" && !beenHitEnemy)
        {
            beenHitEnemy = true;
        }
    }
}
