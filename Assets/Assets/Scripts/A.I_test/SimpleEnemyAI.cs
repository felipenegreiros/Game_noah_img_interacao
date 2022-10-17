using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Projectil;
    public Transform player;
    [SerializeField] Animator anima;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyattacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Transform alvo;

    private void Awake()
    {
        player = GameObject.Find("personagemGame (1)").transform;
        agent = GetComponent<NavMeshAgent>();
        anima.SetBool("walk", true);

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        anima.SetBool("Attack", false);
        anima.SetBool("walk", true);
        // Debug.Log("patrol");

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkpoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        anima.SetBool("Attack", false);
        anima.SetBool("walk", true);

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.Log("voidSearchWalkpoint");
        //nao parece ser nada desse void

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("ifSearchWalkpoint");
        }
    }

    private void ChasePlayer()
    {
        anima.SetBool("Attack", false);
        agent.SetDestination(player.transform.position);


        anima.SetBool("walk", true);
        // transform.LookAt(player);
    }
    private void AttackPlayer()
    {
        //agent.SetDestination(transform.position);

        agent.SetDestination(player.transform.position / 2);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        anima.SetBool("Attack", true);

        if (!alreadyattacked)
        {
            Rigidbody rb = Instantiate(Projectil, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 4f, ForceMode.Impulse);
            rb.AddForce(transform.up * 0.4f, ForceMode.Impulse);



            alreadyattacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        anima.SetBool("Attack", false);
    }

    private void ResetAttack()
    {
        anima.SetBool("Attack", false);
        alreadyattacked = false;
    }
}
