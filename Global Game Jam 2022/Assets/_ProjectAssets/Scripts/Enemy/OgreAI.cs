using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Panda;

public class OgreAI : MonoBehaviour
{
    [SerializeField] Transform player;
    bool hasBeenhit = false;
    bool hasBeenTickled = false;
    [SerializeField] Slider bossHealthSlider;
    [SerializeField] int maxHealth = 1000;
    [SerializeField] int currenthealth;
    [SerializeField] Vector3 destination; // The movement destination.
    [SerializeField] Vector3 target;      // The position to aim to.
    [SerializeField] float visibleRange = 80.0f;
    [SerializeField] float attackRange = 40.0f;
    [SerializeField] float rotSpeed;
    [SerializeField] float minXrange, maxXRange, minZRange, maxZRange, x, z;
    PandaBehaviour bTree;
    Vector3 lastAttackingPos;
    public Animator bossAnim;
    bool phase2;
    NavMeshAgent bossAgent;
    public bool dead = false;
    [SerializeField] Text bossSliderText;
    float timeSinceLasthit;
    float hitCoolDown = 1.0f;
    float timesinceLastBoost;
    float boostCoolDown = 1.0f;
    private void Awake()
    {
        bossAnim = GetComponentInChildren<Animator>();
        bTree = GetComponent<PandaBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bossAgent = GetComponent<NavMeshAgent>();
        bossHealthSlider.gameObject.SetActive(true);
        bossSliderText.text = "Greg the Ogre King";
        phase2 = false;
        bossHealthSlider.maxValue = maxHealth;
        currenthealth = maxHealth;
        bossHealthSlider.value = currenthealth;
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthSlider.value = currenthealth;
        if (currenthealth <= (maxHealth / 2.0f))
        {
            phase2 = true;
        }

        if (phase2)
        {
            bossAgent.speed = 3.5f;
            visibleRange = 20;
        }
        else
        {
            bossAgent.speed = 2.0f;
            visibleRange = 10;
        }

    }
    [Task]
    private void ReactToTickle()
    {
        if (hasBeenTickled && currenthealth <= 950 && Time.time > timesinceLastBoost)
        {
            timesinceLastBoost = Time.time + boostCoolDown;
            currenthealth = currenthealth + 50;
            hasBeenTickled = false;
            Task.current.Succeed();
        }
    }

    [Task]
    private void ReactToHit()
    {
        if (hasBeenhit )
        {
           
            bossAnim.SetBool("BeenHit",true);
            currenthealth = currenthealth - 50;
            hasBeenhit = false;
            Task.current.Succeed();
        }
    }

    [Task]
    public bool InPhaseTwo()
    {
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("phase2={0}", phase2);

        return phase2;
    }
    [Task]
    public void PickDestination()
    {
        Vector3 dest = new Vector3(x, 0, z);
        bossAgent.SetDestination(dest);
        Task.current.Succeed();
    }

    [Task]
    public void PickRandomDestination()
    {
        Vector3 dest = new Vector3(Random.Range(minXrange, maxXRange), 0, Random.Range(minZRange, maxZRange));
        bossAgent.SetDestination(dest);
        if (!phase2)
        {
            bossAnim.SetBool("Walk", true);
            bossAnim.SetBool("Run", false);
        }
        else if (phase2)
        {
            bossAnim.SetBool("Walk", false);
            bossAnim.SetBool("Run", true);
        }
        Task.current.Succeed();
    }

    [Task]
    public void MoveToDestination()
    {
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);

