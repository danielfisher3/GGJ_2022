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
    [Tooltip("Map Object")] [SerializeField] GameObject liveMap;
    [Tooltip("Inventory Panel")] [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject pCamera;
    [SerializeField] PlayerHealthStaminaUI playerGUIController;
    public Collider sword1;
    [SerializeField] GameObject divineSword;
    [SerializeField] GameObject divineAura;
    [SerializeField] GameObject evilSword;
    [SerializeField] GameObject evilAura;
    [SerializeField] GameObject forceFieldD;
    [SerializeField] GameObject forceFieldE;
    public bool dead = false;
    bool walk;
    float clampedX;
    float inputHorizontal;
    float rotY;
    bool forceActive = false;
    public bool forceDeployed = false;
    float Timer;
    [SerializeField] float timeToOffFF = 2.0f;
    float timesincelastattackLight;
    float attackCooldownLight = 1.0f;
    float timesincelastAttackHeavy;
    float attackCooldownHeavy = 1.0f;
    public bool swapSwordColliders;
    [SerializeField] GameObject divineSwordNoC, divineSwordC, evilSwordNoC, evilSwordC;
    #endregion

    #region Unity Native
    private void Awake()
    {
       
    }
    private void Start()
    {
        
        sword1.enabled = false;

        if (liveMap.activeInHierarchy)
        {
            liveMap.SetActive(false);
        }
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
    }
    private void Update()
    {
        divineAnim.SetBool("Walk", walk);
        PlayerAndCameraRotation();
        LightAttack();
        HardAttck();
        OpenCloseMap();
        OpenCloseInventory();
        DivineDemonSwap();
        Sneer();
        ForcefieldDeployment();
        Die();
        if(forceDeployed && forceActive)
        {
            Timer += Time.deltaTime;
            if (Timer >= timeToOffFF)
            {
                if (forceFieldD.activeInHierarchy)
                {
                    forceFieldD.SetActive(false);
                    forceActive = false;
                }
                if (forceFieldE.activeInHierarchy)
                {
                    forceFieldE.SetActive(false);
                    forceActive = false;
                }
                forceDeployed = false;
            }
        }
        if (swapSwordColliders && demonForm)
        {
            evilSwordC.SetActive(false);
            evilSwordNoC.SetActive(true);
            swapSwordColliders = false;
        }
        if(swapSwordColliders && !demonForm)
        {
            divineSwordC.SetActive(false);
            divineSwordNoC.SetActive(true);
            swapSwordColliders = false;
        }
    }

    private void ForcefieldDeployment()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !demonForm && !forceActive && !forceDeployed)
        {
            if (!forceFieldD.activeInHierarchy)
            {
                forceFieldD.SetActive(true);
                forceActive = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Q) && demonForm && !forceActive && !forceDeployed)
        {
            if (!forceFieldE.activeInHierarchy)
            {
                forceFieldE.SetActive(true);
                forceActive = true;
            }
        }
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
           
            demonForm = true;
            if (!evilSword.activeInHierarchy && !evilAura.activeInHierarchy)
            {
                evilAura.SetActive(true);
                evilSword.SetActive(true);
                divineSword.SetActive(false);
                divineAura.SetActive(false);

            }
        }
        else if(Input.GetButtonUp(("DemonSwap")) && demonForm)
        {
            
            demonForm = false;
            if(!divineAura.activeInHierarchy && !divineSword.activeInHierarchy)
            {
                divineSword.SetActive(true);
                divineAura.SetActive(true);
                evilAura.SetActive(false);
                evilSword.SetActive(false);
            }
        }
    }
    void LightAttack()
    {
        if (playerGUIController.currentStamina >= 10)
        {
            if (Input.GetButtonUp("LightAttack") && Time.time > timesincelastattackLight)
            {
                timesincelastattackLight = Time.time + attackCooldownLight;
                playerGUIController.DecreaseStamina(10);
                

                divineAnim.SetBool("LAttack1",true);

            }
        }
    }
    void HardAttck()
    {
        if (playerGUIController.currentStamina >= 20)
        {
            if (Input.GetButtonUp("HardAttack") && Time.time > timesincelastAttackHeavy)
            {
                timesincelastAttackHeavy = Time.time + attackCooldownHeavy;
                playerGUIController.DecreaseStamina(20);
               

                divineAnim.SetBool("HAttack1",true);
            }
        }
    }

    void Die()
    {
        if(playerGUIController.currentHealth < 1)
        {
            divineAnim.SetBool("HAttack1", false);
            divineAnim.SetBool("LAttack1", false);
            divineAnim.SetBool("Run", false);
            divineAnim.SetBool("Walk", false);
            divineAnim.SetBool("Death", true);
            dead = true;
            this.enabled = false;
        }
    }
    void Sneer()
    {
        if(Input.GetKeyUp(KeyCode.Mouse2))
        {
            divineAnim.SetTrigger("Sneer");
        }
    }

    void OpenCloseMap()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (liveMap.activeInHierarchy)
            {
                liveMap.SetActive(false);
                
            }
            else if (!liveMap.activeInHierarchy)
            {
                liveMap.SetActive(true);
               
            }
        }
    }
    void OpenCloseInventory()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(false);
                
            }
            else if (!inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(true);
               
            }
        }
    }

    bool PlayerGrounded()
    {
        bool grounded = Physics.Linecast(groundcheckLineStart.position, groundcheckLineStop.position, 1 << LayerMask.NameToLayer("ground"));
        return grounded;
    }
    #endregion
}
