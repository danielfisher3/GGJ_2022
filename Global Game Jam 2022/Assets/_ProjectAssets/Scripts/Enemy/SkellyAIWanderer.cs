using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class SkellyAIWanderer : MonoBehaviour
{
    
    NavMeshAgent enemyAgent;
    Animator enemyAnim;
    Rigidbody enemyRGBY;
    bool walking;
    [Header("Misc. Variables")]
    [Tooltip("Enemy Movement Speed, this changes the nav mesh agent speed")]
    [SerializeField] float eMoveSpeed = 1.5f;

    [Tooltip("Enemy Wander Radius Distance")]
    [SerializeField] float wanderDistance = 25.0f;

    [SerializeField] Transform line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop;
    private void Awake()
    {
       
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponentInChildren<Animator>();
        enemyRGBY = GetComponent<Rigidbody>();
    }
    void Start()
    {
        enemyAgent.speed = eMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnim.SetBool("Walk", walking);
        print(CanSeePlayer());
    }
    [Task]
    public void MoveToDestination()
    {
        Vector3 newDestination = EnemyUtilities.RandomNavSphere(this.transform.position, wanderDistance, -1);
        enemyAgent.SetDestination(newDestination);
        walking = true;
        Task.current.Succeed();
    }
    
    [Task]
    public void MadeItToDestination()
    {
        if(enemyAgent.remainingDistance < 1.0f )
        {
            walking = false;
            Task.current.Succeed();
        }
    }

    [Task]
    public bool CanSeePlayer()
    {
        return EnemyUtilities.CanSeePlayer(line1Start, line1Stop, line2Start, line2Stop, line3Start, line3Stop);
    }
    
}
