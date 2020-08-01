using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle, Patrol, Chase, Attack, Dead
    }

    public FSMStates currentState;
    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 5;
    public GameObject player;
    public GameObject[] spellProjectiles;
    public GameObject swordTip;
    public float shootRate = 2;
    public GameObject deadVFX;

    GameObject[] wanderPoints;
    Animator anim;
    Vector3 nextDestination;
    int curDestIndex = 0;
    float distanceToPlayer;
    float elapsedTime;
    UnityEngine.AI.NavMeshAgent agent;

    //EnemyHealth enemyHealth;
    int health;
    Transform deadTransform;

    bool isDead;
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //health = enemyHealth.currentHealth;
        health = 100;

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();

        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        swordTip = GameObject.FindGameObjectWithTag("SwordTip");

        currentState = FSMStates.Patrol;
        FindNextPoint();

        //enemyHealth = GetComponent<EnemyHealth>();
        //health = enemyHealth.currentHealth;
        isDead = false;
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) < 10)
        {
            Debug.Log("hello");
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.Towards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 2);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateAttackState()
    {
        anim.SetInteger("animState", 3);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);

        //enemy swings sword
        //EnemySpellCast();
    }

    void UpdateDeadState()
    {
        isDead = true;
        anim.SetInteger("animState", 4);
        deadTransform = gameObject.transform;

        Destroy(gameObject, 3);
    }

    private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    void FindNextPoint()
    {
        curDestIndex = (curDestIndex + 1) % wanderPoints.Length;
        nextDestination = wanderPoints[curDestIndex].transform.position;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("SpellCasting", animDuration);
                elapsedTime = 0.0f;
            }

        }
    }

    void SpellCasting()
    {
        int randomIndex = Random.Range(0, spellProjectiles.Length);
        GameObject spellProjectile = spellProjectiles[randomIndex];


        Instantiate(spellProjectile, swordTip.transform.position, swordTip.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //chase
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
