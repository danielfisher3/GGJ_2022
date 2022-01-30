using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] Collider divineSword, evilSword;
    [SerializeField]Player_Controller control;

    private void Start()
    {
        
    }
    public void TurnOnSwordCollider()
    {
        if (control.demonForm)
        {
            evilSword.enabled = true;
        }

        if (!control.demonForm)
        {
            divineSword.enabled = true;
        }
    }
    public void TurnOffSwordCollider()
    {
        if (control.demonForm)
        {
            evilSword.enabled = false;
        }
        if (!control.demonForm)
        {
            divineSword.enabled = false;
        }
    }
}
