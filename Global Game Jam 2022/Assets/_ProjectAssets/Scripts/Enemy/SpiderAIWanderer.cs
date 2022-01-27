using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using System;

public class SpiderAIWanderer : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    Animator enemyAnim;

    [SerializeField] enemyHealthUI enemyHealth;
    [SerializeField] HitDetection hitCheck;

    [Header("Misc. Variables")]
    [Tooltip("Enemy Movement Speed, this changes the nav mesh agent speed")]
    [SerializeField] float eMoveSpeed = 1.5f;
   
    [Tooltip("Enemy Wander Radius Distance")]
    [SerializeField] float wanderDistance = 25.0f;
    [SerializeField] GameObject player;
    [SerializeField] Transform line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop, line4Start, line4Stop, line5Start, line5Stop;
  
   
    public bool randomMovementreq = false;
    public bool randomAttackreq = false;

    PandaBehaviour pbehaviorTree;


    enum RandomAttack { Attack1, Attack2, NoAttack };

    List<RandomAttack> rAttackSet = new List<RandomAttack>();


    [SerializeField] RandomAttack rAttack;


    private void Awake()
    {


        rAttackSet.Add(RandomAttack.Attack1);
        rAttackSet.Add(RandomAttack.Attack2);
      


        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponentInChildren<Animator>();
        pbehaviorTree = GetComponent<PandaBehaviour>();


    }
    void Start()
    {
        enemyAgent.speed = eMoveSpeed;

        rAttack = RandomAttack.NoAttack;




    }

    // Update is called once per frame
    void Update()
    {
        RandomAttackSwitch();

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
        if (distance > playerFarDistance)
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
        rAttack = RandomAttack.NoAttack;
        enemyAgent.speed = 1.5f;
        enemyAgent.isStopped = false;
        enemyAgent.SetDestination(GrabRandomNavPoint());
        enemyAnim.SetBool("Walk", true);
        Task.current.Succeed();

    }

    [Task]
    public void LineUpOnPlayer()
    {
        rAttack = RandomAttack.NoAttack;
        transform.LookAt(player.transform);
        Task.current.Succeed();

    }
    [Task]
    public void DetermineIfAtWaypoint()
    {
        if (enemyAgent.remainingDistance < 0.5f)
        {
            enemyAgent.isStopped = true;
            enemyAnim.SetBool("Walk", false);
            Task.current.Succeed();
        }
    }


    [Task]
    public bool InPosition()
    {
        return AttackDistanceChecker();
    }

    [Task]
    public void GrabRandomAttack()
    {

        if (!randomAttackreq)
        {
            rAttack = GrabRandomAttackState();
            randomAttackreq = true;
            Task.current.Succeed();
        }
        else
        {
            rAttack = RandomAttack.NoAttack;
        }

    }

    [Task]
    public void MoveTowardsPlayer()
    {
        if (AttackDistanceChecker() == false)
        {

            enemyAgent.isStopped = false;

            NavMeshHit hit;
            NavMesh.SamplePosition(player.transform.position, out hit, 10, -1);
            enemyAgent.speed = eMoveSpeed + 2;
            enemyAnim.SetBool("Walk", true);
            randomAttackreq = false;
            enemyAgent.SetDestination(hit.position);
        }


    }
    [Task]
    public bool HasEnemyBeenHit()
    {
        return hitCheck.hasbeenhit;
    }
    [Task]
    public bool IsHealthLessThan(int minHealth)
    {
        if (enemyHealth.currentHealth < minHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    [Task]
    public void TakeHit()
    {

        enemyAnim.SetTrigger("BeenHit");
        Task.current.Succeed();
    }
    [Task]
    public void Die()
    {
        enemyAnim.SetBool("Death", true);
        pbehaviorTree.enabled = false;
        Task.current.Succeed();
    }
    [Task]
    public bool AttackRange(float mindistance)
    {
        if (DistanceFromPlayer() <= mindistance)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        if (DistanceFromPlayer() > 1)
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

    public void Attack1()
    {

        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
     
        enemyAnim.SetBool("Attack2", false);
       
        enemyAnim.SetBool("Attack1", true);

    }


    public void Attack2()
    {

        enemyAgent.isStopped = true;
        enemyAnim.SetBool("Walk", false);
     
        enemyAnim.SetBool("Attack1", false);
       
        enemyAnim.SetBool("Attack2", true);

    }

  
    private void RandomAttackSwitch()
    {
        switch (rAttack)
        {
            case RandomAttack.Attack1:
                Attack1();
                break;

            case RandomAttack.Attack2:
                Attack2();
                break;

          

            case RandomAttack.NoAttack:
                NoAttack();
                break;


        }
    }

    void NoAttack()
    {
        enemyAnim.SetBool("Attack2", false);
      
        enemyAnim.SetBool("Attack1", false);
        
        randomAttackreq = false;
    }
    RandomAttack GrabRandomAttackState()
    {
        int rIndex = UnityEngine.Random.Range(0, rAttackSet.Count);
        RandomAttack attackSelected = rAttackSet[rIndex];
        return attackSelected;
    }



    #endregion

}


