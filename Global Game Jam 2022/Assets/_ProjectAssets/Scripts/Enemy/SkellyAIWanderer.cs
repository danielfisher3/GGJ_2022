using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkellyAIWanderer : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemyAgent;
    Animator enemyAnim;
    Rigidbody enemyRGBY;
   
    
    enum EnemyStates { Idle,Walk,Run,Attack};
    [SerializeField] EnemyStates enemyState;
    bool idle = false;
    bool walking = false;
    bool running = false;
    bool attack = false;

    [Header("Misc. Variables")]
    [Tooltip("Enemy Movement Speed, this changes the nav mesh agent speed")]
    [SerializeField] float eMoveSpeed = 1.5f;

    [Tooltip("Enemy Wander Radius Distance")]
    [SerializeField] float wanderDistance = 25.0f;

    [SerializeField] Transform line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop,line4Start,line4Stop, line5Start,line5Stop;
    private void Awake()
    {
       
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponentInChildren<Animator>();
        enemyRGBY = GetComponent<Rigidbody>();
        
    }
    void Start()
    {
        enemyAgent.speed = eMoveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyState = EnemyStates.Idle;
        
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch();
        enemyAnim.SetBool("Walk", walking);
        enemyAnim.SetBool("Run", running);
        enemyAnim.SetBool("Attack", attack);
        enemyAnim.SetBool("Idle", idle);
    }

   

    private void LateUpdate()
    {
       
    }

   
    #region Back-End State Machine Methods
    void Idle()
    {
        idle = true;
        walking = false;
        running = false;
        attack = false;
    }
    void Walk()
    {
        idle = false;
        walking = true;
        running = false;
        attack = false;
    }
    void Run()
    {
        idle = false;
        walking = false;
        running = true;
        attack = false;
    }
    void Attack()
    {
        idle = false;
        walking = false;
        running = false;
        attack = true;
    }
    private void StateSwitch()
    {
        switch (enemyState)
        {
            case EnemyStates.Idle:
                Idle();
                break;

            case EnemyStates.Walk:
                Walk();
                break;

            case EnemyStates.Run:
                Run();
                break;

            case EnemyStates.Attack:
                Attack();
                break;

            default:
                Debug.Log("UNRECOGNIZED STATE in skelly backend state machine");
                break;
        }
    }
    #endregion

}
