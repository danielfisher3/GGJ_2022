using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    Animator enemyAnim;

    private void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }
    public void TurnOffBeenHit()
    {
        enemyAnim.SetBool("BeenHit", false);
    }
}
