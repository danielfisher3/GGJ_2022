using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimEvents : MonoBehaviour
{
    [SerializeField]JuggerNautAI jAI = null;

    public void TurnOffAttacks()
    {
        if (jAI != null)
        {
            jAI.bossAnim.SetBool("Attack1", false);
            jAI.bossAnim.SetBool("Attack2", false);
            jAI.bossAnim.SetBool("Attack3", false);
            jAI.bossAnim.SetBool("Attack4", false);
            jAI.bossAnim.SetBool("Attack5", false);
            jAI.bossAnim.SetBool("Attack6", false);
        }
    }
}