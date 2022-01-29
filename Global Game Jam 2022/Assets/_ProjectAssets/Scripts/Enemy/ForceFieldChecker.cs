using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldChecker : MonoBehaviour
{
    Collider attackCollider;
    [SerializeField] bool evil;
    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            if(other.gameObject.name == "Divine" && evil)
            {
                attackCollider.enabled = false;
            }
            else if(other.gameObject.name == "Evil" && !evil)
            {
                attackCollider.enabled = false;
            }
        }
    }
}
