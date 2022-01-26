using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyAnimEvents : MonoBehaviour
{
    [SerializeField]SkellyAIWanderer skellyAI;
    [SerializeField] Animator skellyAnim;
    public void AttackReqNull()
    {
       foreach(AnimatorControllerParameter p in skellyAnim.parameters)
        {
            skellyAnim.SetBool(p.name, false);
        }

        skellyAI.randomAttackreq = false;
    }
   
}
