using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimEvents : MonoBehaviour
{
    [SerializeField]JuggerNautAI jAI = null;
    [SerializeField] AngelAI aAI = null;
    [SerializeField] OgreAI oAI = null;
    [SerializeField] MutantAI mAI = null;
    [SerializeField] DeathKingAI dkAI = null;
    public bool deathKingDead;
    [SerializeField] Collider attackCollider1 = null,attackCollider2 = null;
    
    public void TurnOnColliders()
    {
        attackCollider1.enabled = true;
        attackCollider2.enabled = true;
    }
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
           
            attackCollider1.enabled = false;
            attackCollider2.enabled = false;
        }
        if(aAI != null)
        {
            aAI.bossAnim.SetBool("Attack1", false);
            aAI.bossAnim.SetBool("Attack2", false);
            aAI.bossAnim.SetBool("Attack3", false);

            attackCollider1.enabled = false;
            attackCollider2.enabled = false;
        }
        if(oAI != null)
        {
            oAI.bossAnim.SetBool("Attack1", false);
            oAI.bossAnim.SetBool("Attack2", false);
            oAI.bossAnim.SetBool("Attack3", false);

            attackCollider1.enabled = false;
            attackCollider2.enabled = false;
        }
        if(mAI != null)
        {
            mAI.bossAnim.SetBool("Attack1", false);
            mAI.bossAnim.SetBool("Attack2", false);
            mAI.bossAnim.SetBool("Attack3", false);

            attackCollider1.enabled = false;
            attackCollider2.enabled = false;
        }
        if(dkAI != null)
        {
            dkAI.bossAnim.SetBool("Attack1", false);
            dkAI.bossAnim.SetBool("Attack2", false);
            dkAI.bossAnim.SetBool("Attack3", false);
            dkAI.bossAnim.SetBool("Attack4", false);
            dkAI.bossAnim.SetBool("Attack5", false);
            dkAI.bossAnim.SetBool("Attack6", false);
            dkAI.bossAnim.SetBool("Attack7", false);

            attackCollider1.enabled = false;
            attackCollider2.enabled = false;
        }
    }
    public void TurnOffBeenHit()
    {
        if(oAI != null)
        {
            oAI.bossAnim.SetBool("BeenHit", false);
        }
        if(aAI != null)
        {
            aAI.bossAnim.SetBool("BeenHit", false);
        }
        if(jAI != null)
        {
            jAI.bossAnim.SetBool("BeenHit", false);
        }
        if(mAI != null)
        {
            mAI.bossAnim.SetBool("BeenHit", false);
        }
        if(dkAI != null)
        {
            dkAI.bossAnim.SetBool("BeenHit", false);
        }
    }
}
