using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFAnimEvent : MonoBehaviour
{
    Player_Controller pcontrol;

    private void Start()
    {
        pcontrol = GameObject.FindObjectOfType<Player_Controller>();
    }
    public void SetBoolForFFOff()
    {
        pcontrol.forceDeployed = true;
    }
}