        if (bossAgent.remainingDistance <= bossAgent.stoppingDistance && !bossAgent.pathPending)
        {
            if (!phase2)
            {
                bossAnim.SetBool("Walk", false);

            }
            else if (phase2)
            {
                bossAnim.SetBool("Run", false);
            }
            Task.current.Succeed();
        }
    }

    [Task]
    public void TargetPlayer()
    {
        target = player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    bool Turn(float angle)
    {
        var p = this.transform.position + Quaternion.AngleAxis(angle, Vector3.up) * this.transform.forward;
        target = p;
        return true;
    }

    [Task]
    public void LookAtTarget()
    {
        Vector3 direction = target - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotSpeed);

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("angle={0}",
                Vector3.Angle(this.transform.forward, direction));

        if (Vector3.Angle(this.transform.forward, direction) < 5.0f)
        {
            Task.current.Succeed();
        }
    }



    [Task]
    bool SeePlayer()
    {
        Vector3 distance = player.transform.position - this.transform.position;

        RaycastHit hit;
        bool seeWall = false;

        Debug.DrawRay(this.transform.position, distance, Color.red);

        if (Physics.Raycast(this.transform.position, distance, out hit))
        {
            if (hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("wall={0}", seeWall);

        if (distance.magnitude < visibleRange && !seeWall)
            return true;
        else
            return false;
    }

    [Task]
    public bool IsHealthLessThan(int health)
    {
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("health={0}", this.currenthealth);

        return this.currenthealth < health;
    }

    [Task]
    public bool InDanger(float minDist)
    {
        Vector3 distance = player.transform.position - this.transform.position;
        return (distance.magnitude < minDist);
    }

    [Task]
    public void TargetAttackPos()
    {
        target = lastAttackingPos;
        Task.current.Succeed();
    }

    [Task]
    public void TakeCover()
    {
        Vector3 awayFromPlayer = this.transform.position - player.transform.position;

        //increased the flee range to the agent
        //has further to come back
        Vector3 dest = this.transform.position + awayFromPlayer * 5;
        bossAgent.SetDestination(dest);
        if (phase2)
        {
            bossAnim.SetBool("Run", true);
            bossAnim.SetBool("Walk", false);
        }
        else if (!phase2)
        {
            bossAnim.SetBool("Walk", true);
            bossAnim.SetBool("Run", false);
        }
        //remember where we were before fleeing
        //and get angry
        lastAttackingPos = this.transform.position;

        Task.current.Succeed();
    }

    [Task]
    public bool HasBeenHit()
    {
        return hasBeenhit;
    }

    [Task]
    public bool HaveBeenTickled()
    {
        return hasBeenTickled;
    }

    [Task]
    bool AttackLinedUp()
    {
        Vector3 distance = target - this.transform.position;
        if (distance.magnitude < attackRange)
            return true;
        else
            return false;
    }
    [Task]
    public void Die()
    {
        dead = true;
        bossAnim.SetBool("Walk", false);
        bossAnim.SetBool("Run", false);
        bossAnim.SetBool("Attack2", false);
        bossAnim.SetBool("Attack1", false);
        bossAnim.SetBool("Attack3", false);

        bossAnim.SetBool("Death", true);
        bTree.enabled = false;
        Task.current.Succeed();

    }
    [Task]
    public void Attack1()
    {
        bossAnim.SetBool("Walk", false);
        bossAnim.SetBool("Run", false);
        bossAnim.SetBool("Attack2", false);
        bossAnim.SetBool("Attack1", true);
        bossAnim.SetBool("Attack3", false);

        Task.current.Succeed();
    }
    [Task]
    public void Attack2()
    {
        bossAnim.SetBool("Walk", false);
        bossAnim.SetBool("Run", false);
        bossAnim.SetBool("Attack1", false);
        bossAnim.SetBool("Attack2", true);
        bossAnim.SetBool("Attack3", false);

        Task.current.Succeed();

    }
    [Task]
    public void Attack3()
    {
        bossAnim.SetBool("Walk", false);
        bossAnim.SetBool("Run", false);
        bossAnim.SetBool("Attack2", false);
        bossAnim.SetBool("Attack1", false);
        bossAnim.SetBool("Attack3", true);

        Task.current.Succeed();
    }

    [Task]
    public void SetTargetDestination()
    {
        bossAgent.SetDestination(target);
        if (phase2)
        {
            bossAnim.SetBool("Run", true);

        }
        else if (!phase2)
        {
            bossAnim.SetBool("Walk", true);
        }
        Task.current.Succeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenhit || hasBeenTickled) return;
        if (other.gameObject.tag == "PlayerSword")
        {
            if (other.gameObject.name == "DivineSword")
            {
                hasBeenTickled = true;
                hasBeenhit = false;
            }
            else if (other.gameObject.name == "EvilSword")
            {
                hasBeenhit = true;
                hasBeenTickled = false;
            }
        }
    }
}
