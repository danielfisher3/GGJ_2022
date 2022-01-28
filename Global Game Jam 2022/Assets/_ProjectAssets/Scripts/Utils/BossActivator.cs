using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    BossManager bManager;
    bool bossActivated = false;

    [SerializeField] bool jBoss;
    [SerializeField] bool aBoss;
    [SerializeField] bool oBoss;
    [SerializeField] bool mBoss;

    private void Start()
    {
        bManager = FindObjectOfType<BossManager>();
    }


    private void Update()
    {
       if(bossActivated && jBoss)
        {
            bManager.juggernautActivated = true;
            bossActivated = false;
        }
        else if (bossActivated && aBoss)
        {
            bManager.angelActivated = true;
            bossActivated = false;
        }
        else if (bossActivated && oBoss)
        {
            bManager.ogreActivated = true;
            bossActivated = false;
        }
        else
        {
            if (bossActivated && mBoss)
            {
                bManager.mutantActivated = true;
                bossActivated = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bossActivated = true;
        }
    }
}
