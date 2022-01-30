using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    [SerializeField] enemyHealthUI enemyHealth;
    public bool hasbeenhit = false;
    Player_Controller pcontrol;


    private void Start()
    {
        pcontrol = GameObject.FindObjectOfType<Player_Controller>();
    }
    private void Update()
    {
        if (hasbeenhit)
        {
            enemyHealth.DecreaseEnemyHealth(25);
            pcontrol.sword1.enabled = false;
            hasbeenhit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasbeenhit) return;
        if(other.tag == "PlayerSword")
        {
            hasbeenhit = true;
        }
    }
}
