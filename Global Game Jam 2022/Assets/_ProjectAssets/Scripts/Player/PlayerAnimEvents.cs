using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] GameObject divineSwordNoC, divineSwordC, evilSwordNoC,evilSwordC;
    [SerializeField]Player_Controller control;
    Animator pAnim;

    private void Start()
    {
        pAnim = GetComponent<Animator>();
    }
    public void TurnOnSwordCollider()
    {
        if (control.demonForm)
        {
            evilSwordC.SetActive(true);
            evilSwordNoC.SetActive(false);
        }

        if (!control.demonForm)
        {
            divineSwordC.SetActive(true);
            divineSwordNoC.SetActive(false);
           
        }
    }
    public void TurnOffSwordCollider()
    {
        pAnim.SetBool("LAttack1", false);
        pAnim.SetBool("HAttack1", false);
        control.swapSwordColliders = true;
    }
}
