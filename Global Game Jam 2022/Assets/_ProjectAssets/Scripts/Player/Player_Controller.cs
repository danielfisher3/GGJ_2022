using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    #region Global Variables
    [Tooltip("Animator for Divinity")] [SerializeField] Animator divineAnim;
    [Tooltip("Movement Speed Variable not running")] [SerializeField] float walkSpeed = 3.0f;
    [Tooltip("Movement Speed Variable for running")] [SerializeField] float runSpeed = 6.0f;
    [Tooltip("Are you in Demon form?")] public bool demonForm = false;
    [Tooltip("Minimum UP Down Clamp Rotation")] [SerializeField] float minClamp;
    [Tooltip("Maximum UP Down Clamp Rotation")] [SerializeField] float maxClamp;
    [Tooltip("StartPoint for Ground check, bottom of feet")] [SerializeField] Transform groundcheckLineStart;
    [Tooltip("EndPoint for Ground check, how far below feet do you want to check?")] [SerializeField] Transform groundcheckLineStop;
    [SerializeField] GameObject pCamera;
    [SerializeField] PlayerHealthStaminaUI playerGUIController;
    public Collider sword1;
    
    bool walk;
    float clampedX;
    float inputHorizontal;
    float rotY;

    #endregion

    #region Unity Native
    private void Start()
    {
        sword1.enabled = false;
       
      
    }
    private void Update()
    {
        divineAnim.SetBool("Walk", walk);
        PlayerAndCameraRotation();
        LightAttack();
        HardAttck();
        DivineDemonSwap();
        Sneer();
        
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
        clampedX = Mathf.Clamp(clampedX, minClamp, maxClamp);
        rotY += Input.GetAxis("Mouse X");
        pCamera.transform.localEulerAngles = new Vector3(clampedX, 0, 0);
        transform.localEulerAngles =  new Vector3(0, rotY, 0);
        
    }
    private void WalkingMovement()
    {
        
        float inputVertical = Input.GetAxis("Vertical");
       

        if (inputVertical >= 0.01f && !Input.GetButton("Run"))
        {
            divineAnim.SetBool("Run", false);
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);
            walk = true;

        }
        else if (inputVertical >= 0.01f && Input.GetButton("Run"))
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
    void DivineDemonSwap()
    {
        if(Input.GetButtonUp(("DemonSwap")) && !demonForm)
        {
            print("DEMON FORM");
            demonForm = true;
        }
        else if(Input.GetButtonUp(("DemonSwap")) && demonForm)
        {
            print("SAINT FORM");
            demonForm = false;
        }
    }
    void LightAttack()
    {
        if (Input.GetButtonUp("LightAttack"))
        {
            playerGUIController.DecreaseStamina(10);
            sword1.enabled = true;
         
            divineAnim.SetTrigger("LAttack1");
         
        }
    }
    void HardAttck()
    {
        
        if (Input.GetButtonUp("HardAttack"))
        {
           
            playerGUIController.DecreaseStamina(20);
            sword1.enabled = true;
            
            divineAnim.SetTrigger("HAttack1");
        }
    }

    void Sneer()
    {
        if(Input.GetKeyUp(KeyCode.Mouse2))
        {
            divineAnim.SetTrigger("Sneer");
        }
    }
    bool PlayerGrounded()
    {
        bool grounded = Physics.Linecast(groundcheckLineStart.position, groundcheckLineStop.position, 1 << LayerMask.NameToLayer("ground"));
        return grounded;
    }
    #endregion
}
