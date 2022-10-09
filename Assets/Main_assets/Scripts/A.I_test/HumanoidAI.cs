using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgente;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_timeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_timeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgente = GetComponent<NavMeshAgent>();

        navMeshAgente.isStopped = false;
        navMeshAgente.speed = speedWalk;
        navMeshAgente.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Patroling()
    {
        if(m_PlayerNear)
        {
            if(m_timeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_timeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgente.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if(navMeshAgente.remainingDistance <= navMeshAgente.stoppingDistance)
            {
                if(m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    void Move(float speed)
    {
        navMeshAgente.isStopped = false;
        navMeshAgente.speed = speed;
    }
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgente.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    void Stop()
    {
        navMeshAgente.isStopped = true;
        navMeshAgente.speed = 0;
    }
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }
    void LookingPlayer(Vector3 player)
    {
        navMeshAgente.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgente.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_timeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }

            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }
}
