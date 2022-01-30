using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitDetection : MonoBehaviour
{
    [SerializeField] PlayerHealthStaminaUI playerHealth;
    bool beenHit;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthStaminaUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (beenHit)
        {
            playerHealth.DecreaseHealth(25);
            beenHit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BossAttack" && !beenHit)
        {
            beenHit = true;
        }
    }
}
