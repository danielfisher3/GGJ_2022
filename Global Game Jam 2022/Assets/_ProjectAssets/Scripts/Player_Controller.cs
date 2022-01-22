using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    #region Global Variables
    [Tooltip("Animator for Divinity")] [SerializeField] Animator divineAnim;
    [Tooltip("Movement Speed Variable not running")] [SerializeField] float walkSpeed = 3.0f;
    [Tooltip("Movement Speed Variable for running")] [SerializeField] float runSpeed = 6.0f;
    [Tooltip("Minimum UP Down Clamp Rotation")] [SerializeField] float minClamp;
    [Tooltip("Maximum UP Down Clamp Rotation")] [SerializeField] float maxClamp;
    [Tooltip("StartPoint for Ground check, bottom of feet")] [SerializeField] Transform groundcheckLineStart;
    [Tooltip("EndPoint for Ground check, how far below feet do you want to check?")] [SerializeField] Transform groundcheckLineStop;
    [SerializeField] GameObject pCamera;
    bool walk;
    float clampedX;
    float rotY;
    #endregion

    #region Unity Native
    private void Update()
    {
        divineAnim.SetBool("Walk", walk);
        
        PlayerAndCameraRotation();
        

    }

    

    void LateUpdate()
    {
        WalkingMovement();

    }
    #endregion

    #region Custom
    private void PlayerAndCameraRotation()
    {
        clampedX -= Input.GetAxis("Mouse Y");
        rotY += Input.GetAxis("Mouse X");
        clampedX = Mathf.Clamp(clampedX, minClamp, maxClamp);
        pCamera.transform.localEulerAngles = new Vector3(clampedX, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotY, 0);
    }
    private void WalkingMovement()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputVertical >= 0.01f && !Input.GetKey(KeyCode.LeftShift))
        {
            divineAnim.SetBool("Run", false);
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);
            walk = true;
           
        }
        else if (inputVertical >= 0.01f && Input.GetKey(KeyCode.LeftShift))
        {
            walk = false;
            divineAnim.SetBool("Run", true);
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
           
           
        }
        else if (inputVertical <= -0.01f)
        {
            divineAnim.SetBool("Run", false);
            transform.Translate(0, 0, -walkSpeed * Time.deltaTime);
            walk = true;
            

        }
        else
        {
            divineAnim.SetBool("Run", false);
            inputVertical = 0;
            walk = false;
          
        }


    }

    bool PlayerGrounded()
    {
        bool grounded = Physics.Linecast(groundcheckLineStart.position, groundcheckLineStop.position, 1 << LayerMask.NameToLayer("ground"));
        return grounded;
    }
    #endregion
}
