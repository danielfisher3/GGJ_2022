using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;


public class SkellyAIWanderer : MonoBehaviour
{ 
    NavMeshAgent enemyAgent;
    Animator enemyAnim;
    
   
    
    [Header("Misc. Variables")]
    [Tooltip("Enemy Movement Speed, this changes the nav mesh agent speed")]
    [SerializeField] float eMoveSpeed = 1.5f;
    float timecount;
    [Tooltip("Enemy Wander Radius Distance")]
    [SerializeField] float wanderDistance = 25.0f;
    [SerializeField] GameObject player;
    [SerializeField] Transform line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop,line4Start,line4Stop, line5Start,line5Stop;
    [SerializeField] float rotSpeed = 1;
    [SerializeField] Transform playerLookPoint;
    float timeCount;
    private void Awake()
    {
       
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponentInChildren<Animator>();
        
        
    }
    void Start()
    {
        enemyAgent.speed = eMoveSpeed;
      
       
        
    }

    // Update is called once per frame
    void Update()
    {
        timecount = timecount + Time.deltaTime;
    }

   

    private void LateUpdate()
    {
       
    }

    #region Tasks
    [Task]
    public bool CheckIfPlayerIsOnEnemy()
    {
        
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);

        if (CanSeePlayer())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    [Task]
    public bool PlayerFarEnoughAwayToPatrol(float playerFarDistance)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance > playerFarDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [Task]
    public void GoToDestination()
    {
        if (!enemyAgent.pathPending)
        {
            enemyAgent.isStopped = false;
            enemyAgent.SetDestination(GrabRandomNavPoint());
            enemyAnim.SetBool("Walk", true);
            Task.current.Succeed();
        }
    }

    [Task]
    public void LineUpOnPlayer()
    {

        transform.LookAt(player.transform);
        Task.current.Succeed();
      
    }
    [Task]
    public void DetermineIfAtWaypoint()
    {
        if(enemyAgent.remainingDistance < 0.5f)
        {
            enemyAgent.isStopped = true;
            enemyAnim.SetBool("Walk", false);
            Task.current.Succeed();
        }
    }
    [Task]
    public void Walk()
    {
        enemyAgent.isStopped = false;
        enemyAgent.SetDestination(player.transform.position);
        enemyAnim.SetBool("Walk", true);
        Task.current.Succeed();
    }
    [Task]
    public void Run()
    {
        enemyAgent.isStopped = false;
        enemyAgent.SetDestination(player.transform.position);
        enemyAnim.SetBool("Run", true);
        Task.current.Succeed();
    }
    [Task]
    public bool InPosition()
    {
        return AttackDistanceChecker();
    }
    [Task]
    public void Attack1()
    {
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetTrigger("Attack1");
        Task.current.Succeed();
    }
    [Task]
    public void Attack2()
    {
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetTrigger("Attack2");
        Task.current.Succeed();
    }
    [Task]
    public void Kick()
    {
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetTrigger("Kick");
        Task.current.Succeed();
    }
    [Task]
    public void Cast()
    {
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetTrigger("Casting");
        Task.current.Succeed();
    }
    [Task]
    public void Combo()
    {
        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetTrigger("Combo");
        Task.current.Succeed();
    }
    #endregion

    #region CustomMethods
    Vector3 GrabRandomNavPoint()
    {
        Vector3 newDestination = EnemyUtilities.RandomNavSphere(this.gameObject.transform.position, wanderDistance, -1);
        return newDestination;
    }
    bool CanSeePlayer()
    {
        bool canSee = EnemyUtilities.CanSeePlayer(line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop, line4Start, line4Stop, line5Start,
            line5Stop);
        return canSee;
    }
    bool AttackDistanceChecker()
    {
        if(DistanceFromPlayer() > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
   
   

   float DistanceFromPlayer()
   {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance;
   }

    #endregion

}
