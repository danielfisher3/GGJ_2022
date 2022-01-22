using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
  
    [Tooltip("Animator for Divinity")] [SerializeField] Animator divineAnim;
    [Tooltip("Movement Speed Variable not running")] [SerializeField] float walkSpeed = 3.0f;
    [Tooltip("Minimum UP Down Clamp Rotation")] [SerializeField] float minClamp;
    [Tooltip("Maximum UP Down Clamp Rotation")] [SerializeField] float maxClamp;
    [SerializeField] GameObject pCamera;
    bool walk;
    float clampedX;
    float rotY;
   
    private void Update()
    {
        divineAnim.SetBool("Walk",walk);
        clampedX -= Input.GetAxis("Mouse Y");
        rotY += Input.GetAxis("Mouse X");
        clampedX = Mathf.Clamp(clampedX, minClamp, maxClamp);
        pCamera.transform.localEulerAngles = new Vector3(clampedX, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotY, 0);

        
    }

    void LateUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputVertical >= 0.01f)
        {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);
            walk = true;
        }
        else if (inputVertical <= -0.01f)
        {
            transform.Translate(0, 0, -walkSpeed * Time.deltaTime);
            walk = true;
        }
        else
        {
            
            inputVertical = 0;
            walk = false;
        }
       
    }
}
