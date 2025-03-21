using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class PotEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;

    public GameObject slimeTrailPrefab; // Reference to the slime trail prefab
    public float timeBetweenSlimeDrop = 0.5f; // Time between slime drops
    private float slimeDropTimer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttack;
    bool alreadyAttacked;
    public GameObject hitbox;
    public float potdamge;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator anim;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        // Handle slime trail dropping as enemy moves
        DropSlimeTrail();
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
            walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        anim.SetBool("Walk", true);
    }

    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!alreadyAttacked)
            {
                collision.gameObject.GetComponent<HealthManager>().TakeDamage(potdamge);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttack);
            }
        }
    }*/

    void AttackPlayer()
    {
        agent.isStopped = true;

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            hitbox.SetActive(true);
            anim.SetTrigger("Attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    void ResetAttack()
    {
        hitbox.SetActive(false);  
        alreadyAttacked = false;
    }

    // Function to drop slime trail as the enemy moves
    void DropSlimeTrail()
    {
        // Drop slime trail every 'timeBetweenSlimeDrop' seconds
        if (slimeDropTimer <= 0)
        {
            // Create a new position for the slime trail, slightly lower than the enemy
            Vector3 slimePosition = new Vector3(transform.position.x, transform.position.y , transform.position.z);

            // Instantiate the slime trail at the lower position
            Instantiate(slimeTrailPrefab, slimePosition, Quaternion.identity);

            // Reset the timer
            slimeDropTimer = timeBetweenSlimeDrop;
        }
        else
        {
            slimeDropTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
